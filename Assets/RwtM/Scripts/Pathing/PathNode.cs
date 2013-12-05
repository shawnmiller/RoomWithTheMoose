using UnityEngine;

public class PathNode : MonoBehaviour
{
  public string PathName;
  public PathNodeBehaviour NodeBehaviour;

  void OnDrawGizmos()
  {
    Gizmos.DrawSphere(transform.position, 0.2f);
  }
}