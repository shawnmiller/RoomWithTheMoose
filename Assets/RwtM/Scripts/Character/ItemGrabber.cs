using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ItemGrabber : MonoBehaviour
{
  private InteractibleItem ClosestItem;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      Debug.Log("Attempting to use an item");
      UseClosestItem();
    }
  }

  private void UseClosestItem()
  {
    if (ClosestItem != null)
    {
      Debug.Log("Item exists");
      ClosestItem.Use();
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<InteractibleItem>() != null)
    {
      Debug.Log("Set ClosestItem to: " + other.name);
      ClosestItem = other.GetComponent<InteractibleItem>();
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (ClosestItem != null && ClosestItem.gameObject == other.gameObject)
    {
      Debug.Log("No ClosestItem");
      ClosestItem = null;
    }
  }
}