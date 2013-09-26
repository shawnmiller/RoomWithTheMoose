using UnityEngine;
using System.Collections.Generic;

public class IDManager : Singleton<IDManager>
{
  /*// Singleton
  private static IDManager manager = null;
  public static IDManager Get()
  {
    manager = (IDManager)FindObjectOfType(typeof(IDManager));

    return manager;
  }*/


  private Dictionary<int, GameObject> IDs;

  void Awake()
  {
    IDs = new Dictionary<int, GameObject>();
  }

  public void AddObject(GameObject obj)
  {
    ObjectID id = obj.GetComponent<ObjectID>();
    if (id == null)
    {
      Debug.LogError("No ObjectID found on " + obj.name);
    }
    if (!IDs.ContainsKey(id.id))
    {
      IDs.Add(id.id, obj);
    }
  }

  public GameObject GetObjectByID(int id)
  {
    return IDs[id];

    /*GameObject result = IDs.Find(x => x.ID == id);
    if (result != null)
    {
      return result.gameObject;
    }
    else
    {
      return null;
    }*/
  }
}