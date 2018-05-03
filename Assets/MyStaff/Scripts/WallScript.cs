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
	public string filePath;
	public Vector3 startingPosition;

	private DaydreamElements.ObjectManipulation.MoveablePhysicsObject moveablePhysicsScript;
	private Renderer _renderer;
	private Room roomScript;
	private bool selected=false;
	private bool isPanelOpenedYet= false;
	private GameObject instantiatedPanel;
	private int clickCount;
	private GameObject loadPanel;


	void Awake(){
		roomScript=transform.parent.GetComponent<Room> ();
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	void Start() {
		_renderer = GetComponent<Renderer>();
		SetGazedAt(false);
		moveablePhysicsScript= gameObject.GetComponent<DaydreamElements.ObjectManipulation.MoveablePhysicsObject> ();
		SwitchMoveablePhysicsScript(false,false);
		transform.GetSiblingIndex ();

		GameObject menuRoot = GameObject.FindGameObjectWithTag ("MenuRoot");
		loadPanel = menuRoot.GetComponent<MakeRoomMenu> ().loadPanel;
	}
		
	public void SwitchMoveablePhysicsScript(bool enable, bool saveOnSwitchOff){
		if(enable){
			moveablePhysicsScript.enabled=true;	
		}
		else{
			print ("switch movePhysscript");
			moveablePhysicsScript.enabled=false;
			roomScript.CalculateRoomsCenter();
			if (saveOnSwitchOff) {
				roomScript.Save ();
			}
		}
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
			Destroy (instantiatedPanel.gameObject);

			isPanelOpenedYet = false;
			//parentScript.SetSelectedWallIndex (-1);
			GameObject.Find ("MenuRoot").GetComponent<MakeRoomMenu> ().ChangeToAlterMenu (false);
			selected = false;
			gameObject.tag="Untagged";
			SwitchMoveablePhysicsScript(false,true);
			roomScript.isRotatable = true;

		}
	}

	public void SetClicked(){		
		if (!loadPanel.activeSelf) { //h ne lehessen akkor megkattintani amikor load panel nyitva van, így nem fordulhat elő inkoztisztens állapot betoltéskor
			int previousSelectedIndex = roomScript.GetSelectedWallIndex ();

			//GameObject previousSelected =null;
			//previousSelected =GameObject.FindWithTag("SelectedWall");
			//int previousSelectedIndex = GameObject.FindWithTag("SelectedWall").transform.GetSiblingIndex();
	
			//ha van kijelolt es ez a kijelolt nem onmaga akkor
			if (previousSelectedIndex > -1 && previousSelectedIndex != transform.GetSiblingIndex ()) {
				GameObject previousSelected = roomScript.transform.GetChild (previousSelectedIndex).gameObject;
				previousSelected.GetComponent<WallScript> ().SetInactive ();
				previousSelected.GetComponent<WallScript> ().selected = false;
			}

			gameObject.tag = "SelectedWall";
			GameObject.Find ("MenuRoot").GetComponent<MakeRoomMenu> ().ChangeToAlterMenu (true);

			if (!isPanelOpenedYet) {
				instantiatedPanel = Instantiate (panel, new Vector3 (-3, 2.5f, 2f), Quaternion.identity);
				isPanelOpenedYet = true;

				//instantiatedPanel.transform.GetChild (0).GetComponent<WallCustomizePanel> ().wall=gameObject; 
			} else {
				Destroy (instantiatedPanel);
				isPanelOpenedYet = false;
			}


			selected = true;
			roomScript.SetSelectedWallIndex (transform.GetSiblingIndex ());
			if (selectedMaterial != null) {
				_renderer.material = selectedMaterial;
			}

			roomScript.isRotatable = false;
		}
	}

	public void Save(){

		BinaryFormatter binary = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/"+roomScript.GetSaveFolderName() +"/wall" + transform.GetSiblingIndex().ToString() + ".dat");
		WallData dat = new WallData ();

		dat.siblingIndex = gameObject.transform.GetSiblingIndex();
		dat.px = gameObject.transform.position.x;
		dat.py = gameObject.transform.position.y;
		dat.pz = gameObject.transform.position.z;

		dat.sx = gameObject.transform.localScale.x;
		dat.sy = gameObject.transform.localScale.y;
		dat.sz = gameObject.transform.localScale.z;

		dat.rx = gameObject.transform.localRotation.x;
		dat.ry = gameObject.transform.localRotation.y;
		dat.rz = gameObject.transform.localRotation.z;
		dat.rw = gameObject.transform.localRotation.w;

		binary.Serialize (file, dat);
		file.Close ();
	}

	public void Load(int id){
		if (File.Exists (Application.persistentDataPath +"/"+roomScript.GetSaveFolderName() +"/wall" + id.ToString () + ".dat")){
			
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/"+roomScript.GetSaveFolderName() +"/wall" + id.ToString () + ".dat",FileMode.Open);
			WallData dat = (WallData)binary.Deserialize (file);
			file.Close ();

			transform.localRotation = new Quaternion (dat.rx, dat.ry, dat.rz,dat.rw);
			transform.localScale = new Vector3 (dat.sx,dat.sy,dat.sz);
			transform.position = new Vector3(dat.px,dat.py,dat.pz);
			transform.SetSiblingIndex( dat.siblingIndex);	
		}
			
	}

	public void Delete(){
		string path = Application.persistentDataPath + "/"+roomScript.GetSaveFolderName() +"/wall" + transform.GetSiblingIndex() + ".dat";
		if (File.Exists (path)) {

			SetInactive ();

			roomScript.SetSelectedWallIndex (-1);
			transform.parent = null;
			Destroy (gameObject);

			#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh();
			#endif


			//print (transform.parent.transform.childCount);

			string filePath=path.Replace('\\','/');
			File.SetAttributes (filePath, FileAttributes.Normal);
			File.Delete (filePath);

			//transform.parent.GetComponent<Room> ().Save ();

		}
	}

	public bool IsSelected(){
		return selected;
	}
}
