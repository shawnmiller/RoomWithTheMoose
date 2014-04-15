using UnityEngine;
using System.Collections.Generic;

public class PhaseEvent
{
  public string Name { get; set; }
  public void BeginEvent() { }
  private List<Conditional> PlayConditions = new List<Conditional>();
  private Queue<PhaseEventStep> Steps = new Queue<PhaseEventStep>();

  public void Run()
  {
    foreach (Conditional condition in PlayConditions)
    {
      if (!condition.ConditionMet())
        Debug.Log("Condition was not met for PhaseEvent: " + Name);
        return;
    }

    Queue<PhaseEventStep> TempQueue = new Queue<PhaseEventStep>(Steps);
    Debug.Log("Steps for PhaseEvent \"" + Name + "\": " + Steps.Count);
    while (TempQueue.Count > 0)
    {
      PhaseEventStep step = TempQueue.Dequeue();
      step.Run();
    }
  }

  public void AddConditional(Conditional condition)
  {
    PlayConditions.Add(condition);
  }

  public void AddStep(PhaseEventStep step)
  {
    Steps.Enqueue(step);
  }
}