using UnityEngine;

public class ShaderUpdater : MonoBehaviour
{
  public static Material shaderMat;
  ShaderManager manager;

  void Start()
  {
    manager = ShaderManager.Get();
    
    renderer.material = manager.TouchMaterial;
  }

  void Update()
  {
		if(!renderer.enabled)
		{
			return;
		}

    if (GetComponent<InteractibleItem>() != null)
    {
      renderer.material.SetColor("_Color", manager.KeyItemColor);
    }
    else
    {
      renderer.material.SetColor("_Color", manager.NormalColor);
    }

    Vector4 position = manager.GetHandPosition();
    renderer.material.SetVector("_HandPosition", position);

    /*Matrix4x4 FingerPositions = manager.GetLocationMatrix(ShaderManager.FINGERS);
    renderer.material.SetMatrix("_TouchLocationsHand", FingerPositions);

    Matrix4x4 BodyPositions = manager.GetLocationMatrix(ShaderManager.BODY);
    renderer.material.SetMatrix("_TouchLocationsBody", BodyPositions);*/
    //Vector4 position = manager.GetPosition();
    //renderer.material.SetVector("_TempTouch", position);
  }
}