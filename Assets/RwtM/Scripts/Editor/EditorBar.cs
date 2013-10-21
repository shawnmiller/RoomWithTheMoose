using UnityEngine;
using UnityEditor;

public class EditorBar
{
  private Rect drawArea;
  private Texture2D texture;
  private BarHandle[] handles;

  public EditorBar(Rect barArea, bool handles, HandleAction action)
  {
    drawArea = barArea;
    if (handles)
    {
      this.handles = new BarHandle[2] { new BarHandle(), new BarHandle() };
    }
  }

  public void Draw(float clipLeft, float clipRight)
  {

  }
}