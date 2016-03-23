
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[SerializeField] Texture[] menuItems;
	[SerializeField] Texture backButton;
	[SerializeField] GameObject blankObject;
	[SerializeField] Texture[] characterFrames;
	int index = 0;
	Vector3 spawnLoc;
	Vector3 optionsSpawnLoc;
	Vector3 backbuttonSpawnLoc;
	public bool menu = false;
	bool options = false;
	bool controls = false;
	bool cSelect = false;
	GUIStyle style;
	float offset=0.2f;
	float musicVolume = 9;
	float SfxVolume = 9;
	string musicVolumeString = "9";
	string SfxVolumeString = "9";
	[SerializeField] int difficulty;
	[SerializeField] MainMenuCameraMovement cam;
	[SerializeField] Texture UI;
	[SerializeField] Texture[] arenaTextures;
	private int choise1 = 0;
	private int choise2 = 1;
	private int count = 0;
	private bool pl1Choosing = true;
	private string name1 = "Cena";
	private string name2= "John";
	[SerializeField] float random;
	//CharacterAnimation chara;
	//CharacterAnimation charb;
	[SerializeField] Texture arrowNotSelected;
	[SerializeField] Texture arrowSelected;
	[SerializeField] Texture arrowNotSelectedR;
	[SerializeField] Texture arrowSelectedR;
	[SerializeField] Font karate;
	int arenaSelected = 0;
	Texture usingarrowArena;
	Texture usingarrowArenaR;

	void Start(){
		style = new GUIStyle ();
		style.fontSize = (120 * Screen.width) / 1920;
		style.font = karate;
		cam = Camera.main.GetComponent<MainMenuCameraMovement>();
		spawnLoc = new Vector3 (0, 11, 0);
		backbuttonSpawnLoc = new Vector3 (12.4f, -4.1f, 30);
		random = Random.Range (200, 400);
		menu = false;
		options = false;
		usingarrowArena = arrowNotSelected;
		usingarrowArenaR = arrowNotSelectedR;
		//chara = GameObject.FindGameObjectWithTag ("Chara").GetComponent<CharacterAnimation> ();
		//charb = GameObject.FindGameObjectWithTag ("Charb").GetComponent<CharacterAnimation> ();
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
				//AudioController.Instance.get
				cam.MoveCam (1);
				menu = true;
				options = false;
				controls = false;
			}
		} else if (cSelect && !cam.moving) {
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
			style.alignment = TextAnchor.MiddleLeft;
			GUI.TextField (new Rect (Screen.width * 0.02f, Screen.height * 0.195f, Screen.width * 0.4f, Screen.height * 0.1f), name1, style);
			style.alignment = TextAnchor.MiddleRight;
			GUI.TextField (new Rect (Screen.width * 0.7f, Screen.height * 0.195f, Screen.width * 0.28f, Screen.height * 0.1f), name2, style);
			GUI.DrawTexture (new Rect (Screen.width * 0.35f, Screen.height * 0.24f, Screen.width * 0.3f, Screen.width * 0.1695f), arenaTextures [arenaSelected]);
			CheckMousePos ();
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
		usingarrowArena = arrowNotSelected;
		usingarrowArenaR = arrowNotSelectedR;
		if (Input.mousePosition.y > Screen.height * 0.57f && Input.mousePosition.y < Screen.height * 0.62f) {
			if (Input.mousePosition.x > Screen.width * 0.325f && Input.mousePosition.x < Screen.width * 0.345f) {
				usingarrowArena = arrowSelected;
			}
			if (Input.mousePosition.x > Screen.width * 0.66f && Input.mousePosition.x < Screen.width * 0.7f) {
				usingarrowArenaR = arrowSelectedR;
			}
		} else if (Input.mousePosition.y > Screen.height * 0.81f) {
			if (Input.mousePosition.x < Screen.width * 0.5f) {
				if (((Input.mousePosition.x/Screen.width)-((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)> 0.335f &&
					((Input.mousePosition.x/Screen.width)-(((Input.mousePosition.y/Screen.height)-0.81f)*0.33f))< 0.435f ) {
					if (!pl1Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							ArenaController.Instance.Player2 = CharacterEnum.Cena;
							Application.LoadLevel (arenaSelected + 1);
						}
						name2 = "Cena";
						GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = true;
						GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = false;
						GUI.DrawTexture (new Rect (Screen.width * 0.335f, Screen.height * 0.01f, Screen.width * 0.159f, Screen.height * 0.21f), characterFrames [3], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							ArenaController.Instance.Player1 = CharacterEnum.Cena;
							pl1Choosing = false;
						}
						name1 = "Cena";
						GameObject.FindGameObjectWithTag ("C1").GetComponent<MeshRenderer>().enabled = true;
						GameObject.FindGameObjectWithTag ("J1").GetComponent<MeshRenderer>().enabled = false;
						GUI.DrawTexture (new Rect (Screen.width * 0.335f, Screen.height * 0.01f, Screen.width * 0.159f, Screen.height * 0.21f), characterFrames [0], ScaleMode.StretchToFill);
					}
				}else if (((Input.mousePosition.x/Screen.width)-(((Input.mousePosition.y/Screen.height)-0.81f)*0.33f))> 0.435f &&
					((Input.mousePosition.x/Screen.width)-((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)< 0.535f ) {
					if (!pl1Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							if (Random.Range (0, 100) > 50) {
								name2 = "Cena";
								GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = true;
								GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = false;
								ArenaController.Instance.Player2 = CharacterEnum.Cena;
							} else {
								name2 = "John";
								GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = false;
								GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = true;
								ArenaController.Instance.Player2= CharacterEnum.John;
							}
							Application.LoadLevel (arenaSelected + 1);
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [4], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							if (Random.Range (0, 100) > 50) {
								name1 = "Cena";
								GameObject.FindGameObjectWithTag ("C1").GetComponent<MeshRenderer>().enabled = true;
								GameObject.FindGameObjectWithTag ("J1").GetComponent<MeshRenderer>().enabled = false;
								ArenaController.Instance.Player1= CharacterEnum.Cena;
							} else {
								name1 = "John";
								GameObject.FindGameObjectWithTag ("C1").GetComponent<MeshRenderer>().enabled = false;
								GameObject.FindGameObjectWithTag ("J1").GetComponent<MeshRenderer>().enabled = true;
								ArenaController.Instance.Player1 = CharacterEnum.John;
							}
							pl1Choosing = false;
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [1], ScaleMode.StretchToFill);
					}
				}
			} else {
				if (((Input.mousePosition.x/Screen.width)+((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)> 0.5f &&
					((Input.mousePosition.x/Screen.width)+((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)< 0.56f ) {
					if (!pl1Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							if (Random.Range (0, 100) > 50) {
								name2 = "Cena";
								GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = true;
								GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = false;
								ArenaController.Instance.Player2 = CharacterEnum.Cena;
							} else {
								name2 = "John";
								GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = false;
								GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = true;
								ArenaController.Instance.Player2= CharacterEnum.John;
							}
							Application.LoadLevel (arenaSelected + 1);
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [4], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							if (Random.Range (0, 100) > 50) {
								name1 = "Cena";
								GameObject.FindGameObjectWithTag ("C1").GetComponent<MeshRenderer>().enabled = true;
								GameObject.FindGameObjectWithTag ("J1").GetComponent<MeshRenderer>().enabled = false;
								ArenaController.Instance.Player1 = CharacterEnum.Cena;
							} else {
								name1 = "John";
								GameObject.FindGameObjectWithTag ("C1").GetComponent<MeshRenderer>().enabled = false;
								GameObject.FindGameObjectWithTag ("J1").GetComponent<MeshRenderer>().enabled = true;
								ArenaController.Instance.Player1 = CharacterEnum.John;
							}
							pl1Choosing = false;
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [1], ScaleMode.StretchToFill);
					}
				}else if (((Input.mousePosition.x/Screen.width)+(((Input.mousePosition.y/Screen.height)-0.81f)*0.33f))> 0.56f &&
					((Input.mousePosition.x/Screen.width)+((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)< 0.66f ) {
					if (!pl1Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							ArenaController.Instance.Player2 = CharacterEnum.John;
							Application.LoadLevel (arenaSelected + 1);
						}
						name2 = "John";
						GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = false;
						GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = true;
						GUI.DrawTexture (new Rect (Screen.width * 0.508f, Screen.height * 0.01f, Screen.width * 0.159f, Screen.height * 0.21f), characterFrames [5], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							ArenaController.Instance.Player1 = CharacterEnum.John;
							pl1Choosing = false;
						}
						name1 = "John";
						GameObject.FindGameObjectWithTag ("C1").GetComponent<MeshRenderer>().enabled = false;
						GameObject.FindGameObjectWithTag ("J1").GetComponent<MeshRenderer>().enabled = true;
						GUI.DrawTexture (new Rect (Screen.width * 0.508f, Screen.height * 0.01f, Screen.width * 0.159f, Screen.height * 0.21f), characterFrames [2], ScaleMode.StretchToFill);
					}
				}
			}

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
