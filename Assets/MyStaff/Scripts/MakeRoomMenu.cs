using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaydreamElements.ClickMenu;

public class MakeRoomMenu : MonoBehaviour {

	public GameObject wall;

	void Start(){		
		gameObject.GetComponent<ClickMenuRoot> ().OnItemSelected += Switcher;
	}

	void Switcher(ClickMenuItem item){
		if (item != null) {
			switch (item.id) {
			case 0: //Home
				StartCoroutine (LoadAsyncScene ());
				break;
			case 1: //Add
				Instantiate (wall,new Vector3(0,0.5f,1),Quaternion.identity);
				break;
			case 2: //File
				break;
			case 3: //View
				break;
			case 4: //Delete
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
