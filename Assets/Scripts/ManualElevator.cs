using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualElevator : MonoBehaviour {

	public float hMin;
	public float hMax;
	public bool up;
	private bool hasPlayer;
	public bool triggered;
	public float speed = 5;

	void Update()
	{
		
		if (triggered == true) {
			if (up == true) 
				transform.Translate (new Vector3 (0, speed, 0) * Time.deltaTime);
			if (up == false) 
				transform.Translate (new Vector3 (0, -speed, 0) * Time.deltaTime);
		}
		if (transform.position.y <= hMin) {
			up = true;
			triggered = false;
		}
		if (transform.position.y >= hMax && triggered == true) {
			up = false;
			triggered = false;
		}
	}

}
