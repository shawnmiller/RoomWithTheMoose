using UnityEngine;
using System.Collections.Generic;

public class Inventory : GameComponent
{
  private const int INVENTORY_SIZE = 10;
  private List<GameComponent> inventory = new List<GameComponent>(INVENTORY_SIZE);

  public bool Full
  {
    get { return inventory.Count == INVENTORY_SIZE; }
  }

  public GameComponent[] Inventory
  {
    get { return inventory.ToArray(); }
  }

  public bool Has(GameComponent item)
  {
    if(!(item is IEquippable))
    {
      Debug.LogError("Inventory on " + gameObject.name + " was passed a non-equippable object: " + item.name);
      return false;
    }

    if (inventory.Contains(item))
    {
      return true;
    }
    return false;
  }
  public void AddToInventory(GameComponent item)
  {
    if (!Full)
    {
      inventory.Add(item);
    }
  }

  public void RemoveFromInventory(GameComponent item)
  {
    inventory.Remove(item);
  }
}