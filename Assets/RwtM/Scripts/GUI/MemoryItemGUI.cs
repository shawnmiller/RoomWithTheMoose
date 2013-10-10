using UnityEngine;

public class MemoryItemGUI : Singleton<MemoryItemGUI>
{
  public Transform spawnPoint;
  public float rotationSpeed;
  public Texture2D background;
  private MemoryItemData data;
  private GameObject display;
  private int currentPage;

  private GameState _state;

  void Start()
  {
    _state = GameState.Get();
  }

  public void Display(MemoryItemData data)
  {
    this.data = data;
    display = Instantiate(data.model, spawnPoint.transform.position, spawnPoint.rotation) as GameObject;
    _state.Transition(StateType.In_Game_Memory);
    camera.depth = 0;
  }

  void OnGUI()
  {
    if (_state.State != StateType.In_Game_Memory)
    {
      Destroy(display);
      return;
    }

    // Model rotation
    Vector3 rotation = display.transform.rotation.eulerAngles;
    rotation.y += rotationSpeed * Time.deltaTime;
    display.transform.rotation = Quaternion.Euler(rotation);

    // Close button
    Rect rect = new Rect(Screen.width - 100, 10, 90, 30);
    if (GUI.Button(rect, "Close"))
    {
      camera.depth = -2;
      Destroy(display);
      _state.Transition(StateType.In_Game);
    }

    // Back button
    if (currentPage != 0)
    {
      rect.x = (Screen.width/2f) - (Screen.width/6f);
      rect.y = Screen.height/4f - 30f;
      rect.width = 90;
      rect.height = 30f;
      if(GUI.Button(rect, "Prev"))
      {
        --currentPage;
      }
    }

    // Next button
    if (currentPage != data.pages.Length-1)
    {
      rect.x = Screen.width - (Screen.width / 3f) - 90;
      rect.y = Screen.height / 4f - 30f;
      rect.width = 90;
      rect.height = 30f;
      if (GUI.Button(rect, "Next"))
      {
        ++currentPage;
      }
    }

    // Write Text
    rect.x = (Screen.width/2f) - (Screen.width/6f);
    rect.y = Screen.height/4f;
    rect.width = Screen.width/3f;
    rect.height = Screen.height/2f;
    GUI.DrawTexture(rect, background);
    GUI.Label(rect, data.pages[currentPage]);
  }
}