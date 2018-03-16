using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;



public class Room : MonoBehaviour {

	public GameObject wall;
	private string saveFolder;

	void Awake(){
		saveFolder = Application.persistentDataPath + "/LastEditedRoom";
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes"); //ez a szerialiazlo miatt kell elvielg, midnen scriptbe ahol binaryformattert hasznalok
	}

	public void Save(){

		//Eloszor toroljuk ha volt valami korabbrol ebben a mappaban, ha nem is letezett a mappa akkor letrehozzuk
		if (!Directory.Exists (saveFolder)) {
			Directory.CreateDirectory (saveFolder);
		}
		else{
			EmptyFolder (saveFolder);
		}

		//elmentjuk a szoba falait
		SaveChildren ();

		//kiirjuk fajlba h hany fal volt
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream file = File.Create (saveFolder+"/room.dat");
		RoomData dat = new RoomData ();

		dat.childCount = transform.childCount;

		binary.Serialize (file, dat);
		file.Close ();
	}

	public void Load(){		
		int childCount = 0;
		childCount=GetChildCount();

		for (int i = 0; i < childCount; i++) {
			GameObject newWall=Instantiate(wall,new Vector3(0,0.5f,1),Quaternion.identity,gameObject.transform);
			newWall.GetComponent<WallScript> ().Load (i);
		}
	}

	private void SaveChildren(){
		if (transform.childCount > 0) {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).GetComponent<WallScript> ().Save ();
			}
		}
	}

	private int GetChildCount(){
		int childCount = 0;
		if(File.Exists(saveFolder+"/room.dat")){
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream file = File.Open (saveFolder+"/room.dat", FileMode.Open);
			RoomData dat = (RoomData)binary.Deserialize (file);
			childCount = dat.childCount;
			file.Close ();
		}
		return childCount;
	}

	public void DeleteFolder(string path){
		EmptyFolder (path);

		Directory.Delete (path);

		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh();
		#endif

	}

	public void EmptyFolder(string path){
		string[] files = Directory.GetFiles (path);

		string[] folders = Directory.GetDirectories (path);

		foreach (string file in files) {
			string filePath=file.Replace('\\','/');
			print (filePath);
			File.SetAttributes (filePath, FileAttributes.Normal);
			File.Delete(filePath);
		}

		foreach(string folder in folders){
			string folderPath=folder.Replace('\\','/');
			DeleteFolder (folder);
		}

		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh();
		#endif
	}

	public void DestroyWalls(){
		EmptyFolder (Application.persistentDataPath + "/LastEditedRoom");
		for (int i = 0; i < transform.childCount; i++) {
			Destroy (transform.GetChild (i).gameObject);
		}
	}
}
