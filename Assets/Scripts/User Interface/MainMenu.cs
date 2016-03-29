using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public static MainMenu Instance;
	[SerializeField] Texture[] menuItems;
	[SerializeField] Texture backButton;
	[SerializeField] Texture backButtonGUI;
	[SerializeField] Texture startButton;
	[SerializeField] Texture overlay;
	[SerializeField] Texture overlayChar;
	[SerializeField] GameObject blankObject;
	[SerializeField] Texture[] characterFrames;
	[SerializeField] GUIStyle slider1;
	[SerializeField] GUIStyle slider2;
	[SerializeField] GUISkin sliderSkin;
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
	public string musicVolumeString = "9";
	public string SfxVolumeString = "9";
	[SerializeField] int difficulty;
	[SerializeField] MainMenuCameraMovement cam;
	[SerializeField] Texture UI;
	[SerializeField] Texture[] arenaTextures;
	private int choise1 = 0;
	private int choise2 = 1;
	private int count = 0;
	private bool pl1Choosing = true;
	private bool pl2Choosing = false;
	private string name1 = "Cena";
	private string name2= "John";
	[SerializeField] float random;
	[SerializeField] Texture arrowNotSelected;
	[SerializeField] Texture arrowSelected;
	[SerializeField] Texture arrowNotSelectedR;
	[SerializeField] Texture arrowSelectedR;
	[SerializeField] Font karate;
	int arenaSelected = 0;
	Texture usingarrowArena;
	Texture usingarrowArenaR;

	void Awake(){
		Instance = this;
	}
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
	}


	void OnGUI(){
		GUI.skin.horizontalSlider = slider1;
		GUI.skin.horizontalSliderThumb = slider2;
		index = 0;
		if (menu) {
			///<summary>
			/// makes a button for each menu texture you add to Texture[] menuItems, with index for spacing
			///</summary>
			foreach (Texture item in menuItems) {
				if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * (0.13f + (0.172f * index)), Screen.width * 0.2f, Screen.height * 0.15f), "", style)) {
					if (index == 0) {
						if (menu) {
							//AudioController.Instance.CallSFXSound("Button");
							cam.MoveCam (3);
							cSelect = true;
							menu = false;
							options = false;
							controls = false;
						}
					} else if (index == 1) {
						if (menu) {
							AudioController.Instance.CallSFXSound("Button");
							cam.MoveCam (2);
							options = true;
							cSelect = false;
							menu = false;
							controls = false;
						}
					} else if (index == 2) {
						if (menu) {
							AudioController.Instance.CallSFXSound("Button");
							cam.MoveCam (4);
							menu = false;
							controls = true;
							options = false;
						}
					}
					else if (index == 3) {
						if (menu) {
							AudioController.Instance.CallSFXSound("Button");
							cam.MoveCam (5);
							menu = false;
							controls = true;
							options = false;
						}
					} else if (index == 4) {
						AudioController.Instance.CallSFXSound("Button");
						Application.Quit ();
					}
				}
				index++;
			}
		} else if (options && !cam.moving) {
			musicVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.6f, Screen.height * 0.17f, Screen.width * 0.3f, Screen.height * 0.15f), musicVolume, 0f, 9f);
			SfxVolume = GUI.HorizontalSlider (new Rect (Screen.width * 0.6f, Screen.height * 0.37f, Screen.width * 0.3f, Screen.height * 0.15f), SfxVolume, 0f, 9f);
			GUI.TextField (new Rect (Screen.width * 0.49f, Screen.height * 0.13f, Screen.width * 0.05f, Screen.height * 0.05f), musicVolume.ToString (), 1, style);
			GUI.TextField (new Rect (Screen.width * 0.49f, Screen.height * 0.34f, Screen.width * 0.05f, Screen.height * 0.05f), SfxVolume.ToString (), 1, style);
			if (GUI.Button (new Rect (Screen.width * 0.7f, Screen.height * 0.8f, Screen.width * 0.165f, Screen.height * 0.15f), "", style)) {
				AudioController.Instance.CallSFXSound("Button");
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
					} else {
					}
					count = 0;
					random = Random.Range (200, 400);
				} else {
					if (Random.Range (0, 100) > 50) {
					} else {
					}
					count = 0;
					random = Random.Range (200, 400);
				}
			}
			GUI.DrawTexture (new Rect (Screen.width * 0.35f, Screen.height * 0.24f, Screen.width * 0.3f, Screen.width * 0.1695f), arenaTextures [arenaSelected]);
			style.alignment = TextAnchor.MiddleCenter;
			if (!pl1Choosing && !pl2Choosing) {
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), overlayChar);
				if(GUI.Button(new Rect(Screen.width*0.35f,Screen.height*0.65f,Screen.width*0.3f,Screen.height*0.15f),startButton,style)){
					AudioController.Instance.CallSFXSound("Button");
					Application.LoadLevel (arenaSelected + 1);
				}
			} else {
				GUI.DrawTexture (new Rect (Screen.width * 0.35f, Screen.height * 0.24f, Screen.width * 0.3f, Screen.width * 0.1695f), overlay);
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), UI);
			}
			if(GUI.Button(new Rect(Screen.width*0.4f,Screen.height*0.85f,Screen.width*0.2f,Screen.height*0.1f),backButtonGUI,style)){
				pl1Choosing = true;
				pl2Choosing = false;
				AudioController.Instance.CallSFXSound("Button");
				cam.MoveCam (1);
				cSelect = false;
				menu = true;
			}
			style.alignment = TextAnchor.MiddleLeft;
			GUI.TextField (new Rect (Screen.width * 0.02f, Screen.height * 0.195f, Screen.width * 0.4f, Screen.height * 0.1f), name1, style);
			style.alignment = TextAnchor.MiddleRight;
			GUI.TextField (new Rect (Screen.width * 0.7f, Screen.height * 0.195f, Screen.width * 0.28f, Screen.height * 0.1f), name2, style);
			CheckMousePos ();
			if (!pl1Choosing && !pl2Choosing) {
				if (GUI.Button (new Rect (Screen.width * 0.315f, Screen.height * 0.38f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowArena, style)) {
					arenaSelected--;
					AudioController.Instance.CallSFXSound ("Button");
					if (arenaSelected == -1) {
						arenaSelected = 1;
					}
				}
				if (GUI.Button (new Rect (Screen.width * 0.66f, Screen.height * 0.38f, Screen.width * 0.02f, Screen.height * 0.0428f), usingarrowArenaR, style)) {
					arenaSelected++;
					AudioController.Instance.CallSFXSound ("Button");
					if (arenaSelected == 2) {
						arenaSelected = 0;
					}
				}
			}
		} else if (controls) {
			if (GUI.Button (new Rect (Screen.width * 0.65f, Screen.height * 0.8f, Screen.width * 0.24f, Screen.height * 0.18f), "",style)) {
				AudioController.Instance.CallSFXSound("Button");
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
		if (Input.mousePosition.y > Screen.height * 0.57f && Input.mousePosition.y < Screen.height * 0.62f &&
			!pl1Choosing && !pl2Choosing) {
			if (Input.mousePosition.x > Screen.width * 0.315f && Input.mousePosition.x < Screen.width * 0.345f) {
				usingarrowArena = arrowSelected;
			}
			if (Input.mousePosition.x > Screen.width * 0.66f && Input.mousePosition.x < Screen.width * 0.7f) {
				usingarrowArenaR = arrowSelectedR;
			}
		} else if (Input.mousePosition.y > Screen.height * 0.81f) {
			if (Input.mousePosition.x < Screen.width * 0.5f) {
				if (((Input.mousePosition.x/Screen.width)-((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)> 0.335f &&
					((Input.mousePosition.x/Screen.width)-(((Input.mousePosition.y/Screen.height)-0.81f)*0.33f))< 0.435f ) {
					if (!pl1Choosing && !pl2Choosing) {

					}
					else if (!pl1Choosing && pl2Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							AudioController.Instance.CallSFXSound("Button");
							ArenaController.Instance.Player2 = CharacterEnum.Cena;
							if (arenaSelected == 0) {
								AudioController.Instance.ChangeBackgroundMusic ("The Ring");
							} else {
								AudioController.Instance.ChangeBackgroundMusic ("The Docs");
							}
							pl1Choosing = false;
							pl2Choosing = false;
						}
						name2 = "Cena";
						GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = true;
						GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = false;
						GUI.DrawTexture (new Rect (Screen.width * 0.335f, Screen.height * 0.01f, Screen.width * 0.159f, Screen.height * 0.21f), characterFrames [3], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							AudioController.Instance.CallSFXSound("Button");
							ArenaController.Instance.Player1 = CharacterEnum.Cena;
							pl1Choosing = false;
							pl2Choosing = true;
						}
						name1 = "Cena";
						GameObject.FindGameObjectWithTag ("C1").GetComponent<MeshRenderer>().enabled = true;
						GameObject.FindGameObjectWithTag ("J1").GetComponent<MeshRenderer>().enabled = false;
						GUI.DrawTexture (new Rect (Screen.width * 0.335f, Screen.height * 0.01f, Screen.width * 0.159f, Screen.height * 0.21f), characterFrames [0], ScaleMode.StretchToFill);
					}
				}else if (((Input.mousePosition.x/Screen.width)-(((Input.mousePosition.y/Screen.height)-0.81f)*0.33f))> 0.435f &&
					((Input.mousePosition.x/Screen.width)-((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)< 0.535f ) {
					if (!pl1Choosing && !pl2Choosing) {

					}
					else if (!pl1Choosing && pl2Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							AudioController.Instance.CallSFXSound("Button");
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
						//	Arena.ArenaManagement.Instance.PlayBackgroundMusic (arenaSelected);
							if (arenaSelected == 0) {
								AudioController.Instance.ChangeBackgroundMusic ("The Ring");
							} else {
								AudioController.Instance.ChangeBackgroundMusic ("The Docs");
							}
							pl1Choosing = false;
							pl2Choosing = false;
							//Application.LoadLevel (arenaSelected + 1);
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [4], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							AudioController.Instance.CallSFXSound("Button");
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
							pl2Choosing = true;
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [1], ScaleMode.StretchToFill);
					}
				}
			} else {
				if (((Input.mousePosition.x/Screen.width)+((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)> 0.5f &&
					((Input.mousePosition.x/Screen.width)+((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)< 0.56f ) {
					if (!pl1Choosing && !pl2Choosing) {

					}
					else if (!pl1Choosing && pl2Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							AudioController.Instance.CallSFXSound("Button");
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
							//Arena.ArenaManagement.Instance.PlayBackgroundMusic (arenaSelected);
							if (arenaSelected == 0) {
								AudioController.Instance.ChangeBackgroundMusic ("The Ring");
							} else {
								AudioController.Instance.ChangeBackgroundMusic ("The Docs");
							}
							pl1Choosing = false;
							pl2Choosing = false;
							Application.LoadLevel (arenaSelected + 1);
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [4], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							AudioController.Instance.CallSFXSound("Button");
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
							pl2Choosing = true;
							pl1Choosing = false;
						}
						GUI.DrawTexture (new Rect (Screen.width * 0.438f, Screen.height * 0.01f, Screen.width * 0.123f, Screen.height * 0.21f), characterFrames [1], ScaleMode.StretchToFill);
					}
				}else if (((Input.mousePosition.x/Screen.width)+(((Input.mousePosition.y/Screen.height)-0.81f)*0.33f))> 0.56f &&
					((Input.mousePosition.x/Screen.width)+((Input.mousePosition.y/Screen.height)-0.81f)*0.33f)< 0.66f ) {
					if (!pl1Choosing && !pl2Choosing) {

					}
					else if (!pl1Choosing && pl2Choosing) {
						if (Input.GetMouseButtonDown (0)) {
							AudioController.Instance.CallSFXSound("Button");
							ArenaController.Instance.Player2 = CharacterEnum.John;
						//	Arena.ArenaManagement.Instance.PlayBackgroundMusic (arenaSelected);
							if (arenaSelected == 0) {
								AudioController.Instance.ChangeBackgroundMusic ("The Ring");
							} else {
								AudioController.Instance.ChangeBackgroundMusic ("The Docs");
							}
							pl2Choosing = false;

						}
						name2 = "John";
						GameObject.FindGameObjectWithTag ("C2").GetComponent<MeshRenderer>().enabled = false;
						GameObject.FindGameObjectWithTag ("J2").GetComponent<MeshRenderer>().enabled = true;
						GUI.DrawTexture (new Rect (Screen.width * 0.508f, Screen.height * 0.01f, Screen.width * 0.159f, Screen.height * 0.21f), characterFrames [5], ScaleMode.StretchToFill);
					} else {
						if (Input.GetMouseButtonUp (0)) {
							AudioController.Instance.CallSFXSound("Button");
							ArenaController.Instance.Player1 = CharacterEnum.John;
							pl1Choosing = false;
							pl2Choosing = true;
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
