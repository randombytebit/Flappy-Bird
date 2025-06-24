using UnityEngine;

public class AudioPlayer : MonoBehaviour {
  public static AudioPlayer Instance;
  public AudioSource src; 
  public AudioClip[] clips;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
    } else {
      Destroy(gameObject); 
    }
  }

  public void PlayAudio(int clipIndex){
    src.clip = clips[clipIndex];
    src.Play();
  }

}
