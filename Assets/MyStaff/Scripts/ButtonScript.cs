using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	public string measurement; //m ,cm, ˚
	public string changingProperty; //height, width, thickness, rotation

	private GameObject wall;

	public void Down(){
		wall = transform.parent.GetComponent<Room> ().GetSelectedWall ();
		Changing (-1);
	}

	public void Up(){
		wall = transform.parent.GetComponent<Room> ().GetSelectedWall ();
		Changing (1);
	}

	//number =1 vagy -1
	private void Changing(int number){
		
		switch (changingProperty.ToLower ()) {
		case "height":
			ChangeProperty (number,true,2);
			break;
		case "width":
			ChangeProperty (number,true,1);
			break;
		case "thickness":
			ChangeProperty (number,true,3);
			break;
		case "rotation":
			ChangeProperty (number,false);
			break;
		default:
			print ("Rosszul irtadbe az editorban a panelnek a changingProperty erteket: " + changingProperty.ToLower ());
			break;
		}
	}

	private void ChangeProperty( int number,bool isSizeProperty,int xyzNumber=2){
		if (isSizeProperty) {
			if (measurement.ToLower () == "m") {
				DoChange( xyzNumber,  number);
			} else if (measurement.ToLower () == "cm") {
				DoChange(xyzNumber, number / 10);
			} else {
				print ("Rosszul irtadbe az editorban a panelnek a measurement erteket: " + measurement.ToLower ());
			}
		}
		else{
			if (measurement.ToLower () == "˚") {
				DoChange( 2,  number);
			} 
			else {
				print ("Rosszul irtadbe az editorban a panelnek a measurement erteket: " + measurement.ToLower ());
			}
		}
	}

	private void DoChange(int xyzNumber, int number ){
		switch(xyzNumber){
		case 1:
			Vector3 temp = wall.GetComponent<WallScript> ().transform.localScale;
			temp.x += number;
			wall.GetComponent<WallScript> ().transform.localScale = temp;
			break;
		case 2:
			Vector3 temp2 = wall.GetComponent<WallScript> ().transform.localScale;
			temp2.y += number;
			wall.GetComponent<WallScript> ().transform.localScale = temp2;
			break;
		case 3:
			Vector3 temp3 = wall.GetComponent<WallScript> ().transform.localScale;
			temp3.z += number;
			wall.GetComponent<WallScript> ().transform.localScale = temp3;
			break;
		}
	}
}
