using UnityEngine;

public class LightFader : MonoBehaviour
{
  public float MinIntensity;
  public float DegradeSpeed;

  void Update()
  {
    light.intensity -= DegradeSpeed * Time.deltaTime;
    light.intensity = Mathf.Max(light.intensity, MinIntensity);
  }
}