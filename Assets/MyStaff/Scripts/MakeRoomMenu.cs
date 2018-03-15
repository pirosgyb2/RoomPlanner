using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaydreamElements.ClickMenu;

public class MakeRoomMenu : MonoBehaviour {

	void Start(){
		
		gameObject.GetComponent<ClickMenuRoot> ().OnItemSelected += Switcher;
	}

	void Switcher(ClickMenuItem item){
		if (item != null) {
			switch (item.id) {
			case 0:
				StartCoroutine (LoadAsyncScene ());
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
