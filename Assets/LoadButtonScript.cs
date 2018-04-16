using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtonScript : MonoBehaviour {

	private string selectedRoomPanelName;
	public GameObject room;

	public void SetSelectedRoomPanel(string selected){
		selectedRoomPanelName = selected;
	}

	public void Click(){
		Room roomScript=room.GetComponent<Room>();
		//TODO room mostani gameobjecteket disposeolni
		roomScript.DestroyChildObjects();

		//TODO loadolni a megfelelo mappabol
		roomScript.SetSaveFolder(selectedRoomPanelName);
		roomScript.Load ();
		roomScript.SetSaveFolder (roomScript.defaultSaveFolderName);
	}
}
