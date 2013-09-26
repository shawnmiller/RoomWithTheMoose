using UnityEngine;

public class WorldState : Singleton<WorldState>
{
  public float position = 0f;

  void FixedUpdate()
  {
    Clamp();
  }

  void Clamp()
  {
    if (position < 0f)
    {
      position = 0f;
      return;
    }
    if (position > 1f)
    {
      position = 1f;
    }
  }
}