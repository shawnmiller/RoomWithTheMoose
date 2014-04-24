using UnityEngine;

public class SoundObj : DynamicObject
{
  public string Actor;
  public AudioClip Sound;

  public string Path
  {
    set
    {
      //Sound = Resources.LoadAssetAtPath(value, typeof(AudioClip)) as AudioClip;
      Sound = Resources.Load(value, typeof(AudioClip)) as AudioClip;
      Debug.Log("Sound Asset \"" + value + "\" Found: " + (Sound != null));
    }
  }
}