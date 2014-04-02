﻿using UnityEngine;
using System.Collections.Generic;

public class PhaseManager : Singleton<PhaseManager>
{
  public TextAsset SceneFile;
  private GameState GState;
  private List<Phase> Phases = new List<Phase>();
  private int CurrentPhaseIndex = 0;

  void Start()
  {
    GState = GameState.Get();
    //SceneParser.BuildScene(SceneFile);
    SceneParserV2.ReadSceneFile(SceneFile);
  }

  void FixedUpdate()
  {
    if (GState.State != StateType.In_Game || Phases.Count == 0)
    {
      return;
    }
  }

  public void PushGlobalEvent(string EventName, string EventInstigator)
  {
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

  public int GetCurrentPhaseIndex()
  {
    return CurrentPhaseIndex;
  }

  private void EndGame()
  {

  }
}