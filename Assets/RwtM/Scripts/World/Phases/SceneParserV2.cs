using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class SceneParser
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

    StreamWriter newFile = new StreamWriter(Application.dataPath + @"\ParseFix.txt");
    newFile.Write(temp);
    newFile.Close();

    StreamReader fileReader = new StreamReader(Application.dataPath + @"\ParseFix.txt");
    
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
      if (line.TrimStart().Substring(0, 2) == PP.COMMENT_LINE)
      {
        Debug.Log("Skipping: Comment Line");
        continue;
      }
      if (line == PP.PHASE_CLOSE)
      {
        Debug.Log("Phase Ended");
        Manager.AddPhase(CurrentPhase);
        CurrentPhase = null;
      }
      else
      {
        switch (line.GetWord(0, " ")) 
        {
          case PP.PHASE_OPEN:
          case PP.GLOBAL_OPEN:
            Debug.Log("Beginning New Phase");
            CurrentPhase = new Phase();
            break;
          case PP.GLOBAL_CLOSE:
            Manager.AddGlobalPhase(CurrentPhase);
            CurrentPhase = null;
            break;
          case PP.CUSTOM_EVENT_OPEN:
            string eName = line.GetWord(1, " ");
            eName = eName.SplitTextDelimited("=")[1];

            // Since Events can have prerequisite 
            string preReqCheck = line.GetLastWord(" ");
            Debug.Log("PreReqs for " + eName + ": " + preReqCheck);
            if (preReqCheck.SplitTextDelimited("=")[0] == PP.PARAM_EVENT_REQ)
            {
              Debug.Log("PreReqs found");
              CreateCustomEvent(ref fileReader, eName, Int32.Parse(preReqCheck.SplitTextDelimited("=")[1]));
            }
            else
            {
              Debug.Log("No PreReqs found, defaulting.");
              CreateCustomEvent(ref fileReader, eName);
            }

            break;
          case PP.EVENT_BEGIN_PHASE:
          case PP.EVENT_ENTER_TRIGGER:
          case PP.EVENT_ITEM_PICKUP:
          case PP.EVENT_TIMER_COMPLETED:
            Debug.Log("Creating Event Watcher");
            CreateEventWatcher(line);
            break;
          case PP.EVENT_MATH_CONDITION:
            CurrentPhase.AddConditional(CreateConditional(line));
            break;
          case PP.OBJECT_TIMER:
          case PP.OBJECT_VARIABLE:
          case PP.OBJECT_SOUND:
            Debug.Log("Creating Object");
            CreateObject(line);
            break;
          case PP.PHASE_CLOSE:
            break;
          default:
            Debug.Log("Couldn't parse line: " + line);
            break;
        }
      }
    }

    fileReader.Close();
    File.Delete(Application.dataPath + @"\ParseFix.txt");
    Debug.Log("Parse Successful");
  }

  // Note that this does not return the first value, which is always assumed to be the identifier.
  private static List<KeyValuePair<string, string>> BreakLine(string line)
  {
    List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
    string[] inputs = line.SplitTextDelimited(" ");
    Debug.Log("Found " + inputs.Length + " entries.");
    for (int i = 1; i < inputs.Length; ++i) 
    {
      string[] inputSplit = inputs[i].SplitTextDelimited("=");
      KeyValuePair<string, string> pair = new KeyValuePair<string, string>(inputSplit[CMD], inputSplit[VAL]);
      pairs.Add(pair);

      Debug.Log("Added pair: " + pair.Key + " -> " + pair.Value);
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

    PropertyInfo pInfo = obj.GetType().GetProperty(PP.GetParameterLiteralName(property));

    if (pInfo == null)
    {
      Debug.LogError(property + " was not found in type " + obj.GetType().ToString());
    }
    else
    {
      object val;
      if (typeOverride == "NullType")
      {
        val = TypeConversion.Convert(PP.GetParameterType(property), value);
      }
      else
      {
        val = TypeConversion.Convert(Type.GetType(typeOverride), value);
      }

      pInfo.SetValue(obj, val, null);
      try
      {
        Debug.Log("Successfully set " + property + " for " + obj.GetType().ToString() + " as " + pInfo.GetValue(obj, null).ToString());
      }
      catch { Debug.LogWarning("If not \"Path\", report to programmer: " + pInfo.Name); } // SoundObjs should be the only thing to cause this
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
      if (pair.Key == PP.PARAM_VALUE)
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
    Debug.Log("PreReq Count: " + preReqs);
    PhaseEvent pEvent = new PhaseEvent();
    pEvent.Name = name;

    // Get any prerequisites if they are present.
    while (preReqs > 0)
    {
      ++CurrentLine;
      Debug.Log("Line #" + CurrentLine);
      string req = fileReader.ReadLine();

      if (String.IsNullOrEmpty(req) || req.Trim().Length == 0)
      {
        Debug.Log("Skipping: Empty Line");
        continue;
      }
      if(req.GetWord(0, " ") != PP.EVENT_MATH_CONDITION)
      {
        Debug.LogError("Found Actions before listed number of Prerequisites were added. Event Name: " + name);
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
      if (String.IsNullOrEmpty(line) || line.Trim().Length == 0)
      {
        Debug.Log("Skipping: Empty Line");
        continue;
      }

      if (line.GetWord(0, " ") == PP.CUSTOM_EVENT_CLOSE)
      {
        Debug.Log("End of " + name + " event");
        break;
      }

      if (line.GetWord(0, " ") == PP.EVENT_MATH_CONDITION)
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
      case PP.OBJECT_VARIABLE:
        type = typeof(Variable);
        break;
      case PP.OBJECT_TIMER:
        type = typeof(Timer);
        break;
      case PP.OBJECT_SOUND:
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
      /*if (pair.Key == PP.PARAM_PATH)
      {
        ((SoundObj)obj).Sound = Resources.LoadAssetAtPath(pair.Value, typeof(AudioClip)) as AudioClip;
      }
      else
      {*/
        Debug.Log("Setting: " + pair.Key + " -> " + pair.Value);
        SetProperty(obj, pair.Key, pair.Value);
      //}
    }

    switch (line.GetWord(0, " "))
    {
      case PP.OBJECT_VARIABLE:
        CurrentPhase.AddVariable((Variable)obj);
        break;
      case PP.OBJECT_TIMER:
        CurrentPhase.AddTimer((Timer)obj);
        break;
      case PP.OBJECT_SOUND:
        CurrentPhase.AddSound((SoundObj)obj);
        break;
    }
  }
}
