
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class AddWallScript : MonoBehaviour {
	private Vector3 startingPosition;
	private Renderer renderer;
	private int addCounter=0;


	public Material inactiveMaterial;
	public Material gazedAtMaterial;
	public GameObject wall;


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
		SetGazedAt(false);
		return;
	}

	public void Reset() {
		int sibIdx = transform.GetSiblingIndex();
		int numSibs = transform.parent.childCount;
		for (int i=0; i<numSibs; i++) {
			GameObject sib = transform.parent.GetChild(i).gameObject;
			sib.transform.localPosition = startingPosition;
			sib.SetActive(i == sibIdx);
		}
	}

	public void Recenter() {
		#if !UNITY_EDITOR
		GvrCardboardHelpers.Recenter();
		#else
		if (GvrEditorEmulator.Instance != null) {
			GvrEditorEmulator.Instance.Recenter();
		}
		#endif  // !UNITY_EDITOR
	}

	public void AddWall() {
		
		//Instantiate (wall,new Vector3(0,0,5),new Quaternion(0,0,0,0));
		Instantiate (wall,new Vector3(0,0.5f,addCounter *wall.transform.localScale.z + 1),Quaternion.identity);
		addCounter++;
		//wall.transform.position -= new Vector3 (0,0,addCounter * wall.transform.localScale.z + 5 );
	//	return;
	}
}

