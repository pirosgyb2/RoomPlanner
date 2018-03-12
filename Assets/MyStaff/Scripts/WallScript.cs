using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	public Material inactiveMaterial;
	public Material gazedAtMaterial;
	public Material selectedMaterial;

	private Vector3 startingPosition;
	private Renderer renderer;

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
		if (selectedMaterial != null) {
			renderer.material = selectedMaterial;
		}
		return;
	}
}
