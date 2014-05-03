using UnityEngine;
using System.Collections.Generic;

public class PhaseManager : Singleton<PhaseManager>, IMessageReceiver
{
  public TextAsset SceneFile;
  private GameState GState;
  private Phase GlobalPhase;
  private List<Phase> Phases = new List<Phase>();
  private int CurrentPhaseIndex = 0;

  void Start()
  {
    MessageDispatch.RegisterReceiver(this);

    GState = GameState.Get();
    //SceneParser.BuildScene(SceneFile);
    SceneParser.ReadSceneFile(SceneFile);

    Debug.Log("CurrentPhaseIndex is Null? " + (Phases[CurrentPhaseIndex] == null));
    GlobalPhase.Init();
    Phases[CurrentPhaseIndex].Init();
    Phases[CurrentPhaseIndex].PushGlobalEvent(PP.EVENT_BEGIN_PHASE, "");
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

  void IMessageReceiver.PushGlobalEvent(string EventName, string Instigator)
  {
    Debug.Log("PhaseManager received event: " + EventName + " Instigator: " + Instigator);
    if (EventName == PP.ACTION_END_PHASE)
    {
      MoveToNextPhase();
    }
    GlobalPhase.PushGlobalEvent(EventName, Instigator);
    Phases[CurrentPhaseIndex].PushGlobalEvent(EventName, Instigator);
  }

  public void MoveToNextPhase()
  {
    ++CurrentPhaseIndex;
    Debug.Log("Phase Completed -> Moving to Phase " + CurrentPhaseIndex + " of " + Phases.Count);
    if (CurrentPhaseIndex > Phases.Count)
    {
      EndGame();
    }
    else
    {
      TimerManager.Get().PushGlobalEvent(PP.EVENT_BEGIN_PHASE, "");
      Phases[CurrentPhaseIndex].Init();
      Phases[CurrentPhaseIndex].PushGlobalEvent(PP.EVENT_BEGIN_PHASE, "");
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
    Application.LoadLevel("GameOver");
  }
}