using UnityEngine;
using UnityEditor;

public class TimelineBar
{
  private string text = "Sample Text";
  private Rect drawArea;
  private Rect barSize;
  private static Texture2D texture;

  public EditorTimeline parent;

  public TimelineBar(Rect drawArea, Rect barSize)
  {
    this.drawArea = drawArea;
    this.barSize = barSize;

    if (texture == null)
    {
      texture = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/bartexture_1px.png", typeof(Texture2D)) as Texture2D;
    }
  }

  public void ChangeBarDrawArea(Rect newArea)
  {
    this.drawArea = newArea;
  }

  public void Draw()
  {
    GUIStyle style = new GUIStyle();
    style.normal.background = texture;
    style.alignment = TextAnchor.MiddleCenter;
    style.normal.textColor = Color.white;

    GUILayout.BeginArea(drawArea);
    GUILayout.BeginArea(barSize);

    if (GUILayout.Button(text, style, GUILayout.Width(barSize.width), GUILayout.Height(barSize.height)))
    {
      Debug.Log("ButtonPressed");
    }
    //GUILayout.Label(text, style, GUILayout.Width(barSize.width), GUILayout.Height(barSize.height));
    GUILayout.EndArea();
    GUILayout.EndArea();
  }
}