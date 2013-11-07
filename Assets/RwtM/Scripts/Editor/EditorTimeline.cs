using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditorTimeline
{
  private const bool DEBUG = true;

  private Rect drawArea;
  private Vector2 overflow;
  private Texture2D bg;
  private Vector2 scroll;

  private float time;
  private float timescale;
  private float farRight;

  private float barHeight;

  //private List<TimelineBar> bars = new List<TimelineBar>();
  private List<EventStepData> cachedData = new List<EventStepData>();
  
  public EditorTimeline(Rect screenArea, float time, int barHeight)
  {
    this.drawArea = screenArea;
    this.overflow = new Vector2(screenArea.width, screenArea.height);
    this.time = time;
    this.timescale = 1f;
    this.barHeight = barHeight;

    //this.bg = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/null_1px.png", typeof(Texture2D)) as Texture2D;
    this.bg = new Texture2D(1, 1, TextureFormat.RGBA4444, true);
    this.bg.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.5f));
    this.bg.Apply();

    /*if (DEBUG)
    {
      Debug.Log("Debugging Timeline.");
      AddBar(0);
      AddBar(1);
    }*/
  }

  public void ResizeGrid(Rect newSize)
  {
    drawArea = newSize;
  }

  public void ChangeTime(float newTime)
  {
    time = newTime;
  }

  private void ManageData(EventStepData data)
  {
    if (!cachedData.Contains(data))
    {
      cachedData.Add(data);
    }

    EvaluateTime();
  }

  private void EvaluateTime()
  {
    float newTime = 0;

    foreach (EventStepData data in cachedData)
    {
      if (data.startTime + data.duration > newTime)
      {
        newTime = data.startTime + data.duration;
      }
    }

    this.time = newTime;

    ResizeOverflow();
  }

  private void ResizeOverflow()
  {
    //TimelineHelper.GetOverflowSize();
  }

  public void Draw()
  {
    GUI.depth = 100;
    GUI.DrawTexture(drawArea, bg);

    GUI.depth = 1;
    GUILayout.BeginArea(drawArea);
      

    GUILayout.EndArea();
  }

  /*public TimelineBar AddBar(int index)
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

    scroll = GUI.BeginScrollView(t, scroll,
                                 new Rect(0,0, drawArea.width * 2, drawArea.height-64));
        foreach (TimelineBar bar in bars)
        {
          bar.Draw();
        }
    GUI.EndScrollView();
  }*/
}