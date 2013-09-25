using UnityEngine;


public class ObjectID : GameComponent
{
  private int id;

  public int ID
  {
    get { return id; }
  }

  void Start()
  {
    IDManager manager = GameObject.FindObjectOfType(typeof(IDManager)) as IDManager;
    manager.AddID(this);
  }
}