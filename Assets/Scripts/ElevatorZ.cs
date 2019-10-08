using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorZ : MonoBehaviour {

	public float zMin;
	public float zMax;
	public bool forward;
	public float speed = 3;

	void Update()
	{
		if (transform.position.z <= zMin)
			forward = true;
		if (transform.position.z >= zMax)
			forward = false;
		if (forward == true) 
			transform.Translate (new Vector3 (0, 0, speed) * Time.deltaTime);
		if (forward == false) 
			transform.Translate (new Vector3 (0, 0, -speed) * Time.deltaTime);
	}
}
