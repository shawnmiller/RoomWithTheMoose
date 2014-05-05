using UnityEngine;

public class ArmIKTarget
{
  public Vector3 Location;
  public float Distance;
  public Vector3 Normal;
  public bool UseNormal; // Should Normal be considered for this IKTarget?
  public bool IsTouching;
}