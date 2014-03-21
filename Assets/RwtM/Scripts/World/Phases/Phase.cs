using UnityEngine;
using System.Collections.Generic;

public class Phase
{
  private List<Conditional> Conditionals = new List<Conditional>();
  private List<Variable> Variables = new List<Variable>();
  private List<PhaseEvent> Events = new List<PhaseEvent>();

  public struct EventWatchObj
  {
    public string GlobalEvent;
    public string Object;
    public string PhaseEvent;
  }

  private List<EventWatchObj> EventWatchList = new List<EventWatchObj>();

  public void Run()
  {
    foreach (Conditional c in Conditionals)
    {
      if (c.ConditionMet())
      {
        RunPhaseEvent(c.Action);
      }
    }
  }

  public void PushGlobalEvent(string eventName, string objectName)
  {
    foreach (EventWatchObj e in EventWatchList)
    {
      if (e.GlobalEvent == eventName && e.Object == objectName)
      {
        RunPhaseEvent(e.PhaseEvent);
      }
    }
  }

  private void RunPhaseEvent(string eventName)
  {
    PhaseEvent evnt = Events.Find(x => x.Name == eventName);
    if (evnt != null)
    {
      evnt.BeginEvent();
    }
    else
    {
      Debug.LogError("Failed to find PhaseEvent \"" + eventName + "\" in World Phase " + (PhaseManager.Get().GetCurrentPhaseIndex()+1));
    }
  }
}