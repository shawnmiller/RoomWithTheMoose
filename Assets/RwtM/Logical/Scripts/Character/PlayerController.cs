using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  [SerializeField]
  private float walkSpeed;
  [SerializeField]
  private float runSpeed;
  [SerializeField]
  private float stamina;
  private float currentStamina;

  private bool isRunning;

  // Cache GameState
  private GameState gameState;

	// Use this for initialization
	void Start () {
    gameState = GameState.Get ();
    currentStamina = stamina;
	}
	
	// Update is called once per frame
	void Update () {
    if (!(gameState.State == StateType.In_Game))
    {
      return;
    }

    if (!isRunning)
    {

    }
	}
}
