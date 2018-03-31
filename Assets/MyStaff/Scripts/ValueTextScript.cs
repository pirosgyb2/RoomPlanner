using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueTextScript : MonoBehaviour {

	[Tooltip("X=1, Y=2, Z=3, rotation=4")]
	public int XYZ=0;

	void Start(){
		GameObject wall = GameObject.FindGameObjectWithTag ("SelectedWall");

		Vector3 scale= wall.transform.localScale;

		float value = 0;

		switch(XYZ){
		case 1:
			value = scale.x;
				break;
		case 2:
			value = scale.y;
			break;
		case 3:
			value = scale.z;
			break;
		case 4:
			value = wall.transform.localRotation.eulerAngles.y;
			break;
		default:
			print ("Nem jo szamot adtal meg az XYZ parameternek: " + XYZ.ToString ());
			break;
		}

		if (XYZ==1 || XYZ==2 || XYZ==3) {
			GetComponent<Text> ().text = Mathf.Round (value * 100).ToString ();
		}
		else if(XYZ==4){
			GetComponent<Text> ().text = Mathf.Round (value).ToString ();
		}
		else{
			GetComponent<Text> ().text="Error";
		}


	}
		
}
