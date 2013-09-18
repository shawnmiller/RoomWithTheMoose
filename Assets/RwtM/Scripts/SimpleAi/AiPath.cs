using UnityEngine;

public class AiPath : GameComponent
{
  public AiPathNode[] path;
  public GameState state;

  void Start()
  {
    state = GameState.Get();
  }

  void Update()
  {
    if(state != StateType.In_Game)
    {
      return;
    }
  }
}
