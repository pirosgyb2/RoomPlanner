using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	public string measurement; //m ,cm, ˚
	public string changingProperty; //height, width, thickness, rotation

	private GameObject wall=null;
	private Text heightText;
	private Text widthText;
	private Text thicknessText;
	private Text rotationText;



	void Start(){
		UpdateTextsAndWall ();
	}


	public void Down(){
		UpdateTextsAndWall ();
		Changing (-1);
		PutWallToGround ();
		wall.transform.parent.GetComponent<Room> ().Save ();
	}

	public void Up(){
		UpdateTextsAndWall ();
		Changing (1);
		PutWallToGround ();
		wall.transform.parent.GetComponent<Room> ().Save ();
	}

	//number =1 vagy -1
	private void Changing(float AddValue){
		
		switch (changingProperty.ToLower ()) {
		case "height":
			ChangeProperty (AddValue, true, 2);
			break;
		case "width":
			ChangeProperty (AddValue,true,1);
			break;
		case "thickness":
			ChangeProperty (AddValue,true,3);
			break;
		case "rotation":
			ChangeProperty (AddValue,false);
			break;
		default:
			print ("Rosszul irtadbe az editorban a panelnek a changingProperty erteket: " + changingProperty.ToLower ());
			break;
		}
	}

	private void ChangeProperty( float AddValue,bool isSizeProperty,int xyzNumber=2){
		if (isSizeProperty) {
			if (measurement.ToLower () == "m") {
				DoScale( xyzNumber,  AddValue);
			} else if (measurement.ToLower () == "cm") {
				DoScale(xyzNumber, AddValue / 10);
			} else {
				print ("Rosszul irtad be az editorban a panelnek a measurement erteket: " + measurement.ToLower ());
			}
		}
		else{
			if (measurement.ToLower () == "degree") {
				DoRotation(AddValue*5);
			} 
			else {
				print ("Rosszul irtad be az editorban a panelnek a measurement erteket: " + measurement.ToLower ());
			}
		}
	}

	private void DoScale(int xyzNumber, float AddValue ){
		
		//UpdateTextsAndWall ();
		Vector3 temp = wall.transform.localScale;

		switch(xyzNumber){
		case 1:	
			if ((AddValue < 0 && temp.x > 0) || AddValue > 0)   //hogy ne legyen negatív egyik scale erteke se
				temp.x += AddValue;	
			widthText.text = (Mathf.Round(temp.x*100)).ToString();
			break;
		case 2:
			if ((AddValue < 0 && temp.y > 0) || AddValue > 0)  //hogy ne legyen negatív egyik scale erteke se
				temp.y += AddValue;
			heightText.text = (Mathf.Round(temp.y*100)).ToString();
			break;
		case 3:
			if ((AddValue < 0 && temp.z > 0.0001) || AddValue > 0)  //hogy ne legyen negatív egyik scale erteke se
				temp.z += AddValue;
			thicknessText.text = (Mathf.Round(temp.z*100)).ToString();
			break;
		}

		wall.transform.localScale=temp;
	}

	private void DoRotation(float AddValue){
		//UpdateTextsAndWall ();
		wall.transform.Rotate (0,AddValue,0);
		rotationText.text = (Mathf.Round(wall.transform.localRotation.eulerAngles.y)).ToString();
	}

	private void UpdateTextsAndWall(){		
		wall = GameObject.FindGameObjectWithTag ("SelectedWall");

		heightText = GameObject.Find ("HeightValueText").GetComponent<Text>();
		widthText  = GameObject.Find ("WidthValueText").GetComponent<Text>();
		thicknessText  = GameObject.Find ("ThicknessValueText").GetComponent<Text>();
		rotationText  = GameObject.Find ("RotationValueText").GetComponent<Text>();
	}

	private void PutWallToGround(){
		Vector3 posNow = wall.transform.position;
		wall.transform.position = new Vector3(posNow.x,wall.transform.localScale.y / 2,posNow.z);
	}

}
