using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	private Rigidbody rb;

	public float tpLocationX;
	public float tpLocationY;
	public float tpLocationZ;
	public Vector3 tpLocation;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		tpLocation = new Vector3 (tpLocationX, tpLocationY, tpLocationZ);
	}


	void OnCollisionEnter(Collision other)
	{

		if(other.gameObject.CompareTag("Player"))
		{

			other.transform.position = tpLocation;
		}
	}
}