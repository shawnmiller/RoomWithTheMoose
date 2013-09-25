using UnityEngine;
using System.Collections.Generic;

public class TimeKeeper
{
  public enum KeeperMode
  {
    FixedUpdate,
    Update,
    Custom
  }

  public KeeperMode mode;

  private float timer;

  public TimeKeeper(KeeperMode mode)
  {
    this.mode = mode;
  }

  public void Tick()
  {
    if (mode == KeeperMode.Custom)
    {
      Debug.LogError("TimeKeeper is recording a custom timer, but a 0 parameter Tick() function is being called.");
      return;
    }

    if (mode == KeeperMode.FixedUpdate)
    {
      timer += Time.fixedDeltaTime;
    }
    else
    {
      timer += Time.deltaTime;
    }
  }

  public void Tick(float time)
  {
    if (mode != KeeperMode.Custom)
    {
      Debug.LogWarning("TimeKeeper was called using Tick(float) but is recording a specific time.");
    }
    timer += time;
  }

  public float GetTime()
  {
    return timer;
  }
}