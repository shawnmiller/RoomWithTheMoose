using UnityEngine;

public class CameraController : MonoBehaviour
{
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

  [SerializeField]
  private Vector3 lookLeftRotation;
  [SerializeField]
  private Vector3 lookRightRotation;
  // 0 = forward, -1 = left, 1 = right
  private int lookDirection = 0; 
  // Used to store the direction that they were looking prior to using a look left/right
  private Vector3 storedLookRotation;

  private float currentX;
  private float currentY;

  // Cached objects
  private GameState gameState;

  void Start ()
  {
    gameState = GameState.Get ();
    if(!neckJoint)
    {
      neckJoint = transform.GetChild(0);
    }
    currentX = neckJoint.rotation.eulerAngles.y;
    currentY = neckJoint.rotation.eulerAngles.x;
  }

  void FixedUpdate ()
  {
    if (!(gameState.State == StateType.In_Game))
    {
      return;
    }

    currentX += Input.GetAxis ("Mouse X");
    currentX = ClampCircular (currentX);
    currentY += Input.GetAxis ("Mouse Y");
    currentY = ClampVertical (currentY);
  }

  void HandleNormalControls ()
  {

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