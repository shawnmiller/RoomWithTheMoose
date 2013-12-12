using UnityEngine;

public class LightSpawner : MonoBehaviour
{
  public GameObject LightPrefab;
  public float MinSpawnWaitTime;
  private float LastSpawnTime;
  private Transform cam;

  void Start()
  {
    cam = Camera.main.transform;
  }

  void Update()
  {
    if(!(Time.time > LastSpawnTime + MinSpawnWaitTime))
    {
      return;
    }

    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      RaycastHit hit;

      if (Physics.Raycast(cam.position, cam.forward, out hit, 5f))
      {
        SpawnLight(hit.point - cam.forward*0.5f);
      }
    }
  }

  /*void OnCollisionStay(Collision collision)
  {
    if (Time.time > LastSpawnTime + MinSpawnWaitTime)
    {
      SpawnLight(collision.contacts[0].point);
    }
  }
  void OnTriggerStay(Collider other)
  {
    if (Time.time > LastSpawnTime + MinSpawnWaitTime)
    {
      //SpawnLight(other);
    }
  }*/

  void SpawnLight(Vector3 position)
  {
    Instantiate(LightPrefab, position, Quaternion.identity);
    LastSpawnTime = Time.time;
  }
}