using UnityEngine;

public class UITextureCropping : MonoBehaviour
{
  public Texture2D texture;
  public float percent = 1f;

  void OnGUI()
  {
    Graphics.DrawTexture(new Rect(0, 0, Screen.width * percent, Screen.height), texture, new Rect(0, 0, percent, 1), 0, 0, 0, 0);
    percent = GUILayout.HorizontalSlider(percent, 0f, 1f, GUILayout.Width(Screen.width), GUILayout.Height(10f));
  }
}