using UnityEngine;

public class Timer : DynamicObject
{
  public string Name { get; set; }
  public float Duration { get; set; }
  private float ElapsedTime { get; set; }
  public float RemainingTime { get { return Duration - ElapsedTime; } }
  public bool IsComplete { get { return ElapsedTime >= Duration; } }
  public bool IsRunning { get; set; }
  public bool Obsolete { 
    get {
      switch (RepeatMode)
      {
        case TimerRepeatType.Count:
        case TimerRepeatType.Single:
          return RepeatCount == 0;
        case TimerRepeatType.Loop:
          return false;
        default:
          return true;
      }
    }
  }
  public TimerRepeatType RepeatMode { get; set; }
  public int RepeatCount { get; set; }

  public Timer()
  {
    ElapsedTime = 0;
  }

  public bool Update(float deltaTime)
  {
    if (!IsRunning)
    {
      return false;
    }

    ElapsedTime += deltaTime;
    bool completed = IsComplete;
    if (completed)
    {
      ElapsedTime = 0 - RemainingTime; // RemainingTime will be negative
    }

    /*if (completed)
    {
      RepeatCount -= 1;
      switch (RepeatMode)
      {
        case TimerRepeatType.Single:
          IsRunning = false;
          break;
        case TimerRepeatType.Loop:
          if (RepeatCount == 0)
          {
            IsRunning = false;
          }
          break;
        case TimerRepeatType.Count:
        default: break;
      }
    }*/
    return completed;
  }
}