interface IStateWatcher
{
  void Subscribe();
  void StateChange(StateType state);
}