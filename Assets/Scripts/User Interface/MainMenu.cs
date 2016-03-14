using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[SerializeField] Texture[] menuItems;
	[SerializeField] Texture[] optionsItems;
	[SerializeField] Texture backButton;
	[SerializeField] GameObject blankObject;
	[SerializeField] int index = 0;
	[SerializeField] Vector3 spawnLoc;
	[SerializeField] bool menu = true;
	[SerializeField] bool options = false;
	[SerializeField] GUIStyle style;
	[SerializeField] float musicVolume=1;
	[SerializeField]float effectVolume=1;
	[SerializeField] int difficulty;

	void Start(){
		style = new GUIStyle ();
		//AddMenuItems ();
	}


	void OnGUI(){
		index = 0;
		if (menu) {
			foreach (Texture item in menuItems) {
				if (GUI.Button (new Rect (Screen.width * 0.3f, Screen.height * (0.15f + (0.2f * index)), Screen.width * 0.4f, Screen.height * 0.15f), "", style)) {
					if (index == 0) {
						if (menu) {
							Application.LoadLevel (1);
							Debug.Log ("pl1vspl2");
						}
						// open new scene
					}/* else if (index == 1) {
						if (menu) {
							Debug.Log ("pl1vscpu");
						}
						// open new scene*/
					 else if (index == 1) {
						if (menu) {
							DeleteUI ();
							AddOptionItems ();
						}
					} else if (index == 2) {
						Application.Quit ();
					}
				}
				index++;
			}
		} else {
			musicVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.45f, Screen.height * 0.2f, Screen.width * 0.45f, Screen.height * 0.05f), musicVolume, 0, 100);
			effectVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.45f, Screen.height * 0.39f, Screen.width * 0.45f, Screen.height * 0.05f), effectVolume, 0, 100);
		}
	/*	if (GUI.Button (new Rect (Screen.width * 0.7f, Screen.height * 0.8f, Screen.width * 0.165f, Screen.height * 0.15f), ""/*, style)) {
			if (menu) {

			} else {
				DeleteUI ();
				AddMenuItems ();
				menu = true;
			}
		}*/
	}

	public void AddMenuItems(){
		index = 0;
		menu = true;
		foreach (Texture item in menuItems) {
			blankObject.GetComponent<MenuItemScript> ().index = index;
			blankObject.GetComponent<MenuItemScript> ().options = false;
			blankObject.GetComponent<MenuItemScript> ().backButton = false;
			blankObject.GetComponent<MenuItemScript> ().image = menuItems [index];
			blankObject.GetComponent<MenuItemScript> ().checkPlacement ();
			blankObject.GetComponent<MenuItemScript> ().insertTime = 0.05f;
			Instantiate (blankObject,spawnLoc,blankObject.transform.rotation);
			index++;
		}
		index = 0;
	}
	
	public void AddOptionItems(){
		index = 0;
		options = true;
		foreach (Texture optionItem in optionsItems) {
			blankObject.GetComponent<MenuItemScript> ().index = index;
			blankObject.GetComponent<MenuItemScript> ().options = true;
			blankObject.GetComponent<MenuItemScript> ().backButton = false;
			blankObject.GetComponent<MenuItemScript> ().image = optionsItems [index];
			blankObject.GetComponent<MenuItemScript> ().insertTime = 0.05f;
			Instantiate (blankObject, spawnLoc, blankObject.transform.rotation);
			index++;
		}
		blankObject.GetComponent<MenuItemScript> ().image = backButton;
		blankObject.GetComponent<MenuItemScript> ().backButton = true;
		Instantiate (blankObject, spawnLoc, blankObject.transform.rotation);
		menu = false;
		index = 0;
	}
	
	public void DeleteUI(){
		foreach (GameObject menuItem in GameObject.FindGameObjectsWithTag("MenuItem")) {
			Destroy (menuItem);
		}
	}
}
