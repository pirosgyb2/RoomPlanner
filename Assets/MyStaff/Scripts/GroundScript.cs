using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

	public void Clicked(){		
		GameObject selectedWall = GameObject.FindGameObjectWithTag ("SelectedWall");
		if (selectedWall != null) {
			selectedWall.GetComponent<WallScript> ().SetInactive ();
		}
	}
}
