using UnityEngine;

public class MemoryItemGUI : StateComponent
{
  private MemoryItemData data;
  private GameObject display;
  private int currentPage;

  void Start()
  {
    base.Start();
  }

  public void Display(MemoryItemData data)
  {
    this.data = data;
  }

  void OnGUI()
  {
    if (_state.State != StateType.In_Game_Memory)
    {
      return;
    }


  }
}