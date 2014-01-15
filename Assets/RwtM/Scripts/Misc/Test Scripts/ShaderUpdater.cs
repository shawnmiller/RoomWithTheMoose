using UnityEngine;

public class ShaderUpdater : MonoBehaviour
{
  ShaderManager manager;

  void Start()
  {
    manager = ShaderManager.Get();
  }

  void Update()
  {
    Vector4 position = manager.GetPosition();
    renderer.material.SetVector("_TempTouch", position);
  }
}