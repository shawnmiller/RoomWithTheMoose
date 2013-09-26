using UnityEngine;
using System.Collections;

public class EventStep : MonoBehaviour
{
  public EventStepData data;
  private IDManager idManager;
  private Event root;

  public void Begin(Event root, EventStepData data)
  {
    this.root = root;
    this.data = data;

    idManager = IDManager.Get();

    StartCoroutine("Run");
  }

  public IEnumerator Run()
  {
    //root = parent;

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
      case EventType.World_Swap:
        StartCoroutine("WorldSwap");
        break;
      case EventType.Unlock_Event:
        UnlockEvent();
        break;
      default:
        break;
    }
  }

  void Complete()
  {
    Destroy(this);
    // Misbehaving code
    /*Event e = gameObject.GetComponent<Event>();
    if (e != null)
    {
      Destroy(this);
    }
    EventStep[] steps = gameObject.GetComponents<EventStep>();
    if (steps.Length > 1)
    {
      Destroy(this);
    }
    Destroy(gameObject);*/
  }

  private void DestroyObject()
  {
    GameObject.Destroy(GetSceneObject());
    Complete();
  }

  private void TogglePlayerCamera()
  {
    CameraController camControl = GameObject.FindObjectOfType(typeof(CameraController)) as CameraController;
    camControl.Controllable = data.toggle;
    Complete();
  }

  private void TogglePlayerControls()
  {
    PlayerController playerControl = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
    playerControl.Controllable = data.toggle;
    Complete();
  }

  private void MoveObjectInstant()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.transform.position = data.position;
    Complete();
  }

  private IEnumerator MoveObjectTimed()
  {
    Debug.Log("Starting Timed Object Move");
    GameObject sceneObject = GetSceneObject();
    float startTime = Time.time;
    Vector3 start = sceneObject.transform.position;

    while (Time.time < startTime + data.duration)
    {
      Vector3 newPosition = Vector3.Lerp(start, data.position, Mathf.Min((Time.time - startTime) / data.duration, 1f));
      //Vector3 newPosition = Vector3.Lerp(start, data.position, Mathf.Min(Time.deltaTime * data.speed, 1f));
      sceneObject.transform.position = newPosition;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    Complete();
  }

  private void PlayAnimation()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.animation.Play(data.animation);
    Complete();
  }

  private void PlaySound()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.audio.PlayOneShot(data.sound);
    Complete();
  }

  private void RotateObjectInstant()
  {
    GameObject sceneObject = GetSceneObject();
    sceneObject.transform.rotation = data.rotation;
    Complete();
  }

  private IEnumerator RotateObjectOverTime()
  {
    GameObject sceneObject = GetSceneObject();
    float startTime = Time.time;
    Quaternion start = sceneObject.transform.rotation;

    while (Time.time < startTime + data.duration)
    {
      Quaternion newRotation = Quaternion.Slerp(start, data.rotation, Mathf.Min((Time.time - startTime) / data.duration, 1f));
      sceneObject.transform.rotation = newRotation;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    Complete();
  }

  private void SpawnObject()
  {
    GameObject temp = GameObject.Instantiate(data.prefab, data.position, data.rotation) as GameObject;
    ObjectID id = temp.AddComponent<ObjectID>();
    id.id = data.associatedID;
    idManager.AddObject(temp);
    Complete();
  }

  private IEnumerator WorldSwap()
  {
    WorldState world = WorldState.Get();
    float startTime = Time.time;

    while (Time.time < startTime + data.duration)
    {
      world.position = ((Time.time - startTime) / data.duration) * data.speed;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    Complete();
  }

  private void UnlockEvent()
  {
    GameObject sceneObject = GetSceneObject();
    EventTrigger trigger = sceneObject.GetComponent<EventTrigger>();
    trigger.enabled = true;
    Complete();
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