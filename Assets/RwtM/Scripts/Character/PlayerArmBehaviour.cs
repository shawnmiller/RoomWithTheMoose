using UnityEngine;
using System.Collections;

public class PlayerArmBehaviour : MonoBehaviour {
  public Transform ShoulderJoint;
  public Transform ElbowJoint;
  public Transform WristJoint;

  public float ReturnSpeed=5f;

  private Vector3 ShoulderForward;

  private float MaxDistance;
  private float UALength;
  private float LALength;

  private Vector3 BaseElbowEuler;
  private Vector3 BaseShoulderEuler;

  private Plane ArmPlane;

	// Use this for initialization
	void Start () {
    if (!ShoulderJoint || !ElbowJoint || !WristJoint)
    {
      Debug.LogError("Missing Joint on Player");
      Debug.Break();
    }

    ArmPlane = new Plane(ShoulderJoint.position, ElbowJoint.position, WristJoint.position);

    UALength = Vector3.Distance(ShoulderJoint.position, ElbowJoint.position);
    LALength = Vector3.Distance(ElbowJoint.position, WristJoint.position);
    MaxDistance = UALength + LALength;

    ShoulderForward = (WristJoint.position - ShoulderJoint.position).normalized;

    BaseElbowEuler = ElbowJoint.localEulerAngles;
    BaseShoulderEuler = ShoulderJoint.localEulerAngles;
    Debug.Log("BaseElbowEuler Z: " + BaseElbowEuler.z);
    Debug.Log("BaseShoulderEuler Z: " + BaseShoulderEuler.z);

    Debug.Log("Angle of elbow: " + Quaternion.Angle(ShoulderJoint.rotation, ElbowJoint.rotation));
	}
	
	// Update is called once per frame
	void Update () {
    //if (!Input.GetKeyDown(KeyCode.G))
      //return;

    float CurrentShoulderAngle = Quaternion.Angle(ShoulderJoint.rotation, ElbowJoint.rotation);

    //Vector3 ForwardVector = (WristJoint.position - ShoulderJoint.position).normalized;

    RaycastHit Hit;
    if (Physics.Raycast(ShoulderJoint.position, ShoulderForward, out Hit, MaxDistance))
    {
      Debug.Log("Should Change Angle");
      Debug.Log("Distance to Target: " + Hit.distance);
      RepositionArm(Hit.distance);
    }
    else
    {
      ReturnToBaseRotation();
    }
	}

  void RepositionArm(float TargetDistance)
  {
    // Elbow
    float NewElbowAngle = Mathf.Acos((UALength*UALength + LALength*LALength - TargetDistance*TargetDistance) / (2*UALength*LALength));
    Debug.Log("Elbow Angle: " + NewElbowAngle);
    Vector3 NewElbowEuler = BaseElbowEuler;
    NewElbowEuler.z = NewElbowAngle+90f;
    ElbowJoint.localRotation = Quaternion.Euler(NewElbowEuler);

    // Shoulder
    float NewShoulderAngle = Mathf.Acos((UALength*UALength + TargetDistance*TargetDistance - LALength*LALength) / (2*UALength*TargetDistance));
    Debug.Log("Shoulder Angle: " + NewShoulderAngle);
    Vector3 NewShoulderEuler = BaseShoulderEuler;
    NewShoulderEuler.z = BaseShoulderEuler.z - 45f + NewShoulderAngle*100f*0.25f;
    Debug.Log("New ShoulderAngle: " + NewShoulderEuler.z);
    ShoulderJoint.localRotation = Quaternion.Euler(NewShoulderEuler);
  }

  void ReturnToBaseRotation()
  {
    ElbowJoint.localRotation = Quaternion.Euler(Vector3.Lerp(ElbowJoint.localEulerAngles, BaseElbowEuler, Time.deltaTime*ReturnSpeed));
    ShoulderJoint.localRotation = Quaternion.Euler(Vector3.Lerp(ShoulderJoint.localEulerAngles, BaseShoulderEuler, Time.deltaTime*ReturnSpeed));
  }
}
