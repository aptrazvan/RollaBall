using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorY : MonoBehaviour {

	public float hMin;
	public float hMax;
	public bool up;
	public float speed;

	void Update()
	{
		if (transform.position.y <= hMin)
			up = true;
		if (transform.position.y >= hMax)
			up = false;
		if (up == true)
			transform.Translate (new Vector3 (0, speed, 0) * Time.deltaTime);
		if (up == false)
			transform.Translate (new Vector3 (0, -speed, 0) * Time.deltaTime);
	}
}
