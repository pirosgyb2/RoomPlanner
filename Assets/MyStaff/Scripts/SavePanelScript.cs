using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanelScript : MonoBehaviour {

	public GameObject room;
	public Text inputField;
	public GameObject savePanel;
	public GameObject keyboard;

	[HideInInspector]
	public bool isOpenNew = false;

	public void Save(){
		Room roomScript = room.GetComponent<Room> ();
		if (!(inputField.text.StartsWith (" ") || inputField.text == "")) {
			roomScript.SetSaveFolder (inputField.text);
			roomScript.Save ();
			roomScript.SetSaveFolder (roomScript.defaultSaveFolderName);
			inputField.text = "";
		}

		if (isOpenNew) {
			room.GetComponent<Room> ().DestroyWalls ();
			room.transform.rotation = Quaternion.identity;
		}

		savePanel.SetActive (false);
		keyboard.SetActive (false);
	}


}
