using UnityEngine;

public class ItemUser : StateComponent
{
  public Transform camera;
  public float maxDistance = 5f;

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
    if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance))
    {
      Debug.Log("Hit something with our pointy thing");
      MemoryItem item = hit.collider.gameObject.GetComponent<MemoryItem>();
      if (item != null)
      {
        Debug.Log("Hit a memory item with our pointy thing");
        isUsing = true;
        MemoryItemGUI memGUI = MemoryItemGUI.Get();
        memGUI.Display(item.data);
      }
    }
  }
}