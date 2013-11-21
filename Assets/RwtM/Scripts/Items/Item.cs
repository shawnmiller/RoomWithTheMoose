using UnityEngine;

public class Item : GameComponent, IUseable
{
  public string name;
  public int useCount = 0;
  private bool fBeingUsed;

  public bool InUse
  {
    get { return fBeingUsed; }
  }

  public bool HasBeenUsed
  {
    get { return useCount > 0 && !fBeingUsed; }
  }

  void Update()
  {
    if(fBeingUsed == true)
      Debug.Log(transform.name + " is being used.");
  }

  /// <summary>
  /// The base class's version of this function MUST be called in all overriden child classes.
  /// </summary>
  public virtual void Use()
  {
    Debug.Log("Use being called on the base class for object: " + transform.name);
    ++useCount;
    fBeingUsed = true;
  }

  /// <summary>
  /// The base class's version of this function MUST be called in all overriden child classes.
  /// </summary>
  public virtual void StopUsing()
  {
    fBeingUsed = false;
  }
}