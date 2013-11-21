using UnityEngine;

public class AmbientTrackManager : Singleton<AmbientTrackManager>
{
  public AudioClip[] tracks;
  private GameState state;
  private AudioSource player;

  void Start()
  {
    state = GameState.Get();
    GameObject p = GameObject.FindGameObjectWithTag("Player");
    player = p.audio;
  }

  void Update()
  {
    if (!(state.State == StateType.In_Game) &&
       !(state.State == StateType.In_Game_Memory))
    {
      return;
    }

    if (!player.isPlaying)
    {
      player.clip = tracks[0];
      player.Play();
    }
  }
}