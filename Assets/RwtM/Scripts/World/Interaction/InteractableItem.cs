using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour
{
  private bool isBeingUsed;
  private bool hasBeenUsed;
  [SerializeField]
  private GameObject inventoryItem;

  public bool Activated
  {
    get { return isBeingUsed; }
  }

  public bool WasActivated
  {
    get { return hasBeenUsed; }
  }

  public void Activate ()
  {
    StartCoroutine ("BeginTasks");
    isBeingUsed = true;
  }

  public void Deactivate ()
  {
    isBeingUsed = false;
    hasBeenUsed = true;
    StopCoroutine ("BeginTasks"); // JiC
  }

  IEnumerator BeginTasks ()
  {
    Debug.LogError ("BeginTasks function has not been implemented yet.");
    return null;
  }
}
