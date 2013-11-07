using UnityEngine;

public static class TimelineHelper
{
  public const int SCROLLBAR_SIZE = 30;

  public static Vector2 GetOverflowSize(float timeScale, Rect timelineArea)
  {
    return new Vector2(Mathf.FloorToInt(timelineArea.width * timeScale), timelineArea.height);
  }

  public static int ConvertTimeToOverflowPixels(float timeToConvert, int overflowSize, float timelineTime)
  {
    return Mathf.FloorToInt(overflowSize * (timeToConvert/timelineTime));
  }
}