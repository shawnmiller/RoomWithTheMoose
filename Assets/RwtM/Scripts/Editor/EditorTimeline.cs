using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditorTimeline
{
  private const bool DEBUG = true;

  public const float MIN_TIMESCALE = 1f;
  public const float MAX_TIMESCALE = 5f;

  private Rect drawArea;
  private Vector2 overflow;
  private Vector2 scroll;
  private Texture2D bg;
  private Texture2D barTex;
  private GUIStyle barStyle;
  private GUIStyle occludedBarStyle;
  private GUIStyle labelStyle;

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

    this.bg = new Texture2D(1, 1, TextureFormat.RGBA32, true);
    this.bg.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.5f));
    this.bg.Apply();
    //this.barTex = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/bartexture_1px.png", typeof(Texture2D)) as Texture2D;

    this.barStyle = new GUIStyle();
    this.barStyle.normal.background = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/bartexture_1px.png", typeof(Texture2D)) as Texture2D;// this.barTex;
    this.barStyle.alignment = TextAnchor.MiddleCenter;
    this.barStyle.normal.textColor = Color.white;
    this.occludedBarStyle = new GUIStyle();
    this.occludedBarStyle.normal.background = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/bartexture_gradient.png", typeof(Texture2D)) as Texture2D;
    this.occludedBarStyle.alignment = TextAnchor.MiddleCenter;
    this.occludedBarStyle.normal.textColor = Color.white;

    this.labelStyle = GUI.skin.label;
    this.labelStyle.normal.textColor = Color.white;

    /*if (DEBUG)
    {
      Debug.Log("Debugging Timeline.");
      AddBar(0);
      AddBar(1);
    }*/
  }

  public void ResizeGrid(Rect newSize)
  {
    this.drawArea = newSize;
  }

  public void ChangeTime(float newTime)
  {
    this.time = newTime;
  }

  private void ManageData(EventStepData data)
  {
    if (!cachedData.Contains(data))
    {
      this.cachedData.Add(data);
    }

    EvaluateTime();
  }

  private void EvaluateTime()
  {
    float newTime = 0;

    //Debug.Log("cachedData size: " + cachedData.Count);

    foreach (EventStepData data in cachedData)
    {
      if (data.startTime + data.duration > newTime)
      {
        newTime = data.startTime + data.duration;
      }
    }

    //this.time = newTime;

    this.time = Mathf.Max(newTime, 0.1f);

    //Debug.Log(this.time);
    ResizeOverflow();
  }

  private void ResizeOverflow()
  {
    this.overflow = TimelineHelper.GetOverflowSize(timescale, drawArea);
  }

  public void GetRescale(int updown)
  {
    bool changed = false;

    if (updown > 0f)
    {
      this.timescale += 0.2f;
      changed = true;
    }

    if (updown < 0f)
    {
      this.timescale -= 0.2f;
      changed = true;
    }

    this.timescale = Mathf.Clamp(timescale, MIN_TIMESCALE, MAX_TIMESCALE);
    if (changed)
    {
      Debug.Log("Key Pressed");
      ResizeOverflow();
    }
  }

  public void DrawBackground()
  {
    GUI.depth = 100;
    GUI.DrawTexture(drawArea, bg);
  }

  public void Draw(EventStepData data, int index)
  {
    //GetRescale();
    ManageData(data);
    Vector2 barPos = new Vector2(TimelineHelper.ConvertTimeToOverflowPixels(data.startTime, (int)overflow.x, time),
                                 TimelineHelper.ConvertTimeToOverflowPixels(data.startTime + data.duration, (int)overflow.x, time));

    // Guarantee a minimum size for all zero-duration steps
    if(data.duration == 0f)
    {
      Vector2 minSize = barStyle.CalcSize(new GUIContent(data.type.ToString()));
      barPos.y = barPos.x + minSize.x + 4;
    }

    GUI.depth = 1;
    GUILayout.BeginArea(drawArea);
    //GUI.BeginScrollView(new Rect(0,0,drawArea.width,drawArea.height), scroll, new Rect(0,0,overflow.x,overflow.y));

    GUIStyle useForBar = this.barStyle;
    Rect barRect = new Rect(barPos.x, index * barHeight + index, barPos.y - barPos.x, barHeight);
    if (barRect.x + barRect.width > this.drawArea.width)
    {
      barRect.x -= barRect.width;
      useForBar = this.occludedBarStyle;
    }

    GUI.Label(barRect, data.type.ToString(), useForBar);
    if (GUI.Button(barRect, "", labelStyle))
    {
      EventStepDataWindow stepDataWindow;
      stepDataWindow = EditorWindow.GetWindow<EventStepDataWindow>();
      stepDataWindow.Edit(this, data);
    }
    
    GUIContent calc = new GUIContent(data.startTime.ToString());
    Vector2 size = GUI.skin.label.CalcSize(calc);
    GUI.Label(new Rect(barRect.x, barRect.y, size.x, size.y), calc, labelStyle);
    calc.text = (data.startTime + data.duration).ToString();
    size = GUI.skin.label.CalcSize(calc);
    GUI.Label(new Rect((barRect.x + barRect.width) - size.x, (barRect.y + barRect.height) - size.y, size.x, size.y), calc, labelStyle);

    //GUI.EndScrollView();
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