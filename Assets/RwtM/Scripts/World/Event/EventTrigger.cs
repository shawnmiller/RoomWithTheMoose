using UnityEngine;
using System.Collections;

public class EventTrigger : GameComponent
{
  /*[SerializeField]
  private bool isRunOnce = true;
  [SerializeField]
  private int maxActivationCount = 1;

  private int currentActivationCount;*/

  [SerializeField]
  private EventTriggerMethod triggerMethod;

  // Object-based activation
  [SerializeField]
  private bool activateOnObjectPickUp = false;
  [SerializeField]
  private bool activateOnObjectPutDown = false;
  [SerializeField]
  private InteractableItem activationObject;

  // Look-based activation
  [SerializeField]
  private float viewAngle;
  private Transform lookPoint;

  private Event eventToActivate;


  // Use this for initialization
  void Start ()
  {
    if (activateOnObjectPickUp && activateOnObjectPutDown)
    {
      Debug.LogWarning("This event will trigger on both pick up and put down.");
    }

    // Get the maximum dot product value for the given view angle
    viewAngle /= 2f;
    viewAngle = 1f - (viewAngle / 90f);
  }

  // Update is called once per frame
  void Update ()
  {
    if (activateOnObjectPickUp && activationObject.Activated)
    {
      TriggerEvent ();
    }

    if (activateOnObjectPutDown && activationObject.WasActivated)
    {
      TriggerEvent ();
    }
  }

  void OnTriggerEnter (Collider other)
  {
    if (other.transform.root.tag != "Player" &&
        triggerMethod != EventTriggerMethod.Enter_Trigger)
    {
      TriggerEvent ();
    }
  }

  void OnTriggerStay(Collider other)
  {
    if (other.transform.root.tag != "Player" &&
        triggerMethod != EventTriggerMethod.Look_Alignment)
    {
      return;
    }

    Transform player = other.transform.root;

    Vector3 toLookPoint = (lookPoint.position - player.position).normalized;
    float result = Vector3.Dot (player.forward, toLookPoint);
    if (result < viewAngle)
    {
      TriggerEvent ();
    }
  }

  void TriggerEvent ()
  {
    // Activate the event.


    throw new System.NotImplementedException ();
  }
}
