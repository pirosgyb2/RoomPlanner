using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonScript : MonoBehaviour {

	public GameObject room;
	public Text inputField;
	public bool isOpenNew = false;

	public void Save(){
		Room roomScript = room.GetComponent<Room> ();
		if (!(inputField.text.StartsWith (" ") || inputField.text == "" || inputField.text == "Saved")) {
			roomScript.SetSaveFolder (inputField.text);
			roomScript.Save ();
			roomScript.SetSaveFolder (roomScript.defaultSaveFolderName);
			inputField.text = "Saved";
		}

		if (isOpenNew) {
			room.GetComponent<Room> ().DestroyWalls ();
		}

		//transform.parent.gameObject.SetActive (false);
	}
}
