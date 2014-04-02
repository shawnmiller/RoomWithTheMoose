public static class MessageDispatch
{
  public static void Send(string Event, string Instigator)
  {
    PhaseManager.Get().PushGlobalEvent(Event, Instigator);
    TimerManager.Get().PushGlobalEvent(Event, Instigator);
  }
}