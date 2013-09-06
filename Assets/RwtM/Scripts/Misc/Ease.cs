using UnityEngine;

public class Ease
{
  private EaseType _type;
  private float _startTime;
  private float _duration;
  private object _value;
  private string _mask;

  public Ease(EaseType type, float duration, object toEase)
  {
    _type = type;
    _startTime = Time.time;
    _value = toEase;
    _mask = "";
  }

  public Ease(EaseType type, float duration, object toEase, string mask)
  {
    _type = type;
    _startTime = Time.time;
    _value = toEase;
    _mask = mask;
  }

  public object GetCurrent()
  {
    
  }
}
