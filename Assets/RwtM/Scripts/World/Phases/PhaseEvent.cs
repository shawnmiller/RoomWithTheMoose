using UnityEngine;
using System.Collections.Generic;

public class PhaseEvent
{
  public string Name { get; set; }
  public void BeginEvent() { }
  private Queue<PhaseEventStep> Steps = new Queue<PhaseEventStep>();

  public void Run()
  {
    while (Steps.Count > 0)
    {
      PhaseEventStep step = Steps.Dequeue();
      step.Run();
    }
  }

  public void AddStep(PhaseEventStep step)
  {
    Steps.Enqueue(step);
  }
}