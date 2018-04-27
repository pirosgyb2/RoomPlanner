using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedRoomPanelScript : MonoBehaviour {

	private Color inactiveBackground;
	private Color activeBackground;
	private Color inactiveFont;
	private Color activeFont;

	void Start(){
		inactiveBackground = new Color (0.9f, 0.9f, 0.9f);
		activeBackground = new Color (0.25f, 0.44f, 1.0f);
		activeFont=new Color (0.9f, 0.9f, 0.9f);
		inactiveFont=new Color (0.31f, 0.31f, 0.31f);

		GetComponent<Image> ().color = inactiveBackground;
		GetComponentInChildren<Text> ().color = inactiveFont;
	}

	public void Clicked(){
		MakeMeActive ();

		string folderName = transform.GetComponentInChildren<Text> ().text;

		GameObject loadButton = GameObject.Find ("LoadButton");
		GameObject deleteButton = GameObject.Find ("DeleteButton");

		loadButton.GetComponent<LoadButtonScript> ().SetSelectedRoomPanel (folderName);
		deleteButton.GetComponent<DeleteButtonScript> ().SetSelectedRoomPanel (folderName);


	}

	public void MakeMeActive(){
		List<GameObject> allPanel = new List<GameObject> ();
		allPanel.AddRange(GameObject.FindGameObjectsWithTag ("SavedRoomPanel"));

		//mindegyik inaktívra szenzése
		foreach (var item in allPanel) {			
			item.GetComponent<Image> ().color =inactiveBackground;
			item.GetComponentInChildren<Text> ().color = inactiveFont;
		}

		//a kijelöltöt pedig vilagosabbra
		GetComponent<Image> ().color = activeBackground;
		GetComponentInChildren<Text> ().color = activeFont;
	}


}
