using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
	public GameObject backGround;
	private GameObject[] elevatorsUpPos;
    public Text countText;
	public Text required;
	public Text livesText;
	public Text menuText;
    public Text winText;
	public Text loseText;
	public Text morePickups;
    private int count;
	private int lives;
	public AudioClip jumpSound;
	public AudioClip hitSound;
	public AudioSource rbAudio;
	public ManualElevator elevatorUp;
	private bool playSound;
	private float maxSpeed = 15;
	public Camera MyCamera;
	bool OnFloor = true;
	bool resPress = false;
	private float x = 0.0f;
    private float multiplier;
	private Vector3 checkpoint;
	private Vector3 cPos;
	bool Win = false;
	public Button resume;
	public Button nextLevel;
	public Button restart;
	public Button lobby;
	public Button exit;
	private bool BPress = false;
	public float minPickups;
	public string currentLevel;
	public float maxPickups;
	private bool ePos = true;
	private bool winLocation = false;
	public JoystickController joystick;
	private static float timeScale;
	public bool lobbyLevel = false;

    private void Start()
    {
		Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
		checkpoint = new Vector3 (rb.transform.position.x, rb.transform.position.y + 1, rb.transform.position.z);
        count = 0;
		lives = 3;
		menuText.text = "";
		resume.gameObject.SetActive (false);
		restart.gameObject.SetActive (false);
		exit.gameObject.SetActive (false);
		nextLevel.gameObject.SetActive (false);
		lobby.gameObject.SetActive (false);
		SetCountText();
		winText.text = "";
		morePickups.text = "";
		loseText.text = "";
    }

	void Update()
	{
		RaycastHit RHit;
		Ray MyRay = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down*3);
		if (Physics.Raycast(MyRay, out RHit, 0.6145f))
		{
			if (RHit.collider.gameObject.tag == "Floor" || RHit.collider.gameObject.tag == "Checkpoint" || RHit.collider.gameObject.tag == "Elevator" || RHit.collider.gameObject.tag == "ManualElevator")
			{
				OnFloor = true;

			}
		}
		if (rb.transform.position.y < (backGround.transform.position.y + 30) && Win == false) 
		{
			lives--;
			SetCountText ();
			if (lives > 0 || lobbyLevel == true) {
				rb.transform.position = checkpoint;
				rb.velocity = rb.velocity.normalized;
			} else 
			{
				Win = true;
				loseText.text = "No more lives!";
				restart.gameObject.SetActive (true);
				exit.gameObject.SetActive (true);
				lobby.gameObject.SetActive (true);
			}
		}
		if (count >= minPickups && winLocation == true)
		{
			winText.text = "Level Complete!";
			Win = true;
			nextLevel.gameObject.SetActive (true);
			restart.gameObject.SetActive (true);
			lobby.gameObject.SetActive (true);
			exit.gameObject.SetActive (true);
		}
		if (winLocation == true && count < minPickups)
		{
			morePickups.text = "Not enough Pickups!";
			Win = true;
			restart.gameObject.SetActive (true);
			lobby.gameObject.SetActive (true);
			exit.gameObject.SetActive (true);
		}

		if (Input.GetButtonUp ("Cancel") && Win == false) 
			if (Time.timeScale == 1) 
			{
				Time.timeScale = 0;
				menuText.text = "Menu";
				resume.gameObject.SetActive (true);
				restart.gameObject.SetActive (true);
				exit.gameObject.SetActive (true);
				lobby.gameObject.SetActive (true);
			} 

			if(resPress == true)
			    {
				    resPress = false;
					Time.timeScale = 1;
				    menuText.text = "";
				    resume.gameObject.SetActive (false);
					restart.gameObject.SetActive (false);
					exit.gameObject.SetActive (false);
				    lobby.gameObject.SetActive (false);
				}
		elevatorsUpPos = GameObject.FindGameObjectsWithTag ("ManualElevator");
		foreach (GameObject elevatorUpPos in elevatorsUpPos) 
		{
			if (rb.transform.position.y < elevatorUpPos.transform.position.y - 1 && Mathf.Abs(Mathf.Abs(rb.transform.position.x)+Mathf.Abs(rb.transform.position.z)-Mathf.Abs(elevatorUpPos.transform.position.x)-Mathf.Abs(elevatorUpPos.transform.position.z)) < 10 && elevatorUp.up == false)
				elevatorUp.triggered = true;
		}
		
	}

    private void FixedUpdate()
    {
        multiplier = (MyCamera.transform.position.y - rb.transform.position.y)/3;
       // float moveHorizontal = Input.GetAxis("Horizontal");
       // float moveVertical = Input.GetAxis("Vertical");

		float moveHorizontal = joystick.Horizontal ();
		float moveVertical = joystick.Vertical ();


		if (winLocation == false) 
		{
			Vector3 movement = MyCamera.transform.forward * moveVertical * 600 * Time.deltaTime;
			Vector3 test = new Vector3 (movement.x, 0f, movement.z);
			if (Mathf.Abs(multiplier) > 1.4 && Mathf.Abs(multiplier) < 1.8)
				rb.AddForce (test * (Mathf.Abs(multiplier)) / 1.3f);
			else if (Mathf.Abs(multiplier) > 1 && Mathf.Abs(multiplier) < 2.15)
					rb.AddForce (test * (Mathf.Abs(multiplier)) / 1.1f);
			else if (Mathf.Abs(multiplier) > 2.15 && Mathf.Abs(multiplier) < 2.3)
				rb.AddForce (test * 2.8f);
			else if (Mathf.Abs(multiplier) > 2.3 && Mathf.Abs(multiplier) < 2.4)
				rb.AddForce (test * 3.5f);
			else if (Mathf.Abs(multiplier) > 2.4 && Mathf.Abs(multiplier) < 2.47)
				rb.AddForce (test * 5);
			else if (Mathf.Abs(multiplier) > 2.47 && Mathf.Abs(multiplier) < 2.49)
				rb.AddForce (test * 10f);
			else if (Mathf.Abs(multiplier) > 2.49)
				rb.AddForce (test * 18f);
			else
				rb.AddForce (test);

			movement = MyCamera.transform.right * moveHorizontal * 600 * Time.deltaTime;
			test = new Vector3 (movement.x, 0f, movement.z);
			rb.AddForce (test);
			if(rb.velocity.magnitude > maxSpeed && rb.velocity.y <7 && rb.velocity.y > -7)
			{
				
					rb.velocity = rb.velocity.normalized * maxSpeed;
				
			}
			rbAudio.volume = rb.velocity.magnitude / 15;
			if (OnFloor == true && !rbAudio.isPlaying)
				rbAudio.Play();
			if (OnFloor == false || playSound==false)
				rbAudio.Stop ();

		}
		if (playSound==true && BPress == true) 
		{
			Vector3 juump = new Vector3 (0.0f, 6, 0.0f);
			rb.AddForce (juump * 50);
			OnFloor = false;
			BPress = false;
			AudioSource.PlayClipAtPoint (jumpSound, rb.transform.position, 1);
		}
    }



    void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
           
		} else if (other.gameObject.CompareTag ("Life")) 
		{
			other.gameObject.SetActive (false);
			lives++;
			SetCountText ();
		}
		if (other.gameObject.CompareTag ("JumpingPlatform")) 
		{
			if (Mathf.Abs (rb.velocity.y) <= 15) {
				if (other.transform.rotation == Quaternion.Euler (30, 0, 0))
					rb.velocity = new Vector3 (rb.velocity.x, -(rb.velocity.y) * 1.15f * 1.73f / 2, -rb.velocity.y * 1.15f / 2);
				else if (other.transform.rotation == Quaternion.Euler (-30, 0, 0))
					rb.velocity = new Vector3 (rb.velocity.x, -(rb.velocity.y) * 1.15f * 1.73f / 2, rb.velocity.y * 1.15f / 2);
				else if (other.transform.rotation == Quaternion.Euler (0, 0, 30))
					rb.velocity = new Vector3 (rb.velocity.y * 1.15f / 2, -(rb.velocity.y) * 1.15f * 1.73f / 2, rb.velocity.z);
				else if (other.transform.rotation == Quaternion.Euler (0, 0, -30))
					rb.velocity = new Vector3 (-rb.velocity.y * 1.15f / 2, -(rb.velocity.y) * 1.15f * 1.73f / 2, rb.velocity.z);
				else
					rb.velocity = new Vector3 (rb.velocity.x, -(rb.velocity.y) * 1.1f, rb.velocity.z);
			} else 
			{
				if (other.transform.rotation == Quaternion.Euler (30, 0, 0))
					rb.velocity = new Vector3 (rb.velocity.x, 12.975f, -rb.velocity.y * 1.15f / 2);
				else if (other.transform.rotation == Quaternion.Euler (-30, 0, 0))
					rb.velocity = new Vector3 (rb.velocity.x, 12.975f, rb.velocity.y * 1.15f / 2);
				else if (other.transform.rotation == Quaternion.Euler (0, 0, 30))
					rb.velocity = new Vector3 (rb.velocity.y * 1.15f / 2, 12.975f, rb.velocity.z);
				else if (other.transform.rotation == Quaternion.Euler (0, 0, -30))
					rb.velocity = new Vector3 (-rb.velocity.y * 1.15f / 2, 12.975f, rb.velocity.z);
				else
				rb.velocity = new Vector3 (rb.velocity.x, 15, rb.velocity.z);
			}
		}

	}
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Wall")||other.gameObject.CompareTag("HighWall")||playSound==false)
		AudioSource.PlayClipAtPoint (hitSound, rb.transform.position, rb.velocity.magnitude/15);
		if(other.gameObject.CompareTag("Checkpoint"))
			{
			checkpoint = new Vector3 (other.transform.position.x, other.transform.position.y + 1 , other.transform.position.z);
			}

		if(other.gameObject.CompareTag("Win Platform"))
		{

			winLocation = true;
		}
		if(other.gameObject.CompareTag("ManualElevator"))
			elevatorUp.triggered = true;
	}

	void OnCollisionStay(Collision other)
	{
		if(other.gameObject.CompareTag("Elevator") || other.gameObject.CompareTag("ManualElevator") )
			rb.transform.parent = other.transform.parent;
		if (other.gameObject.CompareTag ("Floor") || other.gameObject.CompareTag ("ManualElevator") || other.gameObject.CompareTag ("Elevator") || other.gameObject.CompareTag ("Checkpoint") || other.gameObject.CompareTag ("Finish") || other.gameObject.CompareTag ("Wall")) {
			playSound = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.CompareTag("Elevator") || other.gameObject.CompareTag("ManualElevator"))
			rb.transform.parent = null;
		if(other.gameObject.CompareTag("Floor")|| other.gameObject.CompareTag("ManualElevator")|| other.gameObject.CompareTag("Elevator")|| other.gameObject.CompareTag("Checkpoint")|| other.gameObject.CompareTag("Finish")|| other.gameObject.CompareTag("Wall"))
			playSound = false;
	}



		

	public void PlayerJump()
	{
		if(playSound == true)
		BPress = true;
	}

	public void Resume()
	{
		resPress = true;
	}
    
    void SetCountText()
    {
		countText.text = "Count: " + count.ToString() + "/" + maxPickups.ToString();
		required.text = "Required: " + minPickups.ToString ();
		livesText.text = "Lives: " + lives.ToString ();

    }
}
