using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditorTimeline
{
  private const bool debug = true;

  private Rect drawArea;
  private Texture2D bg;
  private Vector2 scroll;

  private float time;
  private float timescale;
  private float farRight;

  private float barHeight;

  private List<TimelineBar> bars = new List<TimelineBar>();
  //private List<EditorBar> bars = new List<EditorBar>();

  /// <summary>Creates a new timeline.</summary>
  /// <param name="screenArea">The area of the window which this timeline will be drawn.</param>
  /// <param name="time">The full duration of the timeline, regardless of what is shown.</param>
  /// <param name="startTime">The far-left time value to display.</param>
  /// <param name="endTime">The far-right time value to display.</param>
  public EditorTimeline(Rect screenArea, float time, float startTime, float endTime, int barHeight)
  {
    drawArea = screenArea;
    timescale = (endTime - startTime) / time;

    this.barHeight = barHeight;
    Debug.Log(this.barHeight);

    if (debug)
    {
      Debug.Log("Debugging Timeline.");
      AddBar(0);
      AddBar(1);
    }

    bg = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/null_1px.png", typeof(Texture2D)) as Texture2D;
    Color c = bg.GetPixel(0, 0);
    c.a = 0.5f;
    bg.SetPixel(0,0, c);
    bg.Apply();
  }

  public void ResizeGrid(Rect newSize)
  {
    drawArea = newSize;
  }

  public void ChangeTime(float newTime)
  {
    time = newTime;
  }

  public TimelineBar AddBar(int index)
  {
    TimelineBar bar = new TimelineBar(new Rect(0, index*barHeight, drawArea.width, barHeight),
                                    new Rect(index * 50f, 0, index * 30 + 350, barHeight));
    bar.parent = this;

    bars.Insert(index, bar);
    return bar;
  }

  public TimelineBar GetBar(int index)
  {
    return bars[index];
  }

  public void RemoveBar(int index)
  {
    bars.RemoveAt(index);
  }

  public void BarClicked(TimelineBar bar)
  {
    int index = bars.IndexOf(bar);
  }

  public void Draw()
  {
    GUI.DrawTexture(drawArea, bg);
    Rect t = drawArea;
    t.height -= 26;

    Debug.Log(drawArea);
    //GUILayout.BeginArea(t);
    scroll = GUI.BeginScrollView(t, scroll,
                                 new Rect(0,0, drawArea.width * 2, drawArea.height-64));
      //scroll = GUILayout.BeginScrollView(scroll);
        foreach (TimelineBar bar in bars)
        {
          bar.Draw();
        }
    GUI.EndScrollView();
      //GUILayout.EndScrollView();
    //GUILayout.EndArea();
  }
}