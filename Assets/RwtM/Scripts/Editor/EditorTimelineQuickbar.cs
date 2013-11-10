using UnityEngine;
using UnityEditor;

public class EditorTimelineQuickbar
{
  public static Texture2D texture;

  public static void Draw(Rect drawAt, EventStepData data)
  {
    GetTexture();

    GUIStyle style = new GUIStyle();
    style.normal.background = texture;
    style.alignment = TextAnchor.MiddleCenter;
    style.normal.textColor = Color.white;

    GUILayout.BeginArea(drawAt);
    if (GUILayout.Button(data.type.ToString(), style, GUILayout.Width(drawAt.width), GUILayout.Height(drawAt.height)))
    {
      
    }
    GUILayout.EndArea();
  }

  private static void GetTexture()
  {
    if (texture != null)
    {
      return;
    }

    texture = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/bartexture_1px.png", typeof(Texture2D)) as Texture2D;
  }
}