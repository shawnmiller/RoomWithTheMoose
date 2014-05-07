using UnityEngine;

public class PhaseEventStep
{
  public string Action { get; set; }
  public string Name { get; set; }
  public string Actor { get; set; }
  public float Value { get; set; }

  // Hacked in for items
  public bool Persistent { get; set; }
  public int Uses { get; set; }

  // Hacked in for sounds
  public bool Loop { get; set; }

  public void Run()
  {
    switch (Action)
    {
      case PP.ACTION_END_GAME:
        EndGame();
        break;
      case PP.ACTION_DISABLE_TRIGGER:
        ToggleTrigger(false);
        break;
      case PP.ACTION_ENABLE_TRIGGER:
        ToggleTrigger(true);
        break;
      case PP.ACTION_BEGIN_TIMER:
        BeginTimer();
        break;
      case PP.ACTION_INCREMENT_VALUE:
        IncrementValue();
        break;
      case PP.ACTION_SET_VALUE:
        SetValue();
        break;
      case PP.ACTION_TOGGLE_BOOL:
        ToggleBool();
        break;
      case PP.ACTION_MAKE_INTERACTIBLE:
        SetInteractible(true);
        break;
      case PP.ACTION_REMOVE_INTERACTIBLE:
        SetInteractible(false);
        break;
      case PP.ACTION_END_PHASE:
        Debug.Log("Moving to next Phase");
        PhaseManager.Get().MoveToNextPhase();
        break;
      case PP.ACTION_LEGACY_EVENT:
        PlayLegacyEvent();
        break;
      case PP.ACTION_PLAY_SOUND:
        Debug.Log("Playing Sound");
        PlaySound();
        break;
      case PP.ACTION_STOP_SOUND:
        StopSound();
        break;
      case PP.ACTION_WAIT:
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
        InteractibleItem iItem = item.AddComponent<InteractibleItem>();
        iItem.Persistent = Persistent;
        iItem.MessageLimit = Uses;
        Debug.Log("Item Given Interaction: " + item.name);
      }
    }
    else
    {
      GameObject.Destroy(item.GetComponent<InteractibleItem>());
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
      AudioSource aSource;

      if (actor.audio && !actor.audio.isPlaying)
      {
        aSource = actor.audio;
      }
      else
      {
        aSource = actor.AddComponent<AudioSource>();
      }

      aSource.loop = Loop;
      aSource.clip = sound.Sound;
      aSource.Play();
    }
    catch { } // We already know what this could be, we don't care, dev will get the warning in console already.
  }

  private void StopSound()
  {
    GameObject actor = GameObject.Find(Actor);
    AudioSource[] aSources = actor.GetComponents<AudioSource>();
    SoundObj sound = ObjectController.Get().GetObject<SoundObj>(ObjectCategories.Sound, Name);

    for (int i = 0; i < aSources.Length; ++i)
    {
      if (aSources[i].clip.name == sound.Sound.name)
      {
        aSources[i].Stop();
      }
    }
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