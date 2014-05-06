using UnityEngine;

public class ShaderManager : Singleton<ShaderManager>
{
  public const int FINGERS = 0;
  public const int BODY = 1;

  public Material TouchMaterial;
  public Color NormalColor;
  public Color KeyItemColor;

  public string HandJointName;
  /*private Transform[] FingerTips;
  private Matrix4x4 FingerPositions;

  public string[] BodyJointNames;
  public float[] JointStrengthModifiers;
  private Transform[] BodyPoints;
  private Matrix4x4 BodyPositions;*/

  private Transform Hand;

  void Start()
  {
    /*FingerTips = new Transform[4];
    FingerPositions = new Matrix4x4();

    BodyPoints = new Transform[4];
    BodyPositions = new Matrix4x4();*/

    Hand = GameObject.Find(HandJointName).transform;

    /*Transform HandJoint = GameObject.Find(HandJointName).transform;
    int ActualIndex = 0;

    for (int i = 0; i < HandJoint.childCount - 1; ++i)
    {
      Transform CurrentFinger = HandJoint.GetChild(i).GetChild(0).GetChild(0);
      
      if (CurrentFinger.name.Contains("Thumb"))
      {
        continue;
      }
      FingerTips[ActualIndex] = CurrentFinger;
      Debug.Log("Set " + CurrentFinger.name + " to finger array " + ActualIndex);
      ++ActualIndex;
    }

    if (BodyJointNames == null || BodyJointNames.Length == 0)
    {
      Debug.LogWarning("No additional joint positions defined for the character.");
    }
    if(BodyJointNames.Length > 3)
    {
      Debug.LogWarning("More than 3 additional joints were defined, only the first 3 will be used.");
    }
    if (JointStrengthModifiers.Length != BodyJointNames.Length)
    {
      Debug.LogError("Different number of joints specified than multipliers");
    }
    try
    {
      for (int j = 0; j < 4; ++j)
      {
        if (BodyJointNames[j] == null || BodyJointNames[j] == "")
        {
          break;
        }

        BodyPoints[j] = GameObject.Find(BodyJointNames[j]).transform;
        if (BodyPoints[j] == null)
        {
          Debug.LogError("Couldn't find joint " + BodyJointNames[j] + ". Make sure you spelled it correctly and that it is in the level.");
        }
      }
    }
    catch { } // We just don't want to deal with a null ref exception, this is fine.*/
  }

  /*void Update()
  {
    for (int i = 0; i < FingerTips.Length; ++i)
    {
      FingerPositions.SetRow(i, new Vector4(FingerTips[i].position.x,
                                            FingerTips[i].position.y,
                                            FingerTips[i].position.z,
                                            1f));
    }

    for (int j = 0; j < BodyPoints.Length; ++j)
    {
      BodyPositions.SetRow(j, new Vector4(BodyPoints[j].position.x,
                                          BodyPoints[j].position.y,
                                          BodyPoints[j].position.z,
                                          JointStrengthModifiers[j]));
    }
  }*/

  /*public Vector3 GetPosition()
  {
    if (player != null)
    {
      return player.position;
    }

    return Vector3.zero;
  }*/

  public Vector4 GetHandPosition()
  {
    return new Vector4(Hand.position.x, Hand.position.y, Hand.position.z, 0f);
  }

  /*public Matrix4x4 GetLocationMatrix(int Index)
  {
    if (Index == FINGERS)
    {
      return FingerPositions;
    }
    if (Index == BODY)
    {
      return BodyPositions;
    }
    return Matrix4x4.zero;
  }*/
}