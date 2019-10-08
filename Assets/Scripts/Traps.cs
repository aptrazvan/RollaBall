using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {
	private Rigidbody rb;
	private Vector3 trapPos;

	void Start()
	{
		rb = GetComponent <Rigidbody> ();
		trapPos = rb.transform.position;
	}

	void Update()
	{
		rb.transform.position = trapPos;
	}

}
