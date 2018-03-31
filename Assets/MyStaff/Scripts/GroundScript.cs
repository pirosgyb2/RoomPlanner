using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

	public void Clicked(){
		print ("GroundScript Clicked");
		GameObject room=GameObject.Find ("Room");
		if (room != null) {
			GameObject selectedWall = room.GetComponent<Room> ().GetSelectedWall ();
			if (selectedWall != null) {
				selectedWall.GetComponent<WallScript> ().SetInactive ();
			}
		}
	}
}
