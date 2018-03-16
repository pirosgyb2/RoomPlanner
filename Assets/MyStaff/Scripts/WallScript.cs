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

	private Renderer _renderer;

	void Awake(){
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	void Start() {
		_renderer = GetComponent<Renderer>();
		SetGazedAt(false);
	}

	public void SetGazedAt(bool gazedAt) {
		if (inactiveMaterial != null && gazedAtMaterial != null) {
			_renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial ;
			return;
		}
	}

	public void SetClicked(){		
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
