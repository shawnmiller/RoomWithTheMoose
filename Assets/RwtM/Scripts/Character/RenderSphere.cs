using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class RenderSphere : MonoBehaviour
{
  public float SphereRadius;
  private const float ExpansionTime = 0.5f;
  private List<Transform> MyCollisions = new List<Transform>();

  void Start()
  {
    SphereCollider Collider = GetComponent<SphereCollider>() as SphereCollider;
    Collider.radius = 0.1f;
    StartCoroutine("ExpandRadius");
  }

  void OnTriggerEnter(Collider other)
  {
    Debug.Log("Collision with: " + other.name);
    if (other.transform.root.tag != "Player")
    {
      if (!MyCollisions.Contains(other.transform))
      {
        MyCollisions.Add(other.transform);
        RenderController.Get().NewRenderStatus(other.transform, true);
      }
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.transform.root.tag != "Player")
    {
      MyCollisions.Remove(other.transform);
      RenderController.Get().NewRenderStatus(other.transform, false);
    }
  }

  public bool Contains(Transform Object)
  {
    return MyCollisions.Contains(Object);
  }

  IEnumerator ExpandRadius()
  {
    float StartTime = Time.time;
    while (Time.time - StartTime < ExpansionTime)
    {
      ((SphereCollider)collider).radius = Mathf.Lerp(0, SphereRadius, Mathf.Min(1, (Time.time - StartTime) / ExpansionTime));
      yield return null;
    }
  }
}