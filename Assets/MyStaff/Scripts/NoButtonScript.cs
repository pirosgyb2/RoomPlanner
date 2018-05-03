using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButtonScript : MonoBehaviour {

	public void Clicked(){
		GameObject room=GameObject.FindGameObjectWithTag ("Room");
		room.GetComponent<Room> ().DestroyWalls ();
		Destroy (transform.parent.gameObject);
		room.transform.rotation=Quaternion.identity;
	}
}
