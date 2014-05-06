using UnityEngine;

public class ArmIK : MonoBehaviour
{
  /****************************Arm Rotation****************************/
  public float ZMin;
  public float ZMax;
  public float PivotSpeedModifier;

  private float CurrentZ;

  /******************************Arm Bend******************************/
  private Transform ArmPivotJoint;
  public Transform ShoulderJoint;
  public Transform ElbowJoint;
  public Transform WristJoint;
  public Transform ClaspPoint;  // used to determine where on the hand will be used as the touching point
  public Transform FingerTip;

  private Transform LookPoint;
  private Transform TargetPoint;

  private Vector3 BaseShoulderRotation;
  private Vector3 BaseElbowRotation;

  private Vector3 DirectionVector; // cached to avoid multiple calculations

  public float ElbowMinBend = 2f;
  public float ElbowMaxBend = 133f;

  private float UALength;
  private float LALength;
  private float HandLength;
  private float WCLength; // wrist -> clasp distance
  private float MaxHitDistance;
  private float PreviousDistance;
  private float PreviousAngle;

  public float AntiJointLockAmount = 0.05f;
  public float HandThickness;
  //private float HHPLength; // Hand Half-Point Length (mid-palm)

  public float MaxDistanceChangeSpeed;
  public float MaxReturnLerpSpeed;

  /******************************Wrist Flex******************************/
  private Vector3 BaseWristRotation;

  void Start()
  {
    if (!ShoulderJoint || !ElbowJoint || !WristJoint || !ClaspPoint || !FingerTip)
    {
      Debug.LogError("Missing Joint");
      Debug.Break();
    }

    ArmPivotJoint = ShoulderJoint.parent;
    CurrentZ = ArmPivotJoint.localEulerAngles.x;

    BaseShoulderRotation = ShoulderJoint.localEulerAngles;
    BaseElbowRotation = ElbowJoint.localEulerAngles;
    BaseWristRotation = WristJoint.localEulerAngles;

    UALength = Vector3.Distance(ShoulderJoint.position, ElbowJoint.position);
    LALength = Vector3.Distance(ElbowJoint.position, WristJoint.position);
    HandLength = Vector3.Distance(WristJoint.position, FingerTip.position);
    //HHPLength = Vector3.Distance(WristJoint.position, FingerTip.position) / 4f;
    //MaxHitDistance = UALength + LALength + HandLength;

    LookPoint = new GameObject("Look Point").transform;
    LookPoint.position = FingerTip.position;
    //LookPoint.parent = transform.root;
    LookPoint.parent = ShoulderJoint.parent;

    MaxHitDistance = Vector3.Distance(ShoulderJoint.position, LookPoint.position);

    TargetPoint = new GameObject("Target Point").transform;
    TargetPoint.parent = transform.root;
  }

  void FixedUpdate()
  {
    RotateArm();

    RaycastHit Hit;
    DirectionVector = (LookPoint.position - ShoulderJoint.position).normalized;
    if (Physics.Raycast(ShoulderJoint.position, DirectionVector, out Hit, MaxHitDistance))
    {
      Debug.Log("Hit: " + Hit.transform.name);
      ArmIKTarget IKTarget = new ArmIKTarget();
      IKTarget.Location = Hit.point;
      IKTarget.Distance = Hit.distance;
      IKTarget.Normal = Hit.normal;
      IKTarget.IsTouching = true;

      PositionIK(IKTarget);
    }
    else
    {
      ReturnToRest();
    }

    PreviousDistance = Vector3.Distance(ShoulderJoint.position, TargetPoint.position);
  }

  private void RotateArm()
  {
    CurrentZ += Input.GetAxis("Mouse Y");
    Vector3 NewRotation = ArmPivotJoint.localEulerAngles;
    CurrentZ = Mathf.Clamp(CurrentZ, ZMin, ZMax);
    NewRotation.x = -CurrentZ;

    ArmPivotJoint.localRotation = Quaternion.Euler(NewRotation);
  }

  private void RotateWrist(ArmIKTarget IKTarget)
  {
    /*if (IKTarget.IsTouching)
    {
      Vector3 WristToHand = (FingerTip.position - WristJoint.position).normalized;
      Vector3 HandUpVector = Vector3.Cross(WristJoint.TransformDirection(Vector3.right), WristToHand);
      Quaternion RotationDifference = Quaternion.FromToRotation(HandUpVector, IKTarget.Normal);
      //WristToHand = RotationDifference * WristToHand;
      WristJoint.rotation = RotationDifference;
    }
    else
    {
      Vector3 NewWristRotation = Vector3.Lerp(WristJoint.localEulerAngles, BaseWristRotation, Time.fixedDeltaTime * MaxReturnLerpSpeed);
      WristJoint.localRotation = Quaternion.Euler(NewWristRotation);
    }*/
  }
  
  private void ReturnToRest()
  {
    ArmIKTarget IKTarget = new ArmIKTarget();
    IKTarget.Distance = Mathf.Min(MaxHitDistance, PreviousDistance + Time.fixedDeltaTime * MaxReturnLerpSpeed);
    IKTarget.Location = ShoulderJoint.position + DirectionVector * IKTarget.Distance;

    PositionIK(IKTarget);
  }

  private void PositionIK(ArmIKTarget IKTarget)
  {
    TargetPoint.position = IKTarget.Location;

    Vector3 WristTarget = GetWristTarget(IKTarget);
    
    

    /****************************Elbow Handling****************************/
    float a = UALength;
    float b = LALength;
    float c = Vector3.Distance(ShoulderJoint.position, WristTarget);
    c = Mathf.Clamp(c, Mathf.Abs(a - b) + 0.01f, UALength + LALength - 0.01f);
    c = Mathf.Clamp(c, PreviousDistance - MaxDistanceChangeSpeed * Time.fixedDeltaTime, PreviousDistance + MaxDistanceChangeSpeed * Time.fixedDeltaTime);

    // Law of Cosines for determining the angle of a joint with 3 known sides:
    // Arccos((AdjSideA^2 + AdjSideB^2 - OppSideC^2) / (2*AdjSideA*AdjSideB))
    float ElbowAngle = Mathf.Acos((a*a + b*b - c*c) / (2*a*b)) * (180f / 3.1415f);
    ElbowAngle = (a*a + b*b - c*c);
    ElbowAngle = ElbowAngle / (2*a*b);
    ElbowAngle = Mathf.Acos(ElbowAngle);
    ElbowAngle = ElbowAngle * (180 / Mathf.PI);
    Vector3 NewElbowRotation = BaseElbowRotation;

    float ElbowBend = 180 - ElbowAngle;
    if (ElbowBend > ElbowMinBend && ElbowBend < ElbowMaxBend)
    {
      Debug.Log("Positive");
      //Debug.Log("C Value: " + c);
      //ElbowBend = Mathf.Clamp(ElbowBend, PreviousAngle - MaxDistanceChangeSpeed*Time.fixedDeltaTime, PreviousAngle + MaxDistanceChangeSpeed*Time.fixedDeltaTime);
      NewElbowRotation.z = ElbowBend;
      PreviousAngle = ElbowBend;
    }
    else if (-ElbowBend > ElbowMinBend && -ElbowBend < ElbowMaxBend)
    {
      Debug.Log("Negative");
      //Debug.Log("C Value: " + c);
      //ElbowBend = Mathf.Clamp(ElbowBend, PreviousAngle - MaxDistanceChangeSpeed * Time.fixedDeltaTime, PreviousAngle + MaxDistanceChangeSpeed * Time.fixedDeltaTime);
      NewElbowRotation.z = -ElbowBend;
      PreviousAngle = ElbowBend;
    }
    else
    {
      Debug.Log("Neither");
      //Debug.Log("C Value: " + c);
      NewElbowRotation.z = ElbowMinBend;
      PreviousAngle = ElbowBend;
    }

    ElbowJoint.localRotation = Quaternion.Euler(NewElbowRotation);

    

    /***************************Shoulder Handling***************************/
    // a is already correct
    b = c;
    c = LALength;

    float ShoulderAngle = Mathf.Acos((a*a + b*b - c*c) / (2*a*b)) * (180f / Mathf.PI);
    //Debug.Log("Shoulder Angle: " + ShoulderAngle);
    Vector3 NewShoulderRotation = BaseShoulderRotation;
    float ShoulderBend = 180f - ShoulderAngle;
    NewShoulderRotation.z = 180f + ShoulderBend;
    ShoulderJoint.localRotation = Quaternion.Euler(NewShoulderRotation);
  }

  private Vector3 GetWristTarget(ArmIKTarget IKTarget)
  {
    Vector3 WristTarget;

    if (IKTarget.Distance > UALength + LALength - AntiJointLockAmount)
    {
      // This is the case scenario where the hand is touching something, but only slightly.
      Vector3 HandForwardVector = (FingerTip.position - WristJoint.position).normalized;

      if (IKTarget.Distance < UALength + LALength + WCLength - AntiJointLockAmount)
      {
        // This sub-case describes a situation where the hand is able to clasp something.
        //WristTarget = IKTarget.Location - DirectionVector * WCLength;
        WristTarget = IKTarget.Location - HandForwardVector * WCLength;
      }
      else
      {
        // This sub-case describes a sitation where the hand is just barely touching something. (Think finger-tips.)
        //WristTarget = IKTarget.Location - DirectionVector * HandLength;
        WristTarget = IKTarget.Location - HandForwardVector * HandLength;
      }
    }
    else
    {
      // This is the case scenario where the hand will be completely pressed up against the object.
      WristTarget = IKTarget.Location - DirectionVector * HandThickness;
      WristTarget -= Vector3.down * WCLength;
    }

    return WristTarget;
  }
}