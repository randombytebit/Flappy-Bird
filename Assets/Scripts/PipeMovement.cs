using UnityEngine;

public class PipeMovement : MonoBehaviour {
  [SerializeField] private float _moveSpeed = 12f;
  public LogicScript logic;

  private void Awake() {
    logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
  }

  private void Update() {
    MovePipe();
    DeletePipe();
  }

  private void MovePipe() {
    if (logic.numPassPipe > 10) {
      _moveSpeed += (float)0.25;
      transform.position = transform.position + (Vector3.left * _moveSpeed) * Time.deltaTime;
    } else {
      transform.position = transform.position + (Vector3.left * _moveSpeed) * Time.deltaTime;
    }
  }

  private void DeletePipe() {
    if (transform.position.x < -30) {
      Destroy(gameObject);
    }
  }

  public void ResetMoveSpeed() {
    _moveSpeed = 12f;
  }
}
