using UnityEngine;
using System.Collections;

public class EventStepData
{
  public EventType type;
  public bool exclusive;
  public GameObject prefab;
  public int associatedID;
  public Vector3 position;
  public Quaternion rotation;
  public string animation;
  public AudioClip sound;
  public float duration;
  public float speed;
  public bool toggle;
}