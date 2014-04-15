using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectController : Singleton<ObjectController>
{
  private MultiDictionary<string, DynamicObject> ManagedObjects = new MultiDictionary<string,DynamicObject>();

  public void AddObject(string Category, DynamicObject Object)
  {
    Debug.Log("Added " + Object.Name + " to Category " + Category);
    ManagedObjects.Add(Category, Object);
    Debug.Log(ManagedObjects.GetValues(Category).Length);
  }

  public T GetObject<T>(string Category, string Name) where T : class
  {
    Debug.Log("Fetch Request for " + Name + " in Category " + Category);

    DynamicObject[] objects = ManagedObjects.GetValues(Category);
    Debug.Log(objects.Length);
    for (int i = 0; i < objects.Length; ++i)
    {
      Debug.Log("Object at " + i + ": " + objects[i].Name);
      if (Name == objects[i].Name)
      {
        return objects[i] as T;
      }
    }
    // Demon spawn code from hell. Do not break the seal and unleash the evil within.
    /*foreach (DynamicObject o in objects)
    {
      Debug.Log("Obeserving: " + o.Name);
      if (o.Name == Name)
        Debug.Log(Name + " Found");
        return o as T;
    }*/
    return null;
  }
}