using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : GameComponent
{
  private bool controllable;

  public bool Controllable
  {
    set { controllable = value; }
  }

  [SerializeField]
  private float walkSpeed;
  [SerializeField]
  private float runSpeedModifier;
  [SerializeField]
  private float gravity;
  [SerializeField]
  private float stamina;
  [SerializeField]
  private float staminaRegeneration;
  //[SerializeField]
  //private float rerunWaitTime;
  [SerializeField]
  private float minStartRunStamina;
  private float stopRunTime;
  public float currentStamina;
  private bool isRunning;
  
  // Cached objects
  private GameState gameState;
  private CharacterController controller;

	// Use this for initialization
	void Start () 
  {
    gameState = GameState.Get ();
    controller = transform.GetComponent<CharacterController> ();
    currentStamina = stamina;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
  {
    if (!(gameState.State == StateType.In_Game) && controllable)
    {
      return;
    }

    float currentSpeed = walkSpeed;
    isRunning = DetermineRunState ();

    // Stamina regeneration
    if (!isRunning)
    {
      currentStamina = Mathf.Min (stamina, currentStamina + (staminaRegeneration * Time.fixedDeltaTime));
    }
    else
    {
      currentSpeed *= runSpeedModifier;
      currentStamina = Mathf.Max (0, currentStamina - Time.fixedDeltaTime);
    }

    //Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
    Vector3 moveDirection = new Vector3 (0f, 0f, Input.GetAxis ("Vertical"));
    moveDirection = transform.TransformDirection (moveDirection);
    moveDirection = moveDirection.normalized;
    moveDirection = AlignToGroundNormal (moveDirection);
    moveDirection *= currentSpeed * Time.fixedDeltaTime;
    moveDirection.y -= gravity * Time.fixedDeltaTime;

    controller.Move (moveDirection);
	}

  private Vector3 AlignToGroundNormal (Vector3 direction)
  {
    RaycastHit hit;
    if (Physics.Raycast (transform.position, Vector3.down, out hit))
    {
      Vector3 temp = Vector3.Cross (Vector3.up, direction);
      temp = Vector3.Cross (temp, hit.normal);
      return temp;
    }
    else
    {
      return direction;
    }
  }

  private bool DetermineRunState ()
  {
    if (currentStamina - Time.fixedDeltaTime < 0)                                                   { return false; }
    if (!isRunning && currentStamina > minStartRunStamina && Input.GetKeyDown (KeyCode.LeftShift))  { return true; }
    if (isRunning && Input.GetKey (KeyCode.LeftShift))                                              { return true; }

    return false;
  }
}