using UnityEngine;
using System.Collections.Generic;

public class TimerManager : Singleton<TimerManager>
{
  private GameState _state;
  private List<Timer> Timers = new List<Timer>();

  void Start()
  {
    _state = GameState.Get();
  }

  void FixedUpdate()
  {
    if (_state.State != StateType.In_Game)
    {
      return;
    }

    foreach (Timer t in Timers)
    {
      Debug.Log("Timer: " + t.Name + "  Duration: " + t.Duration + "  Remaining: " + t.RemainingTime);
      if (t.Update(Time.fixedDeltaTime))
      {
        Debug.Log(t.Name + " Completed");
        ReportCompletedTimer(t);
        if (t.Obsolete)
        {
          Debug.Log(t.Name + " Obsolete");
          Timers.Remove(t);
        }
      }
    }
  }

  public void CreateTimer(string name, float duration, int repeat, bool autoStart)
  {
    Timer newTimer = new Timer();
    newTimer.Name = name;
    newTimer.Duration = duration;
    newTimer.RepeatCount = repeat;
    newTimer.IsRunning = autoStart;
    if (repeat == -1)
    {
      newTimer.RepeatMode = TimerRepeatType.Loop;
    }
    else if (repeat == 1)
    {
      newTimer.RepeatMode = TimerRepeatType.Single;
    }
    else
    {
      newTimer.RepeatMode = TimerRepeatType.Count;
    }

    Timers.Add(newTimer);
  }

  public void RegisterTimer(Timer timer)
  {
    Timers.Add(timer);
  }

  public void RemoveTimer(string name)
  {
    foreach (Timer t in Timers)
    {
      if (t.Name == name)
      {
        Timers.Remove(t);
        break;
      }
    }
  }

  public void StartTimer(string name)
  {
    foreach (Timer t in Timers)
    {
      if (t.Name == name)
      {
        t.IsRunning = true;
        break;
      }
    }
  }

  public void PushGlobalEvent(string Event, string Instigator)
  {
    if (Event == PPS.PP_EVENT_BEGIN_PHASE)
    {
      PurgeTimers();
    }
  }

  private void PurgeTimers()
  {
    foreach (Timer timer in Timers)
    {
      if (!timer.Global)
      {
        Timers.Remove(timer);
      }
    }
  }

  private void ReportCompletedTimer(Timer timer)
  {
    PhaseManager.Get().PushGlobalEvent(PPS.PP_EVENT_TIMER_COMPLETED, timer.Name);
  }
}