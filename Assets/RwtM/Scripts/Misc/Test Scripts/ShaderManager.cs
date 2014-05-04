using UnityEngine;

public class ShaderManager : Singleton<ShaderManager>
{
  public Material TouchMaterial;
  public Color NormalColor;
  public Color KeyItemColor;
  Transform player;

  void Update()
  {
    if (player == null)
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }
  }

  public Vector3 GetPosition()
  {
    if (player != null)
    {
      return player.position;
    }

    return Vector3.zero;
  }
}