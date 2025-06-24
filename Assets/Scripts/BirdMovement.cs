using UnityEngine;

public class BirdMovement : MonoBehaviour {
  
  private Rigidbody2D _rb;
  private SpriteRenderer _spriteRenderer;

  public Sprite[] sprites;
  private LogicScript _logic;
  private int _spriteIndex = 0;

  [SerializeField] private float _jumpPower = 10f;

  private void Awake() {
    _logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    _rb = GetComponent<Rigidbody2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Start() {
    InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
  }

  private void Update() {
      Jump();
  }

  private void OnEnable() {
    transform.position = Vector3.zero;
  }

  private void Jump() {
    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
        _rb.linearVelocity = Vector2.up * _jumpPower;
        AudioPlayer.Instance.PlayAudio(4);
    }
  }

  private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == "Obstacle") {
      AudioPlayer.Instance.PlayAudio(1);
      _logic.GameOver();
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Scoring") {
      _logic.AddScore();
    }
  }

  private void AnimateSprite() {
    _spriteIndex++;

    if (_spriteIndex >= sprites.Length) {
      _spriteIndex = 0;
    }

    _spriteRenderer.sprite = sprites[_spriteIndex];
  }
}
