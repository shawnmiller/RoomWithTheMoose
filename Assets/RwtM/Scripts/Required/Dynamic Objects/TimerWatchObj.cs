public class TimerWatchObj : DynamicObject
{
  public float Duration { get; set; }
  public int RepeatCount { get; set; }
  public bool AutoStart { get; set; }
  public string Action { get; set; }
}