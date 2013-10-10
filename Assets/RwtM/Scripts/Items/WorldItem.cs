using UnityEngine;

public class WorldItem : Item, IUseable
{
  void IUseable.Use()
  {
    ++useCount;

    MemoryItem memoryItem = gameObject.GetComponent<MemoryItem>();
    throw new System.NotImplementedException();
  }
}