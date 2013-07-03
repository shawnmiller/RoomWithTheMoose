using UnityEngine;
using System.Collections;

public class DistanceComparer : IComparer
{
  private GameObject target;

  public void SetTarget(GameObject target)
  {
    this.target = target;
  }

  int IComparer.Compare(object a, object b)
  {
    float dToA = Vector3.Magnitude(target.transform.position - ((GameObject)a).transform.position);
    float dToB = Vector3.Magnitude(target.transform.position - ((GameObject)b).transform.position);

    if (dToA < dToB)
    {
      return -1;
    }
    if (dToA > dToB)
    {
      return 1;
    }
    return 0;
  }
}