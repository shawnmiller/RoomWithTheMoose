using UnityEngine;
using System.Collections.Generic;

public class PhaseManager : Singleton<PhaseManager>
{
  public TextAsset SceneFile;
  private GameState GState;
  private Phase GlobalPhase;
  private List<Phase> Phases = new List<Phase>();
  private int CurrentPhaseIndex = 0;

  void Start()
  {
    GState = GameState.Get();
    //SceneParser.BuildScene(SceneFile);
    SceneParserV2.ReadSceneFile(SceneFile);

    Debug.Log("CurrentPhaseIndex is Null? " + (Phases[CurrentPhaseIndex] == null));
    GlobalPhase.Init();
    Phases[CurrentPhaseIndex].Init();
    Phases[CurrentPhaseIndex].PushGlobalEvent(PPS.PP_EVENT_BEGIN_PHASE, "");
  }

  void FixedUpdate()
  {
    //Debug.Log("Phase Count: " + Phases.Count);
    if (GState.State != StateType.In_Game || Phases.Count == 0)
    {
      return;
    }
    GlobalPhase.Run();
    Phases[CurrentPhaseIndex].Run();
  }

  public void PushGlobalEvent(string EventName, string EventInstigator)
  {
    if (EventName == PPS.PP_ACTION_END_PHASE)
    {
      MoveToNextPhase();
    }
    GlobalPhase.AddPhaseEvent(EventName, EventInstigator);
    Phases[CurrentPhaseIndex].PushGlobalEvent(EventName, EventInstigator);
  }

  public void MoveToNextPhase()
  {
    ++CurrentPhaseIndex;
    if (CurrentPhaseIndex > Phases.Count)
    {
      EndGame();
    }
    else
    {
      TimerManager.Get().PushGlobalEvent(PPS.PP_EVENT_BEGIN_PHASE, "");
      Phases[CurrentPhaseIndex].Init();
      Phases[CurrentPhaseIndex].PushGlobalEvent(PPS.PP_EVENT_BEGIN_PHASE, "");
    }
  }

  public void AddPhase(Phase phase)
  {
    Phases.Add(phase);
  }

  public void AddGlobalPhase(Phase phase)
  {
    GlobalPhase = phase;
  }

  public int GetCurrentPhaseIndex()
  {
    return CurrentPhaseIndex;
  }

  public void EndGame()
  {
    CurrentPhaseIndex = Phases.Count;
    Debug.Log("Game has ended");
  }
}