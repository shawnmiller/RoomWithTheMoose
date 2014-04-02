using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectController : Singleton<ObjectController>
{
  private MultiDictionary<string, DynamicObject> ManagedObjects = new MultiDictionary<string,DynamicObject>();

  public void AddObject(string Category, DynamicObject Object)
  {
    ManagedObjects.Add(Category, Object);
  }

  public T GetObject<T>(string Category, string Name) where T : class
  {
    DynamicObject[] objects = ManagedObjects.GetValues(Category);
    foreach (DynamicObject o in objects)
    {
      if (o.Name == Name)
        return o as T;
    }
    return null;
  }
}