using UnityEngine;
using System;

public abstract class Singleton<T> : GameComponent
  where T : MonoBehaviour
{
  private static T _instance;

  public static T Get()
  {
    if (_instance == null)
    {
      _instance = GameObject.FindObjectOfType(typeof(T)) as T;

      // If we still don't have an instance, create one.
      if (_instance == null)
      {
        GameObject obj = new GameObject("Singleton Instance");
        obj.AddComponent<T>();
        _instance = obj.GetComponent<T>();
      }
    }

    return _instance;
  }
}