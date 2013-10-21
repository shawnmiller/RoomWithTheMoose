using UnityEngine;
using UnityEditor;

public class EditorTimeline
{
  private Rect drawArea;

  private float time;
  private float timescale;
  private float farRight;

  private float barHeight;

  /// <summary>Creates a new timeline.</summary>
  /// <param name="screenArea">The area of the window which this timeline will be drawn.</param>
  /// <param name="time">The full duration of the timeline, regardless of what is shown.</param>
  /// <param name="startTime">The far-left time value to display.</param>
  /// <param name="endTime">The far-right time value to display.</param>
  public EditorTimeline(Rect screenArea, float time, float startTime, float endTime, int barHeight)
  {
    drawArea = screenArea;
    timescale = (endTime - startTime) / time;

  }

  public void ResizeGrid(Rect newSize)
  {
    drawArea = newSize;
  }

  public void ChangeTime(float newTime)
  {
    time = newTime;
  }

  public EditorBar AddBar(int index)
  {
    EditorBar bar = new EditorBar(new Rect(), true, HandleAction.ExtentContractBar);
    return bar;
  }

  public void Draw()
  {

  }
}