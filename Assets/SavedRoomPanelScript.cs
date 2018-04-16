using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedRoomPanelScript : MonoBehaviour {



	public void Clicked(){
		string folderName = transform.GetComponentInChildren<Text> ().text;

		GameObject loadButton = GameObject.Find ("LoadButton");
		GameObject deleteButton = GameObject.Find ("DeleteButton");

		loadButton.GetComponent<LoadButtonScript> ().SetSelectedRoomPanel (folderName);
		deleteButton.GetComponent<DeleteButtonScript> ().SetSelectedRoomPanel (folderName);
	}
}
