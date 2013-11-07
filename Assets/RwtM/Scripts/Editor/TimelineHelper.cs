using UnityEngine;

public static class TimelineHelper
{
  public static const int SCROLLBAR_SIZE = 30;

  public static int GetOverflowSize(float currentTime, float newTime, float timeScale, Rect timelineArea)
  {
    //return Mathf.FloorToInt(timelineArea.width * 1 + timeScale);
    //int cPixels = Mathf.FloorToInt(timeScale * timelineArea.width);
    //return Mathf.FloorToInt((cPixels * newTime) / currentTime) + SCROLLBAR_SIZE;
  }

  public static int ConvertTimeToOverflowPixels(float timeToConvert, int overflowSize, float timelineTime)
  {
    return Mathf.FloorToInt(overflowSize * (timeToConvert/timelineTime));
  }
}