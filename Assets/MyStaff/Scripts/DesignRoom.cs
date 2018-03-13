using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Collider))]
public class DesignRoom : MonoBehaviour {
	private Vector3 startingPosition;
	private Renderer renderer;


	public Material inactiveMaterial;
	public Material gazedAtMaterial;

	void Start() {
		startingPosition = transform.localPosition;
		renderer = GetComponent<Renderer>();
		SetGazedAt(false);
	}

	public void SetGazedAt(bool gazedAt) {
		if (inactiveMaterial != null && gazedAtMaterial != null) {
			renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial ;
			return;
		}
	}

	public void SetClicked(){
		StartCoroutine (LoadAsyncScene ());
		SetGazedAt(false);
		return;
	}
	IEnumerator LoadAsyncScene(){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync ("DesignRoom",LoadSceneMode.Additive);

		while (!asyncLoad.isDone) {
			yield return null;
		}
	}
}

