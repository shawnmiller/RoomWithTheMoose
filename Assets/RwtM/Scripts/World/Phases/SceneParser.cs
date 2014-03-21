using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

delegate object BuildHandler(string line);

public static class SceneParser
{
  private const int CMD = 0;
  private const int VAL = 1;

  static BuildHandler buildHandler;
  static Stack<string> modeStack;


  public static Phase[] BuildScene(TextAsset sceneFile, MonoBehaviour caller)
  {
    bool bShouldPopStack = false;
    bool bWasValid = false;
    modeStack = new Stack<string>();
    modeStack.Push(PPS.PP_NO_MODE);

    List<Phase> Phases = new List<Phase>();
    Dictionary<string, object> MostRecentObject = new Dictionary<string, object>() { 
      {PPS.PP_PHASE_OPEN, null},
      {PPS.PP_CUSTOM_EVENT_BEGIN, null},
      {PPS.PP_OBJECT_VARIABLE, null},
      {PPS.PP_OBJECT_TIMER, null},
      {PPS.PP_OBJECT_SOUND, null},
      {PPS.PP_OBJECT_TRIGGER, null}, 
      {PPS.PP_
    };

    Regex regex = new Regex(@"\t*");
    string temp = regex.Replace(sceneFile.text, "");
    regex = new Regex(@" {2,}");
    temp = regex.Replace(temp, " ");
    regex = new Regex(@" *= *");
    temp = regex.Replace(temp, "=");

    StreamWriter newFile = new StreamWriter(@"C:\Users\Shawn\Desktop\Testing.txt");
    newFile.Write(temp);
    newFile.Close();
    //Debug.Log("Done.");

    StreamReader fileReader = new StreamReader(@"C:\Users\Shawn\Desktop\Testing.txt");
    int lineNumber = 0;
    while (!fileReader.EndOfStream)
    {
      bShouldPopStack = false;
      ++lineNumber;
      string line = fileReader.ReadLine();

      // Check for missing spaces between inputs
      for (int i = 0; i < line.Length; ++i)
      {
        if (line[i] == '=')
        {
          int nextSpace = line.IndexOf(' ', i+1);
          int nextEquals = line.IndexOf('=', i + 1);
          if ((nextSpace != -1) && (nextEquals != -1) && (nextSpace > nextEquals))
          {
            Debug.LogError("Two inputs were defined but not separated by a space. (Line #" + lineNumber + ") Failing to fix this will cause unintended results and potential crashes.");
            Debug.LogError(line);
            Debug.Break();
          }
        }
      }

      string[] inputs = StringHelper.SplitTextDelimited(line, " ");

      for (int j = 0; j < inputs.Length; ++j)
      {
        string[] inputComponents = StringHelper.SplitTextDelimited(inputs[j], "=");

        if (PPS.IsValidForMode(modeStack.Peek(), inputComponents[CMD]))
        {
          bWasValid = true;
          switch (inputComponents[CMD])
          {
            case PPS.PP_COMMENT_LINE:
              continue;
            case PPS.PP_PHASE_OPEN:
              modeStack.Push(PPS.PP_PHASE_OPEN);
              Phases.Add(new Phase());
              MostRecentObject[modeStack.Peek()] = Phases.GetLast();
              break;
            case PPS.PP_CUSTOM_EVENT_BEGIN:
              modeStack.Push(PPS.PP_CUSTOM_EVENT_BEGIN);
              PhaseEvent pEvent = new PhaseEvent();
              Phases.GetLast().AddPhaseEvent(pEvent);
              MostRecentObject[PPS.PP_CUSTOM_EVENT_BEGIN] = pEvent;
              break;
            // Pop the current mode off the stack whenever we reach and end definition
            case PPS.PP_PHASE_CLOSE:
            case PPS.PP_CUSTOM_EVENT_END:
              // These 2 modes create their own object, purge it so that we don't accidentally modify it further.
              MostRecentObject[modeStack.Peek()] = null;
              goto case PPS.PP_PHASE_SETUP_END;
            case PPS.PP_PHASE_SETUP_END:
              bShouldPopStack = true;
              break;
          }
        }
        else
        {
          Debug.LogError("Encountered \"" + inputComponents[CMD] + "\" while in " + modeStack.Peek() + ". This is not currently allowed. (Line " + lineNumber + ")");
        }
      }
      // Done handling the line of text, if we were handling a single-line component, pop that mode off the stack.
      if (bWasValid && 
          (modeStack.Peek() == PPS.PP_OBJECT_CUSTOM_EVENT ||
          modeStack.Peek() == PPS.PP_OBJECT_SOUND ||
          modeStack.Peek() == PPS.PP_OBJECT_TIMER ||
          modeStack.Peek() == PPS.PP_OBJECT_TRIGGER ||
          modeStack.Peek() == PPS.PP_OBJECT_VARIABLE ||
          modeStack.Peek() == PPS.PP_ACTION_DISABLE_TRIGGER ||
          modeStack.Peek() == PPS.PP_ACTION_ENABLE_TRIGGER ||
          modeStack.Peek() == PPS.PP_ACTION_END_PHASE ||
          modeStack.Peek() == PPS.PP_ACTION_PLAY_SOUND ||
          modeStack.Peek() == PPS.PP_ACTION_MAKE_INTERACTIBLE ||
          modeStack.Peek() == PPS.PP_ACTION_REMOVE_INTERACTIBLE ||
          modeStack.Peek() == PPS.PP_ACTION_INCREMENT_VALUE ||
          modeStack.Peek() == PPS.PP_ACTION_SET_VALUE ||
          modeStack.Peek() == PPS.PP_ACTION_WAIT ||
          modeStack.Peek() == PPS.PP_EVENT_ENTER_TRIGGER ||
          modeStack.Peek() == PPS.PP_EVENT_ITEM_PICKUP ||
          modeStack.Peek() == PPS.PP_EVENT_MATH_CONDITION ||
          modeStack.Peek() == PPS.PP_EVENT_TIMER_COMPLETED))
      {
        bShouldPopStack = true;
      }

      if (bShouldPopStack)
        modeStack.Pop();
    }

    fileReader.Close();

    return null;
    //throw new System.NotImplementedException();
  }
}