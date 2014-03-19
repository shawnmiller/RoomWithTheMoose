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

    bool isComplete;
    foreach (Timer t in Timers)
    {
      if (t.Update(Time.fixedDeltaTime))
      {
        ReportCompletedTimer(t);
        if (t.Obsolete)
        {
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

  void ReportCompletedTimer(Timer timer)
  {
    throw new System.NotImplementedException();
  }
}