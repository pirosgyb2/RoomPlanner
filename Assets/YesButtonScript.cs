using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButtonScript : MonoBehaviour {
	
	public void Clicked(){
		GameObject menu = GameObject.FindGameObjectWithTag ("MenuRoot");
		GameObject savePanel = menu.GetComponent<MakeRoomMenu> ().savePanel;
		savePanel.SetActive (true);
		savePanel.GetComponentInChildren<SaveButtonScript>().isOpenNew = true;
		Destroy (transform.parent.gameObject);	
	}
}
