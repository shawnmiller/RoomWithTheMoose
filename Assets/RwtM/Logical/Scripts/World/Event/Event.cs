using UnityEngine;
using System.Collections.Generic;

public class Event : MonoBehaviour
{
  private bool running;
  private Queue<EventStep> eventSteps;
  private bool processing;

  public bool Processing
  {
    set { processing = value; }
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

  public void Init(int eventID)
  {
    eventSteps = new Queue<EventStep>();
  }

  void BeginNext()
  {
    EventStep currentStep = eventSteps.Dequeue();
    StartCoroutine(currentStep.Run(this));

    if (eventSteps.Peek() != null)
    {
      BeginNext();
    }
  }
}