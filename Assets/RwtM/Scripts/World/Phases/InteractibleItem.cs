using UnityEngine;

public class InteractibleItem : MonoBehaviour
{
  public bool Persistent = false;
  public int MessageLimit = 1;
  private int MessageCount = 0;

  public void Use()
  {
    if (MessageLimit != -1 && MessageCount < MessageLimit)
    {
      MessageDispatch.Send(PPS.PP_EVENT_ITEM_PICKUP, this.name);
      ++MessageCount;
    }

    if(!Persistent)
      Destroy(gameObject);
  }
}