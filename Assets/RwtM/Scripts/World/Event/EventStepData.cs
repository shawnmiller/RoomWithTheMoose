using UnityEngine;

[System.Serializable]
public class EventStepData : System.IComparable<EventStepData>
{
  public EventType type;
  public bool exclusive;
  public GameObject prefab;
  public int associatedID;
  public Vector3 position;
  public Quaternion rotation;
  public string animation;
  public AudioClip sound;
  public float startTime;
  public float duration;
  public float speed;
  public bool toggle;

  int System.IComparable<EventStepData>.CompareTo(EventStepData other)
  {
    if (this.startTime < other.startTime)
    {
      return -1;
    }
    else if (this.startTime > other.startTime)
    {
      return 1;
    }
    return 0;
  }
}