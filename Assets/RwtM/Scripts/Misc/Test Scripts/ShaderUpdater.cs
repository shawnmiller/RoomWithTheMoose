using UnityEngine;

public class ShaderUpdater : MonoBehaviour
{
  public static Material shaderMat;
  ShaderManager manager;

  void Start()
  {
    manager = ShaderManager.Get();
    /*if (shaderMat == null)
    {
      shaderMat = Resources.LoadAssetAtPath("Assets/RwtM/Shaders/TemptedToTouch.mat", typeof(Material)) as Material;
    }
    renderer.material = shaderMat;*/
    renderer.material = manager.TouchMaterial;
  }

  void Update()
  {
    Vector4 position = manager.GetPosition();
    renderer.material.SetVector("_TempTouch", position);
  }
}