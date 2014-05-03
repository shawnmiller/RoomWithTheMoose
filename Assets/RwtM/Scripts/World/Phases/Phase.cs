using UnityEngine;
using System.Collections.Generic;

public class Phase
{
  private List<Conditional> Conditionals = new List<Conditional>();
  private List<Variable> Variables = new List<Variable>();
  private List<PhaseEvent> Events = new List<PhaseEvent>();
  private List<SoundObj> Sounds = new List<SoundObj>();
  private List<Timer> Timers = new List<Timer>();

  private List<EventWatchObj> EventWatchList = new List<EventWatchObj>();

  public void Init()
  {
    ObjectController controller = ObjectController.Get();
    foreach (Variable var in Variables)
    {
      controller.AddObject(ObjectCategories.Variable, var);
    }
    foreach (SoundObj sound in Sounds)
    {
      Debug.Log("Pushing " + sound.Name + " to ObjectController.");
      controller.AddObject(ObjectCategories.Sound, sound);
    }
    foreach (Timer timer in Timers)
    {
      TimerManager.Get().RegisterTimer(timer);
    }
    Variables.Clear();
    Sounds.Clear();
    Timers.Clear();
  }

  public void Run()
  {
    foreach (Conditional c in Conditionals)
    {
      //Debug.Log("Name: " + c.Name);
      if (c.ConditionMet())
      {
        Debug.Log("Condition Met");
        RunPhaseEvent(c.Action);
      }
      else
      {
        //Debug.Log("Condition Not Met");
      }
    }
  }

  public void PushGlobalEvent(string eventName, string objectName)
  {
    //List<EventWatchObj> validEvents = EventWatchList.FindAll(x => x.GlobalEvent == eventName && x.Name == objectName);
    foreach (EventWatchObj e in EventWatchList)
    {
      try
      {
        if (e.GlobalEvent.Equals(eventName, System.StringComparison.InvariantCultureIgnoreCase) && e.Name.Equals(objectName, System.StringComparison.InvariantCultureIgnoreCase))
        {
          Debug.Log("Event Found: " + eventName + " For Instigator: " + e.Name + " Action: " + e.Action);
          RunPhaseEvent(e.Action);
        }
      }
      catch { } // Special case will occur here

      // Special case for OnBeginPhase
      if (eventName.Equals(PP.EVENT_BEGIN_PHASE, System.StringComparison.InvariantCultureIgnoreCase) && e.GlobalEvent.Equals(eventName, System.StringComparison.InvariantCultureIgnoreCase))
      {
        Debug.Log("OnBeginPhase event was pushed, executing " + e.Action);
        RunPhaseEvent(e.Action);
      }
    }
  }

  private void RunPhaseEvent(string eventName)
  {
    PhaseEvent evnt = Events.Find(x => x.Name == eventName);
    if (evnt != null)
    {
      Debug.Log("Beginning Phase Event");
      evnt.Run();
    }
    else
    {
      Debug.LogError("Failed to find PhaseEvent \"" + eventName + "\" in World Phase " + (PhaseManager.Get().GetCurrentPhaseIndex()+1));
    }
  }



  // Initialization Code
  public void AddPhaseEvent(PhaseEvent pEvent)
  {
    Events.Add(pEvent);
  }

  public void AddSound(SoundObj sound)
  {
    Debug.Log("Adding: " + sound.Name);
    Sounds.Add(sound);
  }

  public void AddVariable(Variable var)
  {
    Variables.Add(var);
  }

  public void AddConditional(Conditional cond)
  {
    Conditionals.Add(cond);
  }

  public void AddEventWatcher(EventWatchObj watch)
  {
    EventWatchList.Add(watch);
  }

  public void AddTimer(Timer timer)
  {
    Timers.Add(timer);
  }
}
