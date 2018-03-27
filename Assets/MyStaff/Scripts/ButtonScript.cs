using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	public string measurement; //m ,cm, ˚
	public string changingProperty; //height, width, thickness, rotation

	private GameObject wall;

	void Start(){
		wall = transform.parent.GetComponent<WallCustomizePanel> ().wall;
	}


	public void Down(){
		print("Before: " + wall.transform.localScale.ToString());
		Changing (-1);
		print("After : " + wall.transform.localScale.ToString());
	}

	public void Up(){
		print("UP fuggvenybe bejutottam");
		Changing (1);
	}

	//number =1 vagy -1
	private void Changing(float AddValue){
		
		switch (changingProperty.ToLower ()) {
		case "height":
			ChangeProperty (AddValue,true,2);
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
				print ("Rosszul irtadbe az editorban a panelnek a measurement erteket: " + measurement.ToLower ());
			}
		}
		else{
			if (measurement.ToLower () == "degree") {
				DoRotation(AddValue*5);
			} 
			else {
				print ("Rosszul irtadbe az editorban a panelnek a measurement erteket: " + measurement.ToLower ());
			}
		}
	}

	private void DoScale(int xyzNumber, float AddValue ){
		//Vector3 temp = wall.GetComponent<WallScript> ().transform.localScale;
		Vector3 temp = wall.transform.localScale;

		switch(xyzNumber){
		case 1:	
			if((AddValue<0 && temp.x>0) || AddValue>0)  //hogy ne legyen negatív egyik scale erteke se
			temp.x += AddValue;
			break;
		case 2:
			if((AddValue<0 && temp.y>0) || AddValue>0)  //hogy ne legyen negatív egyik scale erteke se
			temp.y += AddValue;
			break;
		case 3:
			if((AddValue<0 && temp.z>0) || AddValue>0)  //hogy ne legyen negatív egyik scale erteke se
			temp.z += AddValue;
			break;
		}

		wall.transform.localScale=temp;
	}

	private void DoRotation(float AddValue){
		print("DoRotate fuggvenybe bejutottam");
		wall.transform.Rotate (0,AddValue,0);
	}
}
