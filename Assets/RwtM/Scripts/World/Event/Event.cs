using UnityEngine;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;

public class Event : StateComponent
{
  public int eventID;
  public Queue<EventStepData> eventData = new Queue<EventStepData>();

  private bool running;
  private int activeSteps;
  //private bool waiting;
  private EventStepData next;

  //GameState state;
  TimeKeeper keeper;

  void Start()
  {
    //state = GameState.Get();
    base.Start();
    next = eventData.Dequeue();
    keeper = new TimeKeeper(TimeKeeper.KeeperMode.FixedUpdate);
  }

  void Update()
  {
    if (!running)
    {
      return;
    }
  }

  void FixedUpdate()
  {
    if (!running || _state.State != StateType.In_Game) { return; }
    if (eventData.Count == 0) { return; }

    keeper.Tick();

    while (next != null && ShouldRunNext())
    {
      PlayStep();
    }
  }

  bool ShouldRunNext()
  {
    if (next != null && keeper.GetTime() >= next.startTime)
    {
      return true;
    }
    return false;
  }

  void PlayStep()
  {
    EventStep newStep = gameObject.AddComponent<EventStep>();
    newStep.Begin(this, next);

    // Prep the next event
    next = eventData.Dequeue();
  }

  public void Activate()
  {
    running = true;
  }

  public void StepDone()
  {
    --activeSteps;

    if (eventData.Count == 0 && activeSteps == 0)
    {
      EventManager manager = EventManager.Get();
      if (manager != null)
      {

      }
    }
  }

  // Moved to PlayStep()
  /*void BeginNext()
  {
    EventStepData currentData = eventData.Dequeue();
    EventStep newStep = gameObject.AddComponent<EventStep>();
    newStep.Begin(this, currentData);
  }*/

  /*void ReadFromFile()
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
          data.exclusive = Boolean.Parse(current.Value);
          break;
        case "position":
          data.position = CompositeVector3(current);
          break;
        case "rotation":
          data.rotation = Quaternion.Euler(CompositeVector3(current));
          break;
        case "duration":
          data.duration = Single.Parse(current.Value);
          break;
        case "sceneid":
        case "eventid":
          data.associatedID = Int32.Parse(current.Value);
          break;
        case "prefab":
          data.prefab = Resources.Load("prefabs/" + current.Value, typeof(GameObject)) as GameObject;
          break;
        case "soundname":
          data.sound = Resources.Load("sounds/" + current.Value, typeof(AudioClip)) as AudioClip;
          break;
        case "animation":
          data.animation = current.Value;
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
  }*/
}