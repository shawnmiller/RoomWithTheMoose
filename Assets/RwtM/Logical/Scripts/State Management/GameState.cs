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
  private bool transitionComplete;

  void Awake ()
  {
    state = StateType.Main_Menu;
    //Transition (StateType.Main_Menu);
  }

  public void Transition (StateType newState)
  {

  }
}
