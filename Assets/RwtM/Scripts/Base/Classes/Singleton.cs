using UnityEngine;

public abstract class Singleton<T> : GameComponent
  where T : MonoBehaviour
{
  private static T _instance;

  public static T Get()
  {
    if (_instance == null)
    {
      _instance = GameObject.FindObjectOfType(typeof(T)) as T;
    }

    return _instance;
  }
}