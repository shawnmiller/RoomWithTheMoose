using UnityEngine;

public sealed class MemoryItem : GameComponent, IUseable, IEquippable
{
  public string name;
  public int useCount = 0;
  public bool beingUsed;
  public MemoryItemData data;


  void IUseable.Use()
  {
    ++useCount;
    beingUsed = true;
  }

  void IEquippable.Take()
  {
    Debug.LogWarning(name + " has been added to the inventory but will not work because functionality is not completed.");
    beingUsed = false;
    Inventory inventory = GameObject.FindObjectOfType(typeof(Inventory)) as Inventory;
    inventory.AddToInventory(this);
  }

  void IEquippable.Discard()
  {
    throw new System.NotImplementedException();
  }

  void IEquippable.Destroy()
  {
    throw new System.NotImplementedException();
  }
}