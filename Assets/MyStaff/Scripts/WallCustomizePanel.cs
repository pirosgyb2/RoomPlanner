using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCustomizePanel : MonoBehaviour {

	public GameObject wall;




	void Start (){
		Text heightText = GameObject.Find ("HeightValueText").GetComponent<Text>();
		Text widthText  = GameObject.Find ("WidthValueText").GetComponent<Text>();
		Text thicknessText  = GameObject.Find ("ThicknessValueText").GetComponent<Text>();
		Text rotationText  = GameObject.Find ("RotationValueText").GetComponent<Text>();

		heightText.text = Mathf.Round(wall.transform.localScale.x*100).ToString();
		widthText.text = Mathf.Round(wall.transform.localScale.y*100).ToString();
		thicknessText.text = Mathf.Round(wall.transform.localScale.z*100).ToString();
		rotationText.text = Mathf.Round(wall.transform.localRotation.eulerAngles.y).ToString();
	}


}
