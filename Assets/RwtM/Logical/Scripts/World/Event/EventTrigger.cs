using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour
{
  [SerializeField]
  private bool isRunOnce = true;
  [SerializeField]
  private int maxActivationCount = 1;

  private int currentActivationCount;

  [SerializeField]
  private bool activateOnObjectPickUp = false;
  [SerializeField]
  private bool activateOnObjectPutDown = false;
  [SerializeField]
  private InteractableItem activationObject;


  // Use this for initialization
  void Start ()
  {
    if (activateOnObjectPickUp && activateOnObjectPutDown)
    {
      Debug.LogWarning("This event will trigger on both pick up and put down. If this is intended, please ensure that Max Activation Count > 1 and Is Run Once is FALSE");
    }
  }

  // Update is called once per frame
  void Update ()
  {
    if (!(activateOnObjectPickUp || activateOnObjectPutDown))
    {
      return;
    }

    if (activationObject.Activated)
    {
      TriggerEvent ();
    }
  }

  void OnTriggerEnter (Collider other)
  {
    if (other.transform.root.tag == "Player")
    {
      TriggerEvent ();
    }
  }

  void TriggerEvent ()
  {
    // Activate the event.


    // Destroy if this event only runs once or has reached
    // its activation limit.
    if (isRunOnce)
    {
      Destroy (this);
    }
  }
}
