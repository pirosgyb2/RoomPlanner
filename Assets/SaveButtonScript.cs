using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonScript : MonoBehaviour {

	public GameObject room;
	public Text inputField;

	public void Save(){
		Room roomScript = room.GetComponent<Room> ();
		if (!(inputField.text.StartsWith (" ") || inputField.text == "")) {
			roomScript.SetSaveFolder (inputField.text);
			roomScript.Save ();
			roomScript.SetSaveFolder (roomScript.defaultSaveFolderName);
			inputField.text="Saved";
		}
	}
	

}
