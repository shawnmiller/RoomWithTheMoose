using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
  // Singleton
  private static GameState manager = null;
  public static GameState Get ()
  {
    if (manager == null)
    {
      manager = (GameState)FindObjectOfType (typeof (GameState));
    }

    return manager;
  }

  private StateType state;

  public StateType State
  {
    get { return state; }
  }

  private bool saveOnTransitionToMenu;
  private bool transitionComplete;

  void Awake ()
  {
    state = StateType.Main_Menu;

    // For testing purposes
    state = StateType.In_Game;
  }

  public void Transition (StateType newState)
  {
    switch (newState)
    {
      case StateType.Main_Menu:
        break;
      case StateType.Settings_Menu:
        break;
      case StateType.Continue_Menu:
        break;
      case StateType.Credits:
        break;
      case StateType.In_Game:
        break;
      case StateType.In_Game_Paused:
        break;
      case StateType.Game_Over:
        break;
      default:
        break;
    }
  }
}
