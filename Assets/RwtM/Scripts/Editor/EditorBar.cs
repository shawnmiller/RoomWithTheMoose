using UnityEngine;
using UnityEditor;

public class EditorBar
{
  private Rect drawArea;
  private static Texture2D texture;
  private BarHandle[] handles;

  public EditorBar(Rect barArea, bool handles, HandleAction action)
  {
    drawArea = barArea;
    if (handles)
    {
      this.handles = new BarHandle[2] { new BarHandle(), new BarHandle() };
    }

    if (texture == null)
    {
      texture = Resources.LoadAssetAtPath("Assets/RwtM/Textures/Editor/bartexture_1px.png", typeof(Texture2D)) as Texture2D;
    }
  }

  public void Draw()
  {
    //GUI.DrawTexture(
  }

  public void Draw(float clipLeft, float clipRight)
  {

  }
}