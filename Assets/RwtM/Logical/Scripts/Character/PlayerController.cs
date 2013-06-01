using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
  private bool controllable;

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
  [SerializeField]
  private float rerunWaitTime;
  private float stopRunTime;
  private float currentStamina;
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
    if (!(gameState.State == StateType.In_Game))
    {
      return;
    }

    float currentSpeed = walkSpeed;

    // Stringy run handling code
    // TODO: Clean up
    if (Input.GetKey (KeyCode.LeftShift) && Time.time > stopRunTime + rerunWaitTime)
    {
      isRunning = true;
      currentSpeed *= runSpeedModifier;
    }
    else
    {
      if (isRunning)
      {
        stopRunTime = Time.time;
      }
      isRunning = false;
    }

    // Stamina regeneration
    if (!isRunning)
    {
      currentStamina = Mathf.Min (stamina, currentStamina + (staminaRegeneration * Time.fixedDeltaTime));
    }
    else
    {
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
}
