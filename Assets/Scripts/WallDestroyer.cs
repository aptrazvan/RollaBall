using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour {
	private GameObject[] walls;

	public void WallDestroy()
	{
		walls = GameObject.FindGameObjectsWithTag ("HighWall");
			foreach (GameObject highWall in walls)
				highWall.gameObject.SetActive (false);
	}

}
