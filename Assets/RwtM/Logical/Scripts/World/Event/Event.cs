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
    throw new System.NotImplementedException();
  }
}