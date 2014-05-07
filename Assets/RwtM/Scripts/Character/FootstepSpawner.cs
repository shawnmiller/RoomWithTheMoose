using UnityEngine;

public class FootstepSpawner : MonoBehaviour
{
  public GameObject FootstepPrefab;

  public float SpawnDistance;
  public Material MaterialInstance;

  private Vector3 LastStepSpawn;

  void Start()
  {
    RaycastHit Hit;

    if (Physics.Raycast(transform.position, Vector3.down, out Hit))
    {
      LastStepSpawn = Hit.point;
    }
    else
    {
      LastStepSpawn = Vector3.zero;
    }
  }

  void Update()
  {
    RaycastHit Hit;

    if (Physics.Raycast(transform.position, Vector3.down, out Hit))
    {
      if (Vector3.Distance(LastStepSpawn, Hit.point) > SpawnDistance)
      {
        MakeFootstep(Hit.point);
      }
    }
  }

  private void MakeFootstep(Vector3 Location)
  {
    LastStepSpawn = Location;
    Location.y += 0.05f;
    Instantiate(FootstepPrefab, Location, Quaternion.Euler(90, 0, 0));
  }
}