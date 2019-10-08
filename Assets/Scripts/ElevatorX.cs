using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorX : MonoBehaviour {

	public float xMin;
	public float xMax;
	public bool right;
	public float speed = 3;

	void Update()
	{
		if (transform.position.x <= xMin)
			right = true;
		if (transform.position.x >= xMax)
			right = false;
		if (right == true) 
			transform.Translate (new Vector3 (speed, 0, 0) * Time.deltaTime);
		if (right == false) 
			transform.Translate (new Vector3 (-speed, 0, 0) * Time.deltaTime);
	}




		
}
