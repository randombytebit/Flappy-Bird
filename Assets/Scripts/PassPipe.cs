using UnityEngine;

public class PassPipe : MonoBehaviour {

  private LogicScript _logic;
  
  private void Awake() {
    _logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      _logic.numPassPipe++;
      AudioPlayer.Instance.PlayAudio(2);
      
    }
  }
}
