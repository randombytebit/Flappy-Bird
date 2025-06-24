using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour {
  
  public BirdMovement bird;
  public PipeSpawner pipeSpawner;
  public GameObject playButton;
  public GameObject gameOver;
  public Text scoreText;

  public int numPassPipe = 0;

  [SerializeField] private int _playerScore; 

  private void Awake() {
    Application.targetFrameRate = 60;
    Pause();
    gameOver.SetActive(false);
    playButton.SetActive(true);
  }

  public void Play() {
    _playerScore = 0;
    scoreText.text = _playerScore.ToString();

    playButton.SetActive(false);
    gameOver.SetActive(false);

    Time.timeScale = 1f;
    bird.enabled = true;
    pipeSpawner.enabled = true;

    GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipes");
    for (int i = 0; i < pipes.Length; i++) {
      Destroy(pipes[i].gameObject);
      PipeMovement pipeMovement = pipes[i].GetComponent<PipeMovement>();
      pipeMovement.ResetMoveSpeed();  
    }
  }

  public void Pause() {
    Time.timeScale = 0f;
    bird.enabled = false;
    pipeSpawner.enabled = false;
  }

  public void AddScore() {
    _playerScore = (numPassPipe <= 5) ? _playerScore + 1 : _playerScore + 3;
    scoreText.text = _playerScore.ToString();
  }

  public void GameOver(){
    AudioPlayer.Instance.PlayAudio(0);
    gameOver.SetActive(true);
    playButton.SetActive(true);
    numPassPipe = 0;
    Pause();
  }
}
