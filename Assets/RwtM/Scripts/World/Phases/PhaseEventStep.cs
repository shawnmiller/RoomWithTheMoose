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
        Debug.Log("Cannot find Action: " + Action);
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
      Debug.Log("No trigger named \"" + Name + "\" found in the scene.");
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
  }

  private void SetValue()
  {
    Variable var = ObjectController.Get().GetObject<Variable>(ObjectCategories.Variable, Name);
    var.Value = Value;
  }

  private void ToggleBool()
  {
    Variable var = ObjectController.Get().GetObject<Variable>(ObjectCategories.Variable, Name);
    var.Value = TypeConversion.Convert(typeof(bool), Value.ToString());
  }

  private void SetInteractible(bool toggle)
  {
    GameObject item = GetSceneObject();
    if (toggle == true)
    {
      if(item.GetComponent<InteractibleItem>() == null)
        item.AddComponent<InteractibleItem>();
    }
    else
    {
      GameObject.Destroy(item.GetComponent<InteractableItem>());
    }
  }

  private void PlaySound()
  {
    SoundObj sound = ObjectController.Get().GetObject<SoundObj>(ObjectCategories.Sound, Name);
    GameObject actor = GameObject.Find(Actor);
    if (sound == null)
    {
      Debug.Log("Cannot find sound: " + Name);
    }
    if (actor == null)
    {
      Debug.Log("Cannot find actor: " + Actor);
    }

    actor.AddComponent<AudioSource>().PlayOneShot(sound.Sound);
  }

  private void PlayLegacyEvent()
  {
    GameObject eventObj = GetSceneObject();
    Event evnt = eventObj.GetComponent<Event>();
    evnt.Activate();
  }

  private GameObject GetSceneObject()
  {
    GameObject obj = GameObject.Find(Name);
    if (obj == null)
    {
      Debug.Log("No trigger named \"" + Name + "\" found in the scene.");
      Debug.Break();
    }
    return obj;
  }
}