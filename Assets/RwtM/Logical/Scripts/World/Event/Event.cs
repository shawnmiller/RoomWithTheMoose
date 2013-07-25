using UnityEngine;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;

public class Event : MonoBehaviour
{
  [SerializeField]
  private int eventID;

  private bool running;
  private Queue<EventStepData> eventData;
  private bool processing;

  private XmlNode valueTypesRoot;

  public bool Processing
  {
    set { processing = value; }
  }

  void Start()
  {
    eventData = new Queue<EventStepData>();
    ReadFromFile();
  }

  void Update()
  {
    if (!running)
    {
      return;
    }

    if (!processing)
    {
      BeginNext();
    }
  }

  void BeginNext()
  {
    EventStepData currentData = eventData.Dequeue();
    EventStep newStep = gameObject.AddComponent<EventStep>();
    newStep.Begin(this, currentData);
  }

  void ReadFromFile()
  {
    TextAsset asset = new TextAsset();
    asset = (TextAsset)Resources.Load(eventID + ".xml", typeof(TextAsset));
    //XmlTextReader reader = new XmlTextReader(asset.text);
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(asset.text);
    XmlNode root = doc.DocumentElement;
    XmlNodeList nodeList = root.ChildNodes;

    for (int i = 0; i < nodeList.Count; ++i)
    {
      eventData.Enqueue(ParseEventStep(nodeList[i]));
    }
  }

  EventStepData ParseEventStep(XmlNode stepNode)
  {
    EventStepData data = new EventStepData();

    StepDataRequirements dataReqs = StepDataRequirements.Get();
    string[] reqValues = dataReqs.GetRequiredValues(stepNode.LocalName);

    for (int i = 0; i < reqValues.Length; ++i)
    {
      // Get the first occurence of the node we're looking for
      // This only works because there will only ever be a single occurance of each element for a given EventStep
      XmlNode current = ((XmlElement)stepNode).GetElementsByTagName(reqValues[i])[0];

      switch (reqValues[i])
      {
        case "exclusive":

          break;
        case "position":

          break;
        case "rotation":

          break;
        case "duration":

          break;
        case "sceneid":
        case "eventid":

          break;
        case "prefab":

          break;
        case "soundname":

          break;
        case "animation":

          break;
      }
    }

    return null;
  }

  Vector3 CompositeVector3(XmlNode toVector3)
  {
    Vector3 temp = new Vector3();

    // This looks like a god damn jungle but here's what it is:
    // Parse the first element of the toVector3 node with the given x/y/z value into a float
    temp.x = System.Single.Parse(((XmlElement)toVector3).GetElementsByTagName("x")[0].Value);
    temp.y = System.Single.Parse(((XmlElement)toVector3).GetElementsByTagName("y")[0].Value);
    temp.z = System.Single.Parse(((XmlElement)toVector3).GetElementsByTagName("z")[0].Value);

    return temp;
  }
}