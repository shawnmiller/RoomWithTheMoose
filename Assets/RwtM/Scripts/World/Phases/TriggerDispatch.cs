using UnityEngine;

public class TriggerDispatch : MonoBehaviour
{
  public bool Enabled { get; set; }
  public bool ReportExit { get; set; }

  void OnTriggerEnter(Collider other)
  {
    if (!Enabled)
      return;

    if (other.transform.root.tag == "Player")
    {
      MessageDispatch.Send("OnTriggerEnter", gameObject.name);
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (!Enabled || !ReportExit)
      return;

    if (other.transform.root.tag == "Player")
    {
      MessageDispatch.Send("OnTriggerEnter", gameObject.name);
    }
  }
}