using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public static class EventStepDataContext
{
  private static MultiDictionary<EventType, string> context;

  public static List<string> GetContext(EventType typeContext)
  {
    if (context == null)
    {
      context = new MultiDictionary<EventType, string>();
    }

    //TESTING PURPOSES
    //context.Remove(typeContext);

    List<string> temp;

    if (context.HasKey(typeContext))
    {
      temp = new List<string>(context.GetValues(typeContext));
    }
    else
    {
      ReadContextFromFile(typeContext);
      temp = new List<string>(context.GetValues(typeContext));
    }

    return temp;
  }

  private static void ReadContextFromFile(EventType typeContext)
  {
    //Debug.Log(Application.dataPath);
    //TextAsset file = Resources.LoadAssetAtPath("Assets/Resources/Editor/EventContext.xml", typeof(TextAsset)) as TextAsset;
    //Debug.Log("File null: " + (file == null));
    XmlDocument doc = new XmlDocument();
    //doc.LoadXml(file.text);
    doc.Load(Application.dataPath + "/Resources/Editor/EventContext.xml");
    XmlNode node = doc.DocumentElement;
    node = node.SelectSingleNode(".//" + typeContext.ToString());
    //node.GetElementsByTagName(typeContext.ToString())[0];

    foreach (XmlNode child in node.ChildNodes)
    {
      Debug.Log("Adding " + child.InnerText + " to " + typeContext + " context.");
      context.Add(typeContext, child.InnerText);
    }
  }
}