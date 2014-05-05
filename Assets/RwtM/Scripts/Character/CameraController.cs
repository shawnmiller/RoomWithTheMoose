using UnityEngine;


public class CameraController : StateComponent
{
  private bool controllable = true;

  public bool Controllable
  {
    set { controllable = value; }
  }

  [SerializeField]
  private float xSpeed;
  [SerializeField]
  private float ySpeed;
  [SerializeField]
  private float yMin;
  [SerializeField]
  private float yMax;
  [SerializeField]
  private Transform neckJoint;

  private bool isLooking;
  [SerializeField]
  private Quaternion lookLeftRotation;
  [SerializeField]
  private Quaternion lookRightRotation;
  [SerializeField]
  private float lookSpeed;

  // 0 = forward, -1 = left, 1 = right
  private int lookDirection = 0; 
  // Used to store the direction that they were looking prior to using a look left/right
  private Quaternion storedLookRotation;
  // The actual direction we're looking at
  private Quaternion currentLookRotation;

  private float currentX;
  private float currentY;

  // Cached objects
  private GameState gameState;


  void Start ()
  {
    base.Start();

    if(!neckJoint)
    {
      neckJoint = transform.GetChild(0);
    }
    currentX = neckJoint.rotation.eulerAngles.y;
    currentY = neckJoint.rotation.eulerAngles.x;
  }

  void FixedUpdate ()
  {
    if (!(_state.State == StateType.In_Game) || !controllable)
    {
      Debug.Log("Controllable: " + controllable);
      Debug.Log("Not working.");
      return;
    }

    //isLooking = (Input.GetKey (KeyCode.Mouse1) ? true : false);

    if (!isLooking)
    {
      HandleNormalControls ();
    }
    else
    {
      HandleNormalControls ();
      //HandleLookControls ();
    }
  }

  void HandleNormalControls ()
  {
    currentX += Input.GetAxis ("Mouse X");
    currentX = ClampCircular (currentX);
    currentY += Input.GetAxis ("Mouse Y");
    currentY = ClampVertical (currentY);
    //Quaternion targetRotation = Quaternion.Euler (-currentY, currentX, 0);
    transform.rotation = Quaternion.Euler (0, currentX, 0);
    neckJoint.localRotation = Quaternion.Euler (-currentY, 0, 0);
    //transform.rotation = Quaternion.Euler (0, currentX, 0);
    //neckJoint.rotation = Quaternion.Euler (-currentY, currentX, 0);
  }

  void HandleLookControls ()
  {

  }

  float ClampCircular (float angle)
  {
    if (angle < -360)
    {
      return angle + 360;
    }
    if (angle > 360)
    {
      return angle - 360;
    }
    return angle;
  }

  float ClampVertical (float angle)
  {
    return Mathf.Clamp(angle, yMin, yMax);
  }
}