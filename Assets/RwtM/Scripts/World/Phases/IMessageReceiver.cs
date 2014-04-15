public interface IMessageReceiver
{
  void PushGlobalEvent(string EventName, string EventInstigator);
}
