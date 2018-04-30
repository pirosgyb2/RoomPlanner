using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButtonScript : MonoBehaviour {
	
	public void Clicked(){
		GameObject menu = GameObject.FindGameObjectWithTag ("MenuRoot");
		GameObject savePanel = menu.GetComponent<MakeRoomMenu> ().savePanel;
		GameObject keyboard = menu.GetComponent<MakeRoomMenu> ().keyboard;

		savePanel.SetActive (true);
		keyboard.SetActive (true);
		savePanel.GetComponentInChildren<SavePanelScript>().isOpenNew = true;
		Destroy (transform.parent.gameObject);	
	}
}
