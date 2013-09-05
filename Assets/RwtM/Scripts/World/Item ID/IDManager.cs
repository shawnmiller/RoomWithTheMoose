using UnityEngine;
using System.Collections.Generic;

public class IDManager : MonoBehaviour
{
  // Singleton
  private static IDManager manager = null;
  public static IDManager Get()
  {
    manager = (IDManager)FindObjectOfType(typeof(IDManager));

    return manager;
  }


  private List<ObjectID> IDs;

  void Awake()
  {
    IDs = new List<ObjectID>();
  }

  public void AddID(ObjectID id)
  {
    if (!IDs.Contains(id))
    {
      IDs.Add(id);
    }
  }

  public GameObject GetObjectByID(int id)
  {
    ObjectID result = IDs.Find(x => x.ID == id);
    if (result != null)
    {
      return result.gameObject;
    }
    else
    {
      return null;
    }
  }
}