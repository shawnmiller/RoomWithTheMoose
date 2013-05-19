using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
  // Singleton
  private static EventManager manager = null;
  public static EventManager Get ()
  {
    if (manager == null)
    {
      manager = (EventManager)FindObjectOfType (typeof (EventManager));
    }

    return manager;
  }

  Queue<GameEvent> eventQueue;

  void Awake ()
  {
    eventQueue = new Queue<GameEvent> ();
  }

  void Update ()
  {

  }

  void BeginEvent (GameEvent gameEvent)
  {
    gameEvent.Begin ();
  }

  public void AddEvent (GameEvent gameEvent, bool instant)
  {
    if (!instant)
    {
      eventQueue.Enqueue (gameEvent);
    }
    else
    {
      BeginEvent (gameEvent);
    }
  }
}