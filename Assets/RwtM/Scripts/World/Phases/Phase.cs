﻿using UnityEngine;
using System.Collections.Generic;

public class Phase
{
  private List<Conditional> Conditionals = new List<Conditional>();
  private List<Variable> Variables = new List<Variable>();
  private List<PhaseEvent> Events = new List<PhaseEvent>();
  private List<SoundObj> Sounds = new List<SoundObj>();
  
  public struct EventWatchObj
  {
    public string GlobalEvent;
    public string Object;
    public string PhaseEvent;
  }
  public struct SoundObj
  {
    public string Name;
    public AudioClip Sound;
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



  // Initialization Code
  public void AddPhaseEvent(PhaseEvent pEvent)
  {
    Events.Add(pEvent);
  }

  public void AddSound(SoundObj sound)
  {
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
}