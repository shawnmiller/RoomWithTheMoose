using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class StepDataRequirements
{
  private const string filePath = "/data/stepreqs.xml";
  private static readonly StepDataRequirements instance = new StepDataRequirements();

  private StepDataRequirements()
  {
    requirements = new MultiDictionary<string, string>();

    // Get resource file
    string path = Application.dataPath + filePath;
    XmlDocument doc = new XmlDocument();
    doc.Load(path);

    XmlNode root = doc.DocumentElement;

    foreach(XmlNode current in root.ChildNodes)
    {
      string key = current.LocalName;
      foreach (XmlNode val in current.ChildNodes)
      {
        requirements.Add(key, val.Value);
      }
    }
  }

  public static StepDataRequirements Get()
  {
    return instance;
  }

  public string[] GetRequiredValues(string eventType)
  {
    return requirements.GetValues(eventType);
  }

  private static MultiDictionary<string, string> requirements;
}