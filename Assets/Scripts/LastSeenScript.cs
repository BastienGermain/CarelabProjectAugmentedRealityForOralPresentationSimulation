using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Store last game object seen by eye tracking
public class LastSeenScript : MonoBehaviour {

	private GameObject lastSeen;

	public void StorePrefab(GameObject toStore)
    {
		lastSeen = toStore;
    }

	public bool IsStored()
	{
		return lastSeen ? true : false;
	}

    public GameObject GetLastSeen()
    {
        return lastSeen;
    }
}
