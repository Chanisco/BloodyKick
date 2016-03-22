
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[SerializeField] Texture[] menuItems;
	[SerializeField] Texture backButton;
	[SerializeField] GameObject blankObject;
	[SerializePrivateVariables] int index = 0;
	[SerializePrivateVariables] Vector3 spawnLoc;
	[SerializePrivateVariables] Vector3 optionsSpawnLoc;
	[SerializePrivateVariables] Vector3 backbuttonSpawnLoc;
	[SerializePrivateVariables] public bool menu = false;
	[SerializePrivateVariables] bool options = false;
	[SerializePrivateVariables] bool controls = false;
	[SerializePrivateVariables] bool cSelect = false;
	[SerializePrivateVariables] GUIStyle style;
	[SerializePrivateVariables] float offset=0.2f;
	[SerializePrivateVariables] float musicVolume = 9;
	[SerializePrivateVariables] float SfxVolume = 9;
	[SerializePrivateVariables] string musicVolumeString = "9";
	[SerializePrivateVariables] string SfxVolumeString = "9";
	[SerializeField] int difficulty;
	[SerializeField] MainMenuCameraMovement cam;
	[SerializeField] Texture UI;
	[SerializeField] Texture[] arenaTextures;
	[SerializePrivateVariables] private int choise1 = 0;
	[SerializePrivateVariables] private int choise2 = 1;
	[SerializePrivateVariables] private int count = 0;
	[SerializeField] float random;
	[SerializePrivateVariables] CharacterAnimation chara;
	[SerializePrivateVariables] CharacterAnimation charb;
	[SerializeField] Texture startButton;
	[SerializeField] Texture arrowNotSelected;
	[SerializeField] Texture arrowSelected;
	[SerializeField] Texture arrowNotSelectedR;
	[SerializeField] Texture arrowSelectedR;
	[SerializeField] Font karate;
	[SerializePrivateVariables] int arenaSelected = 0;
	[SerializePrivateVariables] Texture usingarrowL;
	[SerializePrivateVariables] Texture usingarrowR;
	[SerializePrivateVariables] Texture usingarrowRL;
	[SerializePrivateVariables] Texture usingarrowRR;
	[SerializePrivateVariables] Texture usingarrowArena;
	[SerializePrivateVariables] Texture usingarrowArenaR;

	void Start(){
		style = new GUIStyle ();
		style.fontSize = (24 * 1920) / Screen.width;
		style.font = karate;
		cam = Camera.main.GetComponent<MainMenuCameraMovement>();
		spawnLoc = new Vector3 (0, 11, 0);
		backbuttonSpawnLoc = new Vector3 (12.4f, -4.1f, 30);
		random = Random.Range (200, 400);
		menu = false;
		options = false;
		usingarrowL = arrowNotSelected;
		usingarrowR = arrowNotSelectedR;
		usingarrowRL = arrowNotSelected;
		usingarrowRR = arrowNotSelectedR;
		usingarrowArena = arrowNotSelected;
		usingarrowArenaR = arrowNotSelectedR;
		chara = GameObject.FindGameObjectWithTag ("Chara").GetComponent<CharacterAnimation> ();
		charb = GameObject.FindGameObjectWithTag ("Charb").GetComponent<CharacterAnimation> ();
		//chara.PlayAnimation ("Idle");
		//charb.PlayAnimation ("Idle");
	}


	void OnGUI(){
		index = 0;
		if (menu) {
			///<summary>
			/// makes a button for each menu texture you add to Texture[] menuItems, with index for spacing
			///</summary>
			//	for(int i =0; i<2;i++){
			foreach (Texture item in menuItems) {
				if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * (0.13f + (0.172f * index)), Screen.width * 0.2f, Screen.height * 0.15f), "", style)) {
					if (index == 0) {
						if (menu) {
						//	AudioController.Instance.PlaySound(
							cam.MoveCam (3);
							cSelect = true;
							menu = false;
							options = false;
							controls = false;
						}
					} else if (index == 1) {
						if (menu) {
							cam.MoveCam (2);
							options = true;
							cSelect = false;
							menu = false;
							controls = false;
						}
					} else if (index == 2) {
						if (menu) {
							cam.MoveCam (4);
							menu = false;
							controls = true;
							options = false;
						}
					}
					else if (index == 3) {
						if (menu) {
							cam.MoveCam (5);
							menu = false;
							controls = true;
							options = false;
						}
					} else if (index == 4) {
						Application.Quit ();
					}
				}
				index++;
			}
		} else if (options && !cam.moving) {
			musicVolumeString = GUI.TextField (new Rect (Screen.width * 0.485f, Screen.height * 0.12f, Screen.width * 0.05f, Screen.height * 0.05f), musicVolume.ToString (), 1, style);
			float.TryParse (musicVolumeString, out musicVolume);
			SfxVolumeString = GUI.TextField (new Rect (Screen.width * 0.485f, Screen.height * 0.33f, Screen.width * 0.05f, Screen.height * 0.05f), musicVolume.ToString (), 1, style);
			float.TryParse (SfxVolumeString, out SfxVolume);
			if (GUI.Button (new Rect (Screen.width * 0.7f, Screen.height * 0.8f, Screen.width * 0.165f, Screen.height * 0.15f), "", style)) {
				cam.MoveCam (1);
				menu = true;
				options = false;
				controls = false;
			}
		} else if (cSelect && !cam.moving) {
			CheckMousePos ();
			count++;
			if (count > random) {
				if (Random.Range (0, 100) > 40) {
					if (Random.Range (0, 100) > 50) {
						//chara.PlayAnimation ("Punch");
					} else {
						//charb.PlayAnimation ("Punch");
					}
					count = 0;
					random = Random.Range (200, 400);
				} else {
					if (Random.Range (0, 100) > 50) {
						//chara.PlayAnimation ("Kick");
					} else {
						//charb.PlayAnimation ("Kick");
					}
					count = 0;
					random = Random.Range (200, 400);
				}
			}
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), UI);
			GUI.DrawTexture (new Rect (Screen.width * 0.35f, Screen.height * 0.24f, Screen.width * 0.3f, Screen.width * 0.1695f), arenaTextures [arenaSelected]);
			if (GUI.Button (new Rect (Screen.width * 0.06f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowL, style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.3f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowR, style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.65f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowRL, style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.89f, Screen.height * 0.6f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowRR, style)) {

			}
			if (GUI.Button (new Rect (Screen.width * 0.325f, Screen.height * 0.38f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowArena, style)) {
				arenaSelected--;
				if (arenaSelected == -1) {
					arenaSelected = 1;
				}
			}
			if (GUI.Button (new Rect (Screen.width * 0.66f, Screen.height * 0.38f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowArenaR, style)) {
				arenaSelected++;
				if (arenaSelected == 2) {
					arenaSelected = 0;
				}
			}
			GUI.DrawTexture (new Rect (Screen.width * 0.4f, Screen.height * 0.75f, Screen.width * 0.2f, Screen.height * 0.15f), startButton, ScaleMode.ScaleToFit);
			if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.75f, Screen.width * 0.2f, Screen.height * 0.15f), "", style)) {
				Application.LoadLevel (arenaSelected + 1);
			}
		} else if (controls) {
			if (GUI.Button (new Rect (Screen.width * 0.65f, Screen.height * 0.8f, Screen.width * 0.24f, Screen.height * 0.18f), "",style)) {
				cam.MoveCam (1);
				menu = true;
				controls = false;
				options = false;
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
			if (Input.mousePosition.x > Screen.width * 0.325f && Input.mousePosition.x < Screen.width * 0.345f) {
				usingarrowArena = arrowSelected;
			}
			if (Input.mousePosition.x > Screen.width * 0.66f && Input.mousePosition.x < Screen.width * 0.7f) {
				usingarrowArenaR = arrowSelectedR;
			}
		} else if (Input.mousePosition.y > Screen.height * 0.81f) {
//			Debug.Log ("height" + Input.mousePosition.y);
		}
	}

	/// <summary>
	/// adds a menuitem for each texture in Texture[] menuItems, and sets up all parameters
	/// </summary>
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

	/// <summary>
	/// adds a menuitem for each texture in Texture[] optionsItems, and sets up all parameters
	/// </summary>
	public void AddOptionItems(){
		blankObject.GetComponent<MenuItemScript> ().image = backButton;
		blankObject.GetComponent<MenuItemScript> ().backButton = true;
		Instantiate (blankObject, backbuttonSpawnLoc, Quaternion.Euler(new Vector3(69.215f,227.55f,176)));
	}

	/// <summary>
	/// Deletes the UI
	/// </summary>
	public void DeleteUI(){
		foreach (GameObject menuItem in GameObject.FindGameObjectsWithTag("MenuItem")) {
			Destroy (menuItem);
		}
	}
}
