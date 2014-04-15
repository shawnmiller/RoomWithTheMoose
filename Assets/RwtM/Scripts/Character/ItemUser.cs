using UnityEngine;

public class ItemUser : StateComponent
{
  public Transform Camera;
  public float MaxDistance = 5f;

  private bool isUsing;

  void Start()
  {
    base.Start();
  }

  void Update()
  {
    if (_state.State != StateType.In_Game || isUsing || !Input.GetKeyDown(KeyCode.Mouse0))
    {
      return;
    }

    RaycastHit hit;
    if (Physics.Raycast(Camera.position, Camera.forward, out hit, MaxDistance))
    {
      InteractibleItem item = hit.collider.gameObject.GetComponent<InteractibleItem>();
      if (item != null)
      {
        Debug.Log("Picked up item: " + item.name);
        item.Use();
      }
    }
  }
}