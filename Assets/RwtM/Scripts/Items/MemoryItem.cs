using UnityEngine;

public sealed class MemoryItem : /*GameComponent*/ Item, /*IUseable,*/ IEquippable
{
  public string name;
  //public int useCount = 0;
  //public bool beingUsed;
  public MemoryItemData data;

  public override void Use()
  {
    Debug.Log("begin use");
    base.Use();
    MemoryItemGUI gui = MemoryItemGUI.Get();
    gui.Display(this, data);
  }

  public override void StopUsing()
  {
    base.StopUsing();
    ((IEquippable)this).Take();
  }

  void IEquippable.Take()
  {
    Debug.LogWarning(name + " has been added to the inventory but will not work because functionality is not completed.");
    //Inventory inventory = GameObject.FindObjectOfType(typeof(Inventory)) as Inventory;
    //inventory.AddToInventory(this);
    Destroy(gameObject);
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