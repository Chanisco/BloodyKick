using UnityEngine;
using System.Collections;

public class Esc_Menu : MonoBehaviour {

	[SerializeField] bool opened = false;
	[SerializeField] Healthbar healthBar;
	[SerializeField] Arena.ArenaManagement arena;
	[SerializeField] private float openTime;
	[SerializeField] private float closeTime;
	[SerializeField] Texture escMenu;
	[SerializeField] Texture controlScreen;
	[SerializeField] Texture backButton;
	[SerializePrivateVariables] bool controls=false;
	[SerializePrivateVariables] GUIStyle style;
	public bool active = false;

	void Start(){
		style = new GUIStyle ();
	}

	void Update(){
		if (active && Input.GetKeyDown (KeyCode.Escape)) {
			if (!opened) {
				OpenMenu (true);
			} else {
				OpenMenu (false);
			}
		}
	}

	public void OpenMenu(bool state){
		if (!state) {
			closeTime = Time.time;
			healthBar.startTime += (int)(closeTime - openTime);
		} else {
			openTime = Time.time;
		}
		healthBar.PauseGame (state);
		arena.PauseGame (state);
		opened = state;
	}

	void OnGUI(){
		if (opened) {
			if (!controls) {
				GUI.DrawTexture (new Rect (Screen.width * 0.4f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.4f), escMenu, ScaleMode.ScaleToFit);
				if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.37f, Screen.width * 0.1f, Screen.height * 0.07f), "",style)) {
					OpenMenu (false);
				}
				if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.07f), "",style)) {
					Application.LoadLevel (Application.loadedLevelName);
				}
				if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.525f, Screen.width * 0.1f, Screen.height * 0.075f), "",style)) {
					controls = true;
				}
				if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.6f, Screen.width * 0.1f, Screen.height * 0.075f), "",style)) {
					Application.LoadLevel ("Menu");
				}
			} else {
				GUI.DrawTexture (new Rect (Screen.width * 0.2f, Screen.height * 0.3f, Screen.width * 0.6f, Screen.height * 0.4f), controlScreen, ScaleMode.ScaleToFit);
				GUI.DrawTexture (new Rect (Screen.width * 0.75f, Screen.height * 0.7f, Screen.width * 0.1f, Screen.height * 0.05f), backButton, ScaleMode.ScaleToFit);
				if (GUI.Button (new Rect (Screen.width * 0.75f, Screen.height * 0.7f, Screen.width * 0.1f, Screen.height * 0.05f), "",style)) {
					controls = false;
				}
			}
		}
	}
}