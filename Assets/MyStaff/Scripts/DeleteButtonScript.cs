using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButtonScript : MonoBehaviour {

	private string selectedRoomPanelName;
	public GameObject room;

	public void SetSelectedRoomPanel(string selected){
		selectedRoomPanelName = selected;
	}

	public void Click(){
		if (selectedRoomPanelName != null) {
			Room roomScript = room.GetComponent<Room> ();


			roomScript.SetSaveFolder (selectedRoomPanelName);
			roomScript.DeleteFolder (roomScript.GetSaveFolderPath ());
			roomScript.SetSaveFolder (roomScript.defaultSaveFolderName);

			GameObject.Find ("LoadScrollView").GetComponent<LoadScrollViewScript> ().ListFolderNames ();
		}
	}
}
