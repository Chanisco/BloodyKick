using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[SerializeField] Texture[] menuItems;
	[SerializeField] Texture[] optionsItems;
	[SerializeField] Texture backButton;
	[SerializeField] GameObject blankObject;
	[SerializeField] int index = 0;
	[SerializePrivateVariables] Vector3 spawnLoc;
	[SerializePrivateVariables] Vector3 optionsSpawnLoc;
	[SerializePrivateVariables] Vector3 backbuttonSpawnLoc;
	[SerializeField] public bool menu = false;
	[SerializeField] bool options = false;
	[SerializeField] GUIStyle style;
	[SerializeField] float musicVolume=1;
	[SerializeField] float effectVolume=1;
	[SerializeField] int difficulty;
	[SerializeField] MainMenuCameraMovement cam;

	void Start(){
		style = new GUIStyle ();
		cam = Camera.main.GetComponent<MainMenuCameraMovement>();
		spawnLoc = new Vector3 (0, 11, 0);
		backbuttonSpawnLoc = new Vector3 (12.4f, -4.1f, 30);
		menu = false;
		options = false;
	}


	void OnGUI(){
		index = 0;
		if (menu) {
			// makes a button for each menu texture you add to Texture[] menuItems, with index for spacing
			foreach (Texture item in menuItems) {
				if (GUI.Button (new Rect (Screen.width * 0.3f, Screen.height * (0.12f + (0.22f * index)), Screen.width * 0.4f, Screen.height * 0.15f), "", style)) {
					if (index == 0) {
						if (menu) {
							Application.LoadLevel (1);
							Debug.Log ("pl1vspl2");
						}
						// If AI is made
					}/* else if (index == 1) {
						if (menu) {
							Debug.Log ("pl1vscpu");
						}*/
					 else if (index == 1) {
						if (menu) {
							//DeleteUI ();
							cam.MoveCam (2);
							options = true;
							menu = false;
							//AddOptionItems ();
						}
					} else if (index == 2) {
						Application.Quit ();
					}
				}
				index++;
			}
		} else if (options){
			musicVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.45f, Screen.height * 0.2f, Screen.width * 0.45f, Screen.height * 0.05f), musicVolume, 0, 100);
			effectVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.45f, Screen.height * 0.39f, Screen.width * 0.45f, Screen.height * 0.05f), effectVolume, 0, 100);
			if (GUI.Button (new Rect (Screen.width * 0.7f, Screen.height * 0.8f, Screen.width * 0.165f, Screen.height * 0.15f), "", style)) {
				//DeleteUI ();
				//AddMenuItems ();
				cam.MoveCam (1);
				menu = true;
				options = false;
			}
		}
	}

	// adds a menuitem for each texture in Texture[] menuItems, and sets up all parameters
	public void AddMenuItems(){
		index = 0;
		menu = true;
		foreach (Texture item in menuItems) {
			blankObject.GetComponent<MenuItemScript> ().index = index;
			blankObject.GetComponent<MenuItemScript> ().options = false;
			blankObject.GetComponent<MenuItemScript> ().backButton = false;
			blankObject.GetComponent<MenuItemScript> ().image = menuItems [index];
			//blankObject.GetComponent<MenuItemScript> ().checkPlacement ();
			blankObject.GetComponent<MenuItemScript> ().insertTime = 0.05f;
			Instantiate (blankObject,spawnLoc,blankObject.transform.rotation);
			index++;
		}
		index = 0;
	}

	// adds a menuitem for each texture in Texture[] optionsItems, and sets up all parameters
	public void AddOptionItems(){
		index = 0;
		//options = true;
		foreach (Texture optionItem in optionsItems) {
			blankObject.GetComponent<MenuItemScript> ().index = index;
			blankObject.GetComponent<MenuItemScript> ().options = true;
			blankObject.GetComponent<MenuItemScript> ().backButton = false;
			blankObject.GetComponent<MenuItemScript> ().image = optionsItems [index];
			blankObject.GetComponent<MenuItemScript> ().insertTime = 0.05f;
			optionsSpawnLoc = new Vector3 (5.96f, 2.61f, 35.43f);
			Instantiate (blankObject, optionsSpawnLoc, Quaternion.Euler(new Vector3(90,20,0)));
			index++;
		}
		blankObject.GetComponent<MenuItemScript> ().image = backButton;
		blankObject.GetComponent<MenuItemScript> ().backButton = true;
		Instantiate (blankObject, backbuttonSpawnLoc, Quaternion.Euler(new Vector3(69.215f,227.55f,176)));
		//menu = false;
		index = 0;
	}
	
	public void DeleteUI(){
		foreach (GameObject menuItem in GameObject.FindGameObjectsWithTag("MenuItem")) {
			Destroy (menuItem);
		}
	}
}
