using UnityEngine;
using System.Collections;

public class EventStep
{
  private IDManager idManager;

  [SerializeField]
  private bool exclusive;

  [SerializeField]
  private EventType type;
  [SerializeField]
  private GameObject prefab;
  [SerializeField]
  private int associatedID;
  [SerializeField]
  private Vector3 position;
  [SerializeField]
  private Quaternion rotation;
  [SerializeField]
  private string animation;
  [SerializeField]
  private AudioClip sound;
  [SerializeField]
  private float duration;
  [SerializeField]
  private float speed;
  [SerializeField]
  private bool toggle;

  public IEnumerator Run(Event parent)
  {
    parent.Processing = true;

    if (idManager == null)
    {
      idManager = IDManager.Get();
    }



    switch (type)
    {
      case EventType.Destroy_Object:
        DestroyObject();
        break;
      case EventType.Toggle_Player_Camera:
        TogglePlayerCamera();
        break;
      case EventType.Toggle_Player_Movement:
        TogglePlayerControls();
        break;
      case EventType.Move_Object_Instant:
        MoveObjectInstant();
        break;
      case EventType.Move_Object_Timed:
        yield return StartCoroutine("MoveObjectTimed");
        break;
      case EventType.Play_Animation:
        PlayAnimation();
        break;
      case EventType.Play_Sound:
        PlaySound();
        break;
      case EventType.Rotate_Object_Instant:
        RotateObjectInstant();
        break;
      case EventType.Rotate_Object_Timed:
        yield return StartCoroutine("RotateObjectOverTime");
        break;
      case EventType.Spawn_Object:
        SpawnObject();
        break;
      case EventType.Unlock_Event:
        UnlockEvent();
        break;
      default:
        break;
    }
    parent.Processing = false;
  }

  private void DestroyObject()
  {
    GameObject.Destroy(GetSceneObject());
  }

  private void TogglePlayerCamera()
  {
    CameraController camControl = GameObject.FindObjectOfType(typeof(CameraController)) as CameraController;
    camControl.Controllable = toggle;
  }

  private void TogglePlayerControls()
  {
    PlayerController playerControl = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
    playerControl.Controllable = toggle;
  }

  private void MoveObjectInstant()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.transform.position = position;
  }

  private IEnumerator MoveObjectTimed()
  {
    GameObject sceneObject = GetSceneObject();
    float startTime = Time.time;
    Vector3 start = sceneObject.transform.position;

    while (Time.time < startTime + duration)
    {
      Vector3 newPosition = Vector3.Lerp(start, position, Time.deltaTime * speed);
      sceneObject.transform.position = newPosition;
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }

  private void PlayAnimation()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.animation.Play(animation);
  }

  private void PlaySound()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.audio.PlayOneShot(sound);
  }

  private void RotateObjectInstant()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.transform.rotation = rotation;
  }

  private IEnumerator RotateObjectOverTime()
  {
    GameObject sceneObject = GetSceneObject();
    float startTime = Time.time;
    Quaternion start = sceneObject.transform.rotation;

    while(Time.time < startTime + duration)
    {
      Quaternion newRotation = Quaternion.Slerp(start, rotation, Time.deltaTime * speed);
      sceneObject.transform.rotation = newRotation;
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }

  private void SpawnObject()
  {
    GameObject.Instantiate(prefab, position, rotation);
  }

  private void UnlockEvent()
  {
    GameObject sceneObject = GetSceneObject();
    EventTrigger trigger = sceneObject.GetComponent<EventTrigger>();
    trigger.enabled = true;
  }

  /// <summary>
  /// Cleaner way of going about getting an object from the IDManager.
  /// </summary>
  /// <returns>The object with the associated ID from within the scene.</returns>
  private GameObject GetSceneObject()
  {
    return idManager.GetObjectByID(associatedID);
  }
}