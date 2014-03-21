using UnityEngine;
using System.Collections.Generic;

public class PhaseManager : Singleton<PhaseManager>
{
  public TextAsset SceneFile;
  private GameState GState;
  private Phase[] Phases;
  private int CurrentPhaseIndex = 0;

  void Start()
  {
    GState = GameState.Get();
    Phases = SceneParser.BuildScene(SceneFile, this);
  }

  void FixedUpdate()
  {
    if (GState.State != StateType.In_Game)
    {
      return;
    }
  }

  public void MoveToNextPhase()
  {
    ++CurrentPhaseIndex;
    if (CurrentPhaseIndex > Phases.Length)
    {
      EndGame();
    }
  }

  public int GetCurrentPhaseIndex()
  {
    return CurrentPhaseIndex;
  }

  private void EndGame()
  {

  }
}