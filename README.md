# Flappy Bird Clone

---
finish date: 2025-01-10
aliases:
  - 2D
  - Basic
---

> Flappy Bird is a mobile game developed by Vietnamese video game artist and programmer Dong Nguyen. The game is a side-scroller where the player controls a bird, attempting to fly between columns of green pipes without hitting them.

### Importing Assets
- Download the zip file: https://github.com/zigurous/unity-flappy-bird-tutorial
- Go inside assets - sprites and put all the png file into sprites file, Font into Fonts file
- Bird
	- Change pixels per unit to **24**
	- Change the filter mode to **Point (no filter)** - maintaining pixel style graphics
	- Max size to **256**, Format to **RGBA 32 bit**


### Basic Setup
- Bird
	- Add **Player tag**
	- Add **Rigidbody 2D**
	    - **Body type:** Kinematic (does not simulate physics but can detect collisions)
	- Add **Collider 2D**

### Script Creation
- Create Script - Player
	- For reading player input and control the bird's movement
### Input And Movement

```
// Player.cs

public class Player: MonoBehaviour{
	private Vector 3 _direction;
	public float gravity = -9.8f;
	public float strength = 5f;

	// Update per frame
	private void Update(){
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
			// Vector3.up === Vector3(0, 1, 0)
			_direction = Vector3.up * strength;
		}
		
		// Mobile setup
		if (Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);

			// Tapping just start
			if (touch.phase == TouchPhase.Began){
				_direction = Vector3.up * strength;
			}
		}

		_direction.y += gravity * Time.deltaTime;
		transform.position += _direction * Time.deltaTime;
	}
}
```

### Sprite Animation
**Add variable and method in player script**
==***Add variable and method for player script, which looping image of sprite for animation***==

```
// Player.cs

private SpriteRenderer _spriteRenderer;
public Sprite[] sprites;
private int _spriteIndex;

	
private void Awake(){
	// Get Component of the object
	_spriteRenderer = GetComponent<SpriteRenderer>();
}	

// Call when first frame the object is available
private void Start(){
	// InvokeRepeating - calling method by waiting time and delay time
	InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
}

// Loop the sprite in Sprite[]
private void AnimateSprite(){
	_spriteIndex++;
	
	if (_spiteIndex >= Sprite.length){
		_spriteIndex = 0;
	}
	_spriteRenderer.sprite = sprites[_spriteIndex];
}
```

### Background Parallax
**Background**
- Create 3D object - Quad
	- Create a material
	- Shader - **Unit/Texture**
	- Remove **Collider**
	- Resize the **object**
	- Change the **tile mode of material**
	- Change the background asset wrap mode to **repeat**
**Ground**
- Create 3D object - Quad
	- Create a material
	- Shader - **Unit/Texture** 
	- Resize the **object**
	- Change the **tile mode of material**
	- Change the background asset wrap mode to **repeat**
- Create Script - Parallax
	- For both background and ground to loop the background



```
// Parallax.cs
public class Parallax: MonoBehaviour{
	private MeshRenderer _meshRenderer;
	public float animationSpeed = 0.5f;

	private void Awake(){
		_meshRenderer = GetComponenet<MeshRenderer>();
	}

	private void Update(){
		// Keep changing the offset of material to make it move
		_meshRenderer.material.mainTextureOffset += new Vector(animationSpeed * Time.deltaTime, 0);
	}
}
```

### Pipes Prefab 
- Create Pipe Object
	- Create a child GameObject - Sprite - UpperPipe
		- Put UpperPipe source image to it
	- Create a child GameObject - Sprite - MiddlePipe
		- Put UpperPipe source image to it
	- Create a child GameObject - Sprite - BottomPipe
		- Put UpperPipe source image to it
	- Create Script - PipesMovement
		- For looping the pipe to move from right

```
// PipesMovement.cs

public class PipesMovement: MonoBehaviour{
	public float speed = 5f;
	private float _leftEdge;

	private void Start(){
		// Get the Vector3.zero of camera point to world point, -1f is just offset of
		_leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
	}

	private void Update(){
		transform.position += Vector3.left * speed * Time.deltaTime;
	
		// Detect if pipes cross the leftEdge of the screen
		if(transform.position.x < leftEdge){
			Destroy(gameObject);
		}
	}
}	
```

### Pipe Spawner 
- Create a Empty GameObject - Pipe Spawner
- Create a Script - Spawner
	- For auto spawning pipes when game is not over

```
// Spawner.cs

public class Spawner: MonoBehaviour{
	public GameObject prefab;    // Store the prefab
	public float spawnRate = 1f;
	public float minHeight = -1f;
	public float maxHeight = 1f;

	// Can disable this when it's not use
	private void onEnable(){
		InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
	}

	private void onDisable(){
		CancelInvoke(nameof(Spawn));
	}

	private void Spawn(){
		// Instantiate object called pipes by coloning prefab, Quaternion.identity means no rotation
		GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
		pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
	}
}
```


### Game State & Scoring
- Create a Empty GameObject - Game Manager
- Create a Script - GameManager
	- For manage the whole logic of the game


```
// GameManager.cs

public class GameManager: Monobehaviour{
	private int _score;

	public void GameOver(){
	}
	
	public void IncreaseScore(){
		_score++;
	}
}
```

==After finish the create of game manager script, set tag of TopPipe, BottomPipe, and Ground to **Obstacle** - if the player hits, game over. Set tag of MiddlePipe to **Scoring**==

==Add method for the player script. When the player collider with game object with **obstacle** tag, it will call **gameover** function in gamemanager.==


```
// Player.cs

private void OnTriggerEnter2D(Collider2D other){
	if (other.gameObject.tag == "Obstacle"){
		// Always better function to use, but use it here first
		FindObjectOfType<GameManager>().GameOver();
	} else if (other.gameObject.tag == "Scoring"){
		FindObjectOfType<GameManager>().IncreaseScore();
	}
}
```

### UI Design
- Create a Canvas
    - **Canvas Scaler:** Scale with Screen Size
    - **Based on Height:** For some devices that are not 16/9
- Add a child UI Text element
    - Add outline (optional)
- Add a child UI Image element
    - Set native size - no stretch
- Add a child UI Button element

**Add variables and methods to GameManager script:**


```
// GameManager.cs

// reference to UI elements
public Player playerSprite;
public Text scoreText;	
public GameObject playbutton;
public GameObject gameOverImage;

private void Awake(){
	Application.targetFrameRate = 60;
	// When the game starts, pause the game until the player presses play
	Pause();
}

private void Play(){
	// initialize all the states
	score = 0;
	scoreText.text = score.ToString();
	
	playButton.setActive(false);		
	gameOverImage.setActive(false);
	
	Time.timeScale = 1f;
	player.enabled = true;

	// Destroy all the pipes
	Pipes[] pipes = FindObjectOfType<Pipes>();
	for (int i = 0; i < pipes.Length; i++){
	Destroy(pipes[i].gameObject);
	}
}

private void Pause(){
	// Time is not updating - no update method is running
	Time.timeScale = 0f;
	player.enabled = false;
}

// change method GameOver
public void GameOver(){
	gameOverImage.setActive(true);
	playButton.setActive(true);
	Pause();
}

// change method IncreaseScore
public void Increase(){
	score++;
	scoreText.text = score.toString();
}
```

**Add GameObject to playButton OnClick attribute and add play method**

**Add method in Player script:**

*Script - Player*
```
// Player.cs
private void OnEnable(){
	Vector3 position = transform.position;
	position.y = 0f;
	transform.position = position;
	// Direction also should be zero initially
	direction = Vector3.zero;
}
```
