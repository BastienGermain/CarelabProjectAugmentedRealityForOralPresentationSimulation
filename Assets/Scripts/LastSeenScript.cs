using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSeenScript : MonoBehaviour {

	private GameObject lastSeen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StorePrefab(GameObject toStore)
    {
		lastSeen = toStore;
    }

	public bool IsStored()
	{
		return lastSeen ? true : false;
	}
}
