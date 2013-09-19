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

  public void SetOnPath(GameObject ai, int pathNode)
  {
    if(pathNode >= path.Length)
    {
      Debug.LogError("Attempted to place Ai " + ai.name + " on path " + transform.root.gameObject.name + 
                     " but the pathNode exceeds the length of the path.");
      return;
    }

    ai.transform.position = path[pathNode].transform.position;
    ai.transform.rotation = path[pathNode].transform.rotation;
  }

  public void Move(GameObject actor)
  {
    if(state != StateType.In_Game)
    {
      return;
    }

    SimpleAi ai = actor.GetComponent<SimpleAi>();
    
  }
}
