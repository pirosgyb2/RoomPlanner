using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class WallScript : MonoBehaviour {

	public Material inactiveMaterial;
	public Material gazedAtMaterial;
	public Material selectedMaterial;
	public GameObject panel;

	private Renderer _renderer;
	private Room parentScript;
	private bool selected=false;
	private bool isPanelOpenedYet= false;
	private GameObject instantiatedPanel;

	void Awake(){
		parentScript=transform.parent.GetComponent<Room> ();
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	void Start() {
		_renderer = GetComponent<Renderer>();
		SetGazedAt(false);
	}

	public void SetGazedAt(bool gazedAt) {
		if (inactiveMaterial != null && gazedAtMaterial != null) {
			if(!selected){
				_renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial ;
			}
			return;
		}
	}

	public void SetInactive(){
		if (inactiveMaterial != null) {
			_renderer.material = inactiveMaterial;
			Destroy (instantiatedPanel);
			isPanelOpenedYet = false;
			print("Toroltem a panelt");
		}
	}

	public void SetClicked(){


		int previousSelectedIndex = parentScript.GetSelectedWallIndex ();
		//ha van kijelolt es ez a kijelolt nem onmaga akkor
		if(previousSelectedIndex > -1 && previousSelectedIndex != transform.GetSiblingIndex()){
			GameObject previousSelected=parentScript.transform.GetChild (previousSelectedIndex).gameObject;
			previousSelected.GetComponent<WallScript> ().SetInactive ();
			previousSelected.GetComponent<WallScript> ().selected = false;
		}

		if (!isPanelOpenedYet) {
			instantiatedPanel=Instantiate (panel, new Vector3 (-3, 4.5f, 0.7f), Quaternion.identity);
			isPanelOpenedYet = true;
			//TODO: a fal adja át önmagát a panelnak, vagy valahoyg a panel érje el a falat
			//instantiatedPanel.transform.GetChild ().GetComponent<WallCustomizePanel> ();
			print ("Uj panelt hoztam letre");
		}

		selected = true;
		parentScript.SetSelectedWallIndex (transform.GetSiblingIndex ());
		if (selectedMaterial != null) {
			_renderer.material = selectedMaterial;
		}
		return;
	}

	public void Save(){

		BinaryFormatter binary = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/LastEditedRoom/wall" + transform.GetSiblingIndex().ToString() + ".dat");
		WallData dat = new WallData ();

		dat.siblingIndex = gameObject.transform.GetSiblingIndex();
		dat.x = gameObject.transform.position.x;
		dat.y = gameObject.transform.position.y;
		dat.z = gameObject.transform.position.z;

		binary.Serialize (file, dat);
		file.Close ();
	}

	public void Load(int id){
		if (File.Exists (Application.persistentDataPath + "/LastEditedRoom/wall" + id.ToString () + ".dat")){
			
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/LastEditedRoom/wall" + id.ToString () + ".dat",FileMode.Open);
			WallData dat = (WallData)binary.Deserialize (file);
			file.Close ();

			transform.position = new Vector3(dat.x,dat.y,dat.z);
			transform.SetSiblingIndex( dat.siblingIndex);	
		}
			
	}

}
