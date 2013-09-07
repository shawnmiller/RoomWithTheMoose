using UnityEngine;
using Easing;

public class Ease
{
  private EasingType _type;
  private float _startTime;
  private float _duration;
  private object _value;
  private string _mask;

  public Ease(EasingType
    type, float duration, object toEase)
  {
    _type = type;
    _startTime = Time.time;
    _value = toEase;
    _mask = "";
  }

  public Ease(EasingType type, float duration, object toEase, string mask)
  {
    _type = type;
    _startTime = Time.time;
    _value = toEase;
    _mask = mask;
  }

  public object GetCurrent()
  {
    Debug.LogError("Ease.GetCurrent() is NYI");
    return _value;
  }
}
