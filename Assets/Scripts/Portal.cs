using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Portal : MonoBehaviour {
	private Rigidbody rb;
	public string newLevel;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();

	}

	
	void OnCollisionEnter(Collision other)
	{

		if(other.gameObject.CompareTag("Player"))
		{
			
			SceneManager.LoadScene (newLevel);
		}
	}
}
