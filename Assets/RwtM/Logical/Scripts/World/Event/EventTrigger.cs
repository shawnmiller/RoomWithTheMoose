using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour
{
  public bool isRunOnce = true;
  public int maxActivationCount = 1;
  private int currentActivationCount;

  public bool activateOnObjectInteraction = false;


  // Use this for initialization
  void Start ()
  {

  }

  // Update is called once per frame
  void Update ()
  {

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
