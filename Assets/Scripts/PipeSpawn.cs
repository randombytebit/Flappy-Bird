using System.Threading;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {
   
  public GameObject pipe;
  public LogicScript logic;

  [SerializeField] private float _heightOffset = 10f;
  [SerializeField] private float _timer = 0f;
  [SerializeField] private float _spawnRate = 1f;

  private void Awake() {
    logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
  }

  private void Start() {
    SpawnPipe();
  }
  
  private void Update() {
    if (logic.numPassPipe > 10 && _spawnRate >= 0.5){
      _spawnRate -= (float)0.05;
    }
    if (_timer < _spawnRate) {
      _timer += Time.deltaTime;
    } else {
      SpawnPipe();
      _timer = 0;
    }
  }

  private void SpawnPipe(){
    Instantiate(pipe, new Vector2(transform.position.x, Random.Range(transform.position.y - _heightOffset, transform.position.y + _heightOffset)), transform.rotation);
  }

  private void OnEnable() {
    _spawnRate = 1f;
  }
}
