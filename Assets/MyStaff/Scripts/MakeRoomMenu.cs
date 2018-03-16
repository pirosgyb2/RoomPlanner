using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaydreamElements.ClickMenu;


public class MakeRoomMenu : MonoBehaviour {

	public GameObject wall;
	public GameObject room;

	void Awake(){
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	void Start(){
		room.GetComponent<Room> ().Load ();
		gameObject.GetComponent<ClickMenuRoot> ().OnItemSelected += Switcher;
	}

	void Switcher(ClickMenuItem item){
		if (item != null) {
			switch (item.id) {
			case 0: //Home
				room.GetComponent<Room>().Save();
				StartCoroutine (LoadAsyncScene ());
				break;
			case 1: //Add
				Instantiate (wall,new Vector3(0,0.5f,1),Quaternion.identity,room.transform);
				break;
			case 2: //File
				break;
			case 3: //View				
				break;
			case 4: //Delete
				room.GetComponent<Room> ().DestroyWalls();
				break;
			case 5: //Load
				break;
			case 6: //Save
				break;
			case 7: //TopView
				break;
			case 8: //SideView
				break;
			case 9: //PerspView
				break;
			case 10: //PerspView
				break;

			default:
				break;			
			}
		}
	}

	IEnumerator LoadAsyncScene(){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync ("Home");

		while(!asyncLoad.isDone){
			yield return null;
		}

	}
}