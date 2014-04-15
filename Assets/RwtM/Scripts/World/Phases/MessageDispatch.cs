using UnityEngine;
using System.Collections.Generic;

public static class MessageDispatch
{
  private static List<IMessageReceiver> Receivers = new List<IMessageReceiver>();
  public static void Send(string Event, string Instigator)
  {
    foreach (IMessageReceiver R in Receivers)
    {
      R.PushGlobalEvent(Event, Instigator);
    }
  }

  public static void RegisterReceiver(IMessageReceiver Receiver)
  {
    if (!Receivers.Contains(Receiver))
    {
      Receivers.Add(Receiver);
      Debug.Log("Added new Message Receiver");
    }
  }
}