using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XScript : MonoBehaviour {

	public void Clicked(){
		transform.parent.gameObject.SetActive (false);
	}
}
