using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : GameComponent
{
  public bool Controllable = true;

  public float WalkSpeed;
  public float Gravity;

  public float NormalHeight;
  public float CrouchHeight;
  public float CrouchSpeed;
  //public float RunSpeedModifier;
  //public float Stamina;
  //public float StaminaRegeneration;
  //public float minStartRunStamina;
  //private float stopRunTime;
  //private float currentStamina;
  //private bool isRunning;
  
  // Cached objects
  private GameState gameState;
  private CharacterController Controller;

	// Use this for initialization
	void Start () 
  {
    gameState = GameState.Get ();
    Controller = transform.GetComponent<CharacterController> ();
    //currentStamina = Stamina;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
  {
    if (!(gameState.State == StateType.In_Game) || !Controllable)
    {
      return;
    }

    float currentSpeed = WalkSpeed;
    //isRunning = DetermineRunState ();

    // Stamina regeneration
    /*if (!isRunning)
    {
      currentStamina = Mathf.Min (Stamina, currentStamina + (StaminaRegeneration * Time.fixedDeltaTime));
    }
    else
    {
      currentSpeed *= RunSpeedModifier;
      currentStamina = Mathf.Max (0, currentStamina - Time.fixedDeltaTime);
    }*/

    if (Input.GetKey(KeyCode.LeftControl))
    {
      Controller.height = Mathf.Lerp(Controller.height, CrouchHeight, Time.fixedDeltaTime * CrouchSpeed);
    }
    else
    {
      float NewHeight = Mathf.Lerp(Controller.height, NormalHeight, Time.fixedDeltaTime * CrouchSpeed);
      Controller.height = NewHeight;
      Vector3 AdjustedPosition = transform.position;
      AdjustedPosition.y += (NewHeight - Controller.height) + 0.05f;
      transform.position = AdjustedPosition;
    }

    Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
    moveDirection = transform.TransformDirection (moveDirection);
    moveDirection = moveDirection.normalized;
    moveDirection = AlignToGroundNormal (moveDirection);
    moveDirection *= currentSpeed * Time.fixedDeltaTime;
    moveDirection.y -= Gravity * Time.fixedDeltaTime;

    Controller.Move (moveDirection);
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

  /*private bool DetermineRunState ()
  {
    if (currentStamina - Time.fixedDeltaTime < 0)                                                   { return false; }
    if (!isRunning && currentStamina > minStartRunStamina && Input.GetKeyDown (KeyCode.LeftShift))  { return true; }
    if (isRunning && Input.GetKey (KeyCode.LeftShift))                                              { return true; }

    return false;
  }*/
}