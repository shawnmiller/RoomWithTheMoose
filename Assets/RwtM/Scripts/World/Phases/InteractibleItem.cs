using UnityEngine;

public class InteractibleItem : MonoBehaviour
{
  public void Use()
  {
    MessageDispatch.Send(PPS.PP_EVENT_ITEM_PICKUP, this.name);
    Destroy(gameObject);
  }
}