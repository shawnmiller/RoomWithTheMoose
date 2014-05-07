using UnityEngine;

public class FootstepMarker : MonoBehaviour
{
  public float FullDuration;
  private float StartTime;

  public float FadeSpeed;
  private Vector3 TouchFakePoint;

  private GameObject MyQuad;

  void Start()
  {
    StartTime = Time.time;
    TouchFakePoint = transform.position;
  }

  void Update()
  {
    if (Time.time - StartTime > FullDuration)
    {
      Destroy(gameObject);
    }

    TouchFakePoint += Vector3.up * Time.deltaTime * FadeSpeed;
    renderer.material.SetVector("_HandPosition", new Vector4(TouchFakePoint.x, TouchFakePoint.y, TouchFakePoint.z, 1));
  }
}