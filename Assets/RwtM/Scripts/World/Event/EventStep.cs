using UnityEngine;
using System.Collections;

public class EventStep : GameComponent
{
  private EventStepData data;
  private IDManager idManager;
  private Event root;

  public void Begin(Event root, EventStepData data)
  {
    this.root = root;
    this.data = data;

    idManager = IDManager.Get();

    if (!this.data.exclusive)
    {
      this.root.Processing = false;
    }
  }

  public IEnumerator Run(Event parent)
  {
    root = parent;
    parent.Processing = true;


    switch (data.type)
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
    root.Processing = false;
  }

  private void DestroyObject()
  {
    GameObject.Destroy(GetSceneObject());
  }

  private void TogglePlayerCamera()
  {
    CameraController camControl = GameObject.FindObjectOfType(typeof(CameraController)) as CameraController;
    camControl.Controllable = data.toggle;
  }

  private void TogglePlayerControls()
  {
    PlayerController playerControl = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
    playerControl.Controllable = data.toggle;
  }

  private void MoveObjectInstant()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.transform.position = data.position;
  }

  private IEnumerator MoveObjectTimed()
  {
    GameObject sceneObject = GetSceneObject();
    float startTime = Time.time;
    Vector3 start = sceneObject.transform.position;

    while (Time.time < startTime + data.duration)
    {
      Vector3 newPosition = Vector3.Lerp(start, data.position, Time.deltaTime * data.speed);
      sceneObject.transform.position = newPosition;
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }

  private void PlayAnimation()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.animation.Play(data.animation);
  }

  private void PlaySound()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.audio.PlayOneShot(data.sound);
  }

  private void RotateObjectInstant()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.transform.rotation = data.rotation;
  }

  private IEnumerator RotateObjectOverTime()
  {
    GameObject sceneObject = GetSceneObject();
    float startTime = Time.time;
    Quaternion start = sceneObject.transform.rotation;

    while (Time.time < startTime + data.duration)
    {
      Quaternion newRotation = Quaternion.Slerp(start, data.rotation, Time.deltaTime * data.speed);
      sceneObject.transform.rotation = newRotation;
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }

  private void SpawnObject()
  {
    GameObject.Instantiate(data.prefab, data.position, data.rotation);
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
    return idManager.GetObjectByID(data.associatedID);
  }
}