using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class SceneParserV2
{
  private const int CMD = 0;
  private const int VAL = 1;

  private static Phase CurrentPhase;
  private static PhaseManager Manager;

  private static int CurrentLine = 0;

  public static void ReadSceneFile(TextAsset file)
  {
    Manager = PhaseManager.Get();
    CurrentPhase = new Phase();

    Regex regex = new Regex(@"\t*");
    string temp = regex.Replace(file.text, "");
    regex = new Regex(@" {2,}");
    temp = regex.Replace(temp, " ");
    regex = new Regex(@" *= *");
    temp = regex.Replace(temp, "=");
    regex = new Regex(Environment.NewLine + @"{2,}");
    temp = regex.Replace(temp, "");

    StreamWriter newFile = new StreamWriter(@"C:\Users\Shawn\Desktop\Testing.txt");
    newFile.Write(temp);
    newFile.Close();

    StreamReader fileReader = new StreamReader(@"C:\Users\Shawn\Desktop\Testing.txt");
    
    while (!fileReader.EndOfStream)
    {
      ++CurrentLine;
      Debug.Log("Line #" + CurrentLine);

      string line = fileReader.ReadLine();
      
      if(String.IsNullOrEmpty(line) || line.Trim().Length == 0)
      {
        Debug.Log("Skipping: Empty Line");
        continue;
      }
      if (line.TrimStart().Substring(0, 2) == PPS.PP_COMMENT_LINE)
      {
        Debug.Log("Skipping: Comment Line");
        continue;
      }
      if (line == PPS.PP_PHASE_CLOSE)
      {
        Debug.Log("Phase Ended");
        Manager.AddPhase(CurrentPhase);
        CurrentPhase = null;
      }
      else
      {
        switch (line.GetWord(0, " ")) 
        {
          case PPS.PP_PHASE_OPEN:
          case PPS.PP_GLOBAL_OPEN:
            Debug.Log("Beginning New Phase");
            CurrentPhase = new Phase();
            break;
          case PPS.PP_GLOBAL_CLOSE:
            Manager.AddGlobalPhase(CurrentPhase);
            CurrentPhase = null;
            break;
          case PPS.PP_CUSTOM_EVENT_OPEN:
            string eName = line.GetWord(1, " ");
            eName = eName.SplitTextDelimited("=")[1];

            // Since Events can have prerequisite 
            string preReqCheck = line.GetLastWord(" ");
            if (preReqCheck.SplitTextDelimited("=")[1] == PPS.PP_PARAM_EVENT_REQ)
            {
              CreateCustomEvent(ref fileReader, eName, Int32.Parse(preReqCheck.SplitTextDelimited("=")[1]));
            }
            else
            {
              CreateCustomEvent(ref fileReader, eName);
            }

            break;
          case PPS.PP_EVENT_BEGIN_PHASE:
          case PPS.PP_EVENT_ENTER_TRIGGER:
          case PPS.PP_EVENT_ITEM_PICKUP:
          case PPS.PP_EVENT_TIMER_COMPLETED:
            Debug.Log("Creating Event Watcher");
            CreateEventWatcher(line);
            break;
          case PPS.PP_EVENT_MATH_CONDITION:
            CurrentPhase.AddConditional(CreateConditional(line));
            break;
          case PPS.PP_OBJECT_TIMER:
          case PPS.PP_OBJECT_VARIABLE:
          case PPS.PP_OBJECT_SOUND:
            Debug.Log("Creating Object");
            CreateObject(line);
            break;
          case PPS.PP_PHASE_CLOSE:
            break;
          default:
            Debug.Log("Couldn't parse line: " + line);
            break;
        }
      }
    }
  }

  // Note that this does not return the first value, which is always assumed to be the identifier.
  private static List<KeyValuePair<string, string>> BreakLine(string line)
  {
    List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
    string[] inputs = line.SplitTextDelimited(" ");
    for (int i = 1; i < inputs.Length; ++i) 
    {
      string[] inputSplit = inputs[i].SplitTextDelimited("=");
      KeyValuePair<string, string> pair = new KeyValuePair<string, string>(inputSplit[CMD], inputSplit[VAL]);
      pairs.Add(pair);
    }
    return pairs;
  }

  private static void SetProperty(object obj, string property, string value, string typeOverride = "NullType")
  {
    /*Debug.Log("Properties found for " + obj.GetType().ToString() + ":");
    foreach (PropertyInfo p in obj.GetType().GetProperties())
    {
      Debug.Log(p.Name);
    }*/

    if (property == "Value")
    {
      Debug.Log("Working With Value");
    }

    PropertyInfo pInfo = obj.GetType().GetProperty(PPS.GetParameterLiteralName(property));

    if (pInfo == null)
    {
      Debug.LogError(property + " was not found in type " + obj.GetType().ToString());
    }
    else
    {
      object val;
      if (typeOverride == "NullType")
      {
        val = TypeConversion.Convert(PPS.GetParameterType(property), value);
      }
      else
      {
        val = TypeConversion.Convert(Type.GetType(typeOverride), value);
      }

      pInfo.SetValue(obj, val, null);
      Debug.Log("Successfully set " + property + " for " + obj.GetType().ToString() + " as " + pInfo.GetValue(obj, null).ToString());
    }
  }

  private static void CreateEventWatcher(string line)
  {
    EventWatchObj eWatch = new EventWatchObj();
    eWatch.GlobalEvent = line.GetWord(0, " ");

    List<KeyValuePair<string, string>> values = BreakLine(line);
    foreach (KeyValuePair<string, string> pair in values)
    {
      SetProperty(eWatch, pair.Key, pair.Value);
    }
    CurrentPhase.AddEventWatcher(eWatch);
  }

  private static Conditional CreateConditional(string line)
  {
    Conditional condition = new Conditional();

    List<KeyValuePair<string, string>> inputs = BreakLine(line);
    foreach(KeyValuePair<string, string> pair in inputs)
    {
      // Special case scenario where we're intentionally casting Value as a string
      // in the event that it's the name of a variable.
      if (pair.Key == PPS.PP_PARAM_VALUE)
      {
        SetProperty(condition, pair.Key, pair.Value, "string");
      }
      else
      {
        SetProperty(condition, pair.Key, pair.Value);
      }
    }

    return condition;
  }

  private static void CreateCustomEvent(ref StreamReader fileReader, string name, int preReqs=0)
  {
    Debug.Log("Creating Custom Event: " + name);
    PhaseEvent pEvent = new PhaseEvent();
    pEvent.Name = name;

    // Get any prerequisites if they are present.
    while (preReqs > 0)
    {
      string req = fileReader.ReadLine();
      if(req.GetWord(0, " ") != PPS.PP_EVENT_MATH_CONDITION)
      {
        Debug.LogError("Found Actions before listed number of PreRequisites were added. Event Name: " + name);
        Debug.Break();
      }
      pEvent.AddConditional(CreateConditional(req));
      --preReqs;
    }

    while(true)
    {
      string line = fileReader.ReadLine();
      line = line.Trim();
      ++CurrentLine;
      Debug.Log("Line #" + CurrentLine);
      if (line.GetWord(0, " ") == PPS.PP_CUSTOM_EVENT_CLOSE)
      {
        Debug.Log("End of " + name + " event");
        break;
      }

      if (line.GetWord(0, " ") == PPS.PP_EVENT_MATH_CONDITION)
      {
        Debug.LogError("Found a condition where one should not be. Event Name: " + name);
        Debug.Break();
      }

      PhaseEventStep eStep = new PhaseEventStep();
      eStep.Action = line.GetWord(0, " ");
      Debug.Log("Set Action to " + eStep.Action);

      List<KeyValuePair<string, string>> values = BreakLine(line);
      foreach (KeyValuePair<string, string> pair in values)
      {
        SetProperty(eStep, pair.Key, pair.Value);
      }
      pEvent.AddStep(eStep);
    }
    CurrentPhase.AddPhaseEvent(pEvent);
  }

  private static void CreateObject(string line)
  {
    Type type;
    switch (line.GetWord(0, " "))
    {
      case PPS.PP_OBJECT_VARIABLE:
        type = typeof(Variable);
        break;
      case PPS.PP_OBJECT_TIMER:
        type = typeof(Timer);
        break;
      case PPS.PP_OBJECT_SOUND:
        type = typeof(SoundObj);
        break;
      default:
        // We have to include this for the compiler only, it's impossible to reach.
        type = typeof(object);
        break;
    }

    object obj = Activator.CreateInstance(type);
    List<KeyValuePair<string, string>> inputs = BreakLine(line);
    foreach (KeyValuePair<string, string> pair in inputs)
    {
      if (pair.Key == PPS.PP_PARAM_PATH)
      {
        ((SoundObj)obj).Sound = Resources.LoadAssetAtPath(pair.Value, typeof(AudioClip)) as AudioClip;
      }
      else
      {
        SetProperty(obj, pair.Key, pair.Value);
      }
    }

    switch (line.GetWord(0, " "))
    {
      case PPS.PP_OBJECT_VARIABLE:
        CurrentPhase.AddVariable((Variable)obj);
        break;
      case PPS.PP_OBJECT_TIMER:
        CurrentPhase.AddTimer((Timer)obj);
        break;
      case PPS.PP_OBJECT_SOUND:
        CurrentPhase.AddSound((SoundObj)obj);
        break;
    }
  }
}
