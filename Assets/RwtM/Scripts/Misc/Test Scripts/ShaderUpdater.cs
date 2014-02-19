using UnityEngine;

public class ShaderUpdater : MonoBehaviour
{
  public static Material shaderMat;
  ShaderManager manager;

  void Start()
  {
    manager = ShaderManager.Get();
    if (shaderMat == null)
    {
      shaderMat = Resources.LoadAssetAtPath("Assets/RwtM/Shaders/TheFeelsMat.mat", typeof(Material)) as Material;
    }
    renderer.material = shaderMat;
  }

  void Update()
  {
    Vector4 position = manager.GetPosition();
    renderer.material.SetVector("_TempTouch", position);
  }
}