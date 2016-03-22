
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[SerializeField] Texture[] menuItems;
	[SerializeField] Texture[] optionsItems;
	[SerializeField] Texture backButton;
	[SerializeField] GameObject blankObject;
	[SerializePrivateVariables] int index = 0;
	[SerializePrivateVariables] Vector3 spawnLoc;
	[SerializePrivateVariables] Vector3 optionsSpawnLoc;
	[SerializePrivateVariables] Vector3 backbuttonSpawnLoc;
	[SerializePrivateVariables] public bool menu = false;
	[SerializePrivateVariables] bool options = false;
	[SerializePrivateVariables] bool cSelect = false;
	[SerializePrivateVariables] GUIStyle style;
	[SerializeField] float musicVolume=1;
	[SerializeField] float effectVolume=1;
	[SerializePrivateVariables] float offset=0.2f;
	[SerializeField] int difficulty;
	[SerializeField] MainMenuCameraMovement cam;
	[SerializeField] GameObject[] characters;
	[SerializeField] Texture UI;
	[SerializeField] Texture[] arenaTextures;
	[SerializeField] private int choise1 = 0;
	[SerializeField] private int choise2 = 1;
	[SerializeField] private int count = 0;
	[SerializeField] float random;
	[SerializePrivateVariables] GameObject chara;
	[SerializePrivateVariables] GameObject charb;
	[SerializeField] Texture arrowNotSelected;
	[SerializeField] Texture arrowSelected;
	[SerializeField] Texture arrowNotSelectedR;
	[SerializeField] Texture arrowSelectedR;
	[SerializePrivateVariables] int arenaSelected = 0;
	[SerializePrivateVariables] Texture usingarrowL;
	[SerializePrivateVariables] Texture usingarrowR;
	[SerializePrivateVariables] Texture usingarrowRL;
	[SerializePrivateVariables] Texture usingarrowRR;
	[SerializePrivateVariables] Texture usingarrowArena;
	[SerializePrivateVariables] Texture usingarrowArenaR;

	void Start(){
		style = new GUIStyle ();
		cam = Camera.main.GetComponent<MainMenuCameraMovement>();
		spawnLoc = new Vector3 (0, 11, 0);
		backbuttonSpawnLoc = new Vector3 (12.4f, -4.1f, 30);
		random = Random.Range (200, 400);
		menu = false;
		options = false;
//		usingArena = Gym;
		usingarrowL = arrowNotSelected;
		usingarrowR = arrowNotSelectedR;
		usingarrowRL = arrowNotSelected;
		usingarrowRR = arrowNotSelectedR;
		usingarrowArena = arrowNotSelected;
		usingarrowArenaR = arrowNotSelectedR;
		SetCharacters ();
	}


	void OnGUI(){
		index = 0;
		if (menu) {
			// makes a button for each menu texture you add to Texture[] menuItems, with index for spacing
		//	for(int i =0; i<2;i++){
			foreach (Texture item in menuItems) {
				if (GUI.Button (new Rect (Screen.width * 0.35f, Screen.height * (0.1f + (0.22f * index)), Screen.width * 0.3f, Screen.height * 0.18f), "", style)) {
					if (index == 0) {
						if (menu) {
							cam.MoveCam (3);
							cSelect = true;
							menu = false;
							options = false;
						}
					}
					 else if (index == 1) {
						if (menu) {
							cam.MoveCam (2);
							options = true;
							cSelect = false;
							menu = false;
						}
					} else if (index == 2) {
						
					}
					else if (index == 3) {
						Application.Quit ();
					}
				}
				index++;
			}
		} else if (options) {
			//musicVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.45f, Screen.height * 0.2f, Screen.width * 0.45f, Screen.height * 0.05f), musicVolume, 0, 100);
			//effectVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.45f, Screen.height * 0.39f, Screen.width * 0.45f, Screen.height * 0.05f), effectVolume, 0, 100);
			if (GUI.Button (new Rect (Screen.width * 0.7f, Screen.height * 0.8f, Screen.width * 0.165f, Screen.height * 0.15f), "", style)) {
				cam.MoveCam (1);
				menu = true;
				options = false;
			}
		} else if (cSelect && !cam.moving) {
			CheckMousePos ();
			count++;
			if (count>random) {
				if (Random.Range (0, 100) > 40) {
					if (Random.Range (0, 100) > 50) {
						chara.GetComponent<CharacterBehaviour> ().animator.PlayAnimation ("Punch");
					} else{
						charb.GetComponent<CharacterBehaviour> ().animator.PlayAnimation ("Punch");
					}
					count = 0;
					random = Random.Range (200, 400);
				} else {
					if (Random.Range (0, 100) > 50) {
						chara.GetComponent<CharacterBehaviour> ().animator.PlayAnimation ("Kick");
					} else{
						charb.GetComponent<CharacterBehaviour> ().animator.PlayAnimation ("Kick");
					}
					count = 0;
					random = Random.Range (200, 400);
				}
			}
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),UI);
			GUI.DrawTexture(new Rect(Screen.width*0.35f,Screen.height*0.24f,Screen.width*0.3f,Screen.width*0.1695f),arenaTextures[arenaSelected]);
			if (GUI.Button (new Rect (Screen.width * 0.06f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowL,style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.3f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowR,style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.65f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowRL,style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.89f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowRR,style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.32f, Screen.height * 0.38f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowArena,style)) {
				arenaSelected--;
				if (arenaSelected == -1) {
					arenaSelected = 1;
				}
			}
			if (GUI.Button (new Rect (Screen.width * 0.66f, Screen.height * 0.38f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowArenaR,style)) {
				arenaSelected++;
				if (arenaSelected == 2) {
					arenaSelected = 0;
				}
			}
			if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "Start")) {
				Application.LoadLevel (arenaSelected+1);
			}
		}
	}

	/// <summary>
	/// Checks the mouse position.
	/// </summary>
	void CheckMousePos(){
		usingarrowL = arrowNotSelected;
		usingarrowR = arrowNotSelectedR;
		usingarrowRL = arrowNotSelected;
		usingarrowRR = arrowNotSelectedR;
		usingarrowArena = arrowNotSelected;
		usingarrowArenaR = arrowNotSelectedR;
		if (Input.mousePosition.y > Screen.height * 0.35f && Input.mousePosition.y < Screen.height * 0.4f) {
			if (Input.mousePosition.x > Screen.width * 0.06f && Input.mousePosition.x < Screen.width * 0.08f) {
				usingarrowL = arrowSelected;
			}
			if (Input.mousePosition.x > Screen.width * 0.3f && Input.mousePosition.x < Screen.width * 0.32f) {
				usingarrowR = arrowSelectedR;
			}
			if (Input.mousePosition.x > Screen.width * 0.65f && Input.mousePosition.x < Screen.width * 0.67f) {
				usingarrowRL = arrowSelected;
			}
			if (Input.mousePosition.x > Screen.width * 0.89f && Input.mousePosition.x < Screen.width * 0.91f) {
				usingarrowRR = arrowSelectedR;
			}
		} else if (Input.mousePosition.y > Screen.height * 0.57f && Input.mousePosition.y < Screen.height * 0.62f) {
			if (Input.mousePosition.x > Screen.width * 0.32f && Input.mousePosition.x < Screen.width * 0.34f) {
				usingarrowArena = arrowSelected;
			}
			if (Input.mousePosition.x > Screen.width * 0.66f && Input.mousePosition.x < Screen.width * 0.7f) {
				usingarrowArenaR = arrowSelectedR;
			}
		} else if (Input.mousePosition.y > Screen.height * 0.81f) {
			Debug.Log ("height" + Input.mousePosition.y);
		}
	}

	public void SetCharacters(){
		chara = Instantiate (characters [choise1], characters [choise1].transform.position, characters [choise1].transform.rotation) as GameObject;
		charb = Instantiate (characters [choise2], characters [choise2].transform.position, characters [choise2].transform.rotation) as GameObject;
		chara.GetComponent<CharacterBehaviour> ().animator.PlayAnimation ("Idle");
		charb.GetComponent<CharacterBehaviour> ().animator.PlayAnimation ("Idle");
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
			blankObject.GetComponent<MenuItemScript> ().insertTime = 0.05f;
			Instantiate (blankObject,spawnLoc,blankObject.transform.rotation);
			index++;
		}
		index = 0;
	}

	// adds a menuitem for each texture in Texture[] optionsItems, and sets up all parameters
	public void AddOptionItems(){
		index = 0;
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
		index = 0;
	}
	
	public void DeleteUI(){
		foreach (GameObject menuItem in GameObject.FindGameObjectsWithTag("MenuItem")) {
			Destroy (menuItem);
		}
	}
}
