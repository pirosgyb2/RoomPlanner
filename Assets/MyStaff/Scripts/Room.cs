using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using UnityEditor;



public class Room : MonoBehaviour {

	public GameObject wall;
	private string saveFolderPath;
	private string saveFolderName;

	[HideInInspector]
	public readonly string defaultSaveFolderName="LastEditedRoom";

	[HideInInspector]
	public bool isRotatable=true;

	private int selectedWallIndex = -1;
	private Vector3 roomCenter=new Vector3(0,0,0);

	private List<Vector3> wallsPos;
	private List<Quaternion> wallsRot;

	void Awake(){
		saveFolderPath = Application.persistentDataPath + "/"+defaultSaveFolderName;
		saveFolderName = defaultSaveFolderName;
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes"); //ez a szerialiazlo miatt kell elvielg, midnen scriptbe ahol binaryformattert hasznalok

		wallsPos = new List<Vector3> ();
		wallsRot = new List<Quaternion> ();
	}

	void Update(){
		
		if (GvrControllerInput.TouchDown && isRotatable) {
			
			for (int i = 0; i < transform.childCount; i++) {
				Transform wall = transform.GetChild (i);
				wallsPos.Add (wall.localPosition); 
				wallsRot.Add (wall.localRotation);
			}

		}
		if (GvrControllerInput.IsTouching && isRotatable && (GvrControllerInput.TouchPosCentered.x > 0.7f || GvrControllerInput.TouchPosCentered.x < -0.7f)) {
			float rotSpeed = 2;

			Vector2 touchPos = GvrControllerInput.TouchPosCentered;

			float rotY = touchPos.x * rotSpeed*Mathf.Rad2Deg;
			rotY *= Time.deltaTime;

			transform.RotateAround (roomCenter,Vector3.up,-rotY);

		}
		if (GvrControllerInput.TouchUp && isRotatable) {
			
			for (int i = 0; i < transform.childCount; i++) {
				Transform wall = transform.GetChild (i);
				wall.localPosition = wallsPos [i]; 
				wall.localRotation = wallsRot [i];
			}
			wallsPos.Clear ();
			wallsRot.Clear ();
		}
	}

	public void Save(){
		print ("Room save");
		//Eloszor toroljuk ha volt valami korabbrol ebben a mappaban, ha nem is letezett a mappa akkor letrehozzuk
		if (!Directory.Exists (saveFolderPath)) {
			Directory.CreateDirectory (saveFolderPath);
		}
		else{
			EmptyFolder (saveFolderPath);
		}

		//elmentjuk a szoba falait
		SaveChildren ();

		//kiirjuk fajlba h a room adatait
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream file = File.Create (saveFolderPath+"/room.dat");
		RoomData dat = new RoomData ();

		dat.childCount = transform.childCount;
		dat.rotationX = transform.rotation.x;
		dat.rotationY = transform.rotation.y;
		dat.rotationZ = transform.rotation.z;
		dat.rotationW = transform.rotation.w;
		//dat.centerX = roomCenter.x;
		//dat.centerY = roomCenter.y;
		//dat.centerZ = roomCenter.z;


		binary.Serialize (file, dat);
		file.Close ();
	}

	public void Load(){	
		print ("Room load");
			
		int childCount = 0;
		childCount=LoadRoomData();

		for (int i = 0; i < childCount; i++) {
			GameObject newWall=Instantiate(wall,new Vector3(0,0.5f,1),Quaternion.identity,gameObject.transform);
			newWall.GetComponent<WallScript> ().Load (i);
		}

		CalculateRoomsCenter();

	}

	private void SaveChildren(){
		if (transform.childCount > 0) {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).GetComponent<WallScript> ().Save ();
			}
		}
	}
		
	private int LoadRoomData(){
		int childCount = 0;
		if(File.Exists(saveFolderPath+"/room.dat")){
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream file = File.Open (saveFolderPath+"/room.dat", FileMode.Open);
			RoomData dat = (RoomData)binary.Deserialize (file);
			childCount = dat.childCount;
			//roomCenter = new Vector3 (dat.centerX, dat.centerY, dat.centerZ);
			transform.rotation = new Quaternion (dat.rotationX,dat.rotationY,dat.rotationZ,dat.rotationW);
			//transform.Rotate (dat.rotationX,dat.rotationY,dat.rotationZ);
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
			//print (filePath);
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

	/// <summary>
	/// A falakat gameobjecteit és a mentett fájljait is törlni.
	/// </summary>
	public void DestroyWalls(){
		EmptyFolder (Application.persistentDataPath + "/"+defaultSaveFolderName);
		DestroyChildObjects ();

	}

	/// <summary>
	/// Csak a falak gameobjecteit törli, fájlból nem.
	/// </summary>
	private void DestroyChildObjects(){
		for (int i = 0; i < transform.childCount; i++) {
			Transform wall=transform.GetChild (i);
			Destroy (wall.gameObject);
		}
		transform.DetachChildren (); //ez nagyonnagyon fontos, ha nem csatolnánk le a gyerekeket itt, akkor hiába destroyoltuk a gyerekek gameobjectekeit, a gyerekek nem egyből törlődnek és a transform.childcount sem változik egyből nullára, így pl azokat a childeket is kiírta fájlba amik már nem látszottak de az gameobject nélkül a tramsformjuk még el volt tárolva, tehát ki tudta írni őket fájlba
		/*
		foreach (Transform child in transform) {
			GameObject.Destroy (child.gameObject);
		}*/
	}

	public bool IsAnithingAtSpwanPlace(){
		bool isOnSpawnPlace = false;
		for (int i = 0; i < transform.childCount; i++) {
			WallScript wall = transform.GetChild (i).GetComponent<WallScript> ();
			if(wall.transform.position==wall.startingPosition){
				isOnSpawnPlace = true;
			}
		}
		return isOnSpawnPlace;
	}

	public void SetSelectedWallIndex(int childIndex){
		selectedWallIndex = childIndex;
	}
	public int GetSelectedWallIndex(){
		return selectedWallIndex;
	}

	public GameObject GetSelectedWall(){
		return (selectedWallIndex != -1 ?GetChild(selectedWallIndex):null);
	}

	public GameObject GetChild(int childIndex){
		return transform.GetChild (childIndex).gameObject;
	}


	public void SetSaveFolder(string justTheFolderName){
		saveFolderPath = Application.persistentDataPath + "/"+justTheFolderName;
		saveFolderName = justTheFolderName;
	}

	public string GetSaveFolderName(){
		return saveFolderName;
	}

	public string GetSaveFolderPath(){
		return saveFolderPath;
	}

	public void CalculateRoomsCenter(){
		float sumX = 0;
		float sumY = 0;
		float sumZ = 0;

		int childCount = transform.childCount;

		for (int i = 0; i < childCount; i++) {
			Transform child = transform.GetChild (i);
			sumX += child.position.x;
			sumY += child.position.y;
			sumZ += child.position.z;
		}


		roomCenter = new Vector3 (sumX/(float)childCount,sumY/(float)childCount,sumZ/(float)childCount);

	}
}
