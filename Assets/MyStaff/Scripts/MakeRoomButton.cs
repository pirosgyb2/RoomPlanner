using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Collider))]
public class MakeRoomButton : MonoBehaviour {
	private Renderer _renderer;


	public Material inactiveMaterial;
	public Material gazedAtMaterial;


	void Start() {
		_renderer = GetComponent<Renderer>();
		SetGazedAt(false);
	}

	public void SetGazedAt(bool gazedAt) {
		if (inactiveMaterial != null && gazedAtMaterial != null) {
			_renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial ;
			return;
		}
	}

	public void SetClicked(){
		StartCoroutine (LoadAsyncScene ());
		SetGazedAt(false);
		return;
	}

	IEnumerator LoadAsyncScene(){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync ("MakeRoom");

		while(!asyncLoad.isDone){
			yield return null;
		}

	}

}

