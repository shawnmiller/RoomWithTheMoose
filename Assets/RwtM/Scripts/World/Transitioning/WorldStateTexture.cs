using UnityEngine;

public class WorldStateTexture : GameComponent
{
  private WorldState world;
  private Material local;

  void Start()
  {
    world = WorldState.Get();
    local = renderer.material;
  }

  void LateUpdate()
  {
    local.SetFloat("_BlendPos", world.position);
  }
}