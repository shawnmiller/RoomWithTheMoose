using UnityEngine;

public class PhaseEventStep
{
  public string Action { get; set; }
  public string Name { get; set; }
  public string Actor { get; set; }
  public float Value { get; set; }

  public void Run()
  {
    switch (Action)
    {
      case PPS.PP_ACTION_END_GAME:
        EndGame();
        break;
      case PPS.PP_ACTION_DISABLE_TRIGGER:
        ToggleTrigger(false);
        break;
      case PPS.PP_ACTION_ENABLE_TRIGGER:
        ToggleTrigger(true);
        break;
      case PPS.PP_ACTION_BEGIN_TIMER:
        BeginTimer();
        break;
      case PPS.PP_ACTION_INCREMENT_VALUE:
        IncrementValue();
        break;
      case PPS.PP_ACTION_SET_VALUE:
        SetValue();
        break;
      case PPS.PP_ACTION_TOGGLE_BOOL:
        ToggleBool();
        break;
      case PPS.PP_ACTION_MAKE_INTERACTIBLE:
        SetInteractible(true);
        break;
      case PPS.PP_ACTION_REMOVE_INTERACTIBLE:
        SetInteractible(false);
        break;
      case PPS.PP_ACTION_END_PHASE:
        PhaseManager.Get().MoveToNextPhase();
        break;
      case PPS.PP_ACTION_LEGACY_EVENT:
        PlayLegacyEvent();
        break;
      case PPS.PP_ACTION_PLAY_SOUND:
        Debug.Log("Playing Sound");
        PlaySound();
        break;
      case PPS.PP_ACTION_WAIT:
        Debug.Log("Action: Wait is NYI");
        break;
      default:
        Debug.LogError("Cannot find Action: " + Action);
        Debug.Break();
        break;
    }
  }

  private void EndGame()
  {
    PhaseManager.Get().EndGame();
  }

  private void ToggleTrigger(bool toggle)
  {
    GameObject trigger = GetSceneObject();
    if (trigger == null)
    {
      Debug.LogError("No trigger named \"" + Name + "\" found in the scene.");
      return;
    }

    TriggerDispatch dispatch = trigger.GetComponent<TriggerDispatch>();
    dispatch.Enabled = toggle;
  }

  private void BeginTimer()
  {
    TimerManager.Get().StartTimer(Name);
  }

  private void IncrementValue()
  {
    Variable var = ObjectController.Get().GetObject<Variable>(ObjectCategories.Variable, Name);
    var.Value = (float)var.Value + Value;
    Debug.Log("Value Incremented: " + var.Name + " Value: " + var.Value.ToString());
  }

  private void SetValue()
  {
    Variable var = ObjectController.Get().GetObject<Variable>(ObjectCategories.Variable, Name);
    var.Value = Value;
    Debug.Log("Value Set: " + var.Name + " Value: " + var.Value.ToString());
  }

  private void ToggleBool()
  {
    Variable var = ObjectController.Get().GetObject<Variable>(ObjectCategories.Variable, Name);
    var.Value = TypeConversion.Convert(typeof(bool), Value.ToString());
    Debug.Log("Bool Toggled: " + var.Name + " Value: " + var.Value.ToString());
  }

  private void SetInteractible(bool toggle)
  {
    GameObject item = GetSceneObject();
    if (toggle == true)
    {
      if (item.GetComponent<InteractibleItem>() == null)
      {
        item.AddComponent<InteractibleItem>();
        Debug.Log("Item Given Interaction: " + item.name);
      }
    }
    else
    {
      GameObject.Destroy(item.GetComponent<InteractableItem>());
      Debug.Log("Item Interaction Removed: " + item.name);
    }
  }

  private void PlaySound()
  {
    SoundObj sound = ObjectController.Get().GetObject<SoundObj>(ObjectCategories.Sound, Name);
    GameObject actor = GameObject.Find(Actor);
    if (sound == null)
    {
      Debug.LogError("Cannot find sound: " + Name);
      //Debug.Break();
    }
    if (actor == null)
    {
      Debug.LogError("Cannot find actor: " + Actor);
      //Debug.Break();
    }

    try
    {
      Debug.Log("Within PlaySound try block");
      actor.AddComponent<AudioSource>().PlayOneShot(sound.Sound);
    }
    catch { } // We already know what this could be, we don't care, dev will get the warning in console already.
  }

  private void PlayLegacyEvent()
  {
    GameObject eventObj = GetSceneObject();
    Event evnt = eventObj.GetComponent<Event>();
    evnt.Activate();
    Debug.Log("Playing Legacy Event: " + eventObj.name);
  }

  private GameObject GetSceneObject()
  {
    GameObject obj = GameObject.Find(Name);
    if (obj == null)
    {
      Debug.LogError("No object named \"" + Name + "\" found in the scene.");
      Debug.Break();
    }
    return obj;
  }
}