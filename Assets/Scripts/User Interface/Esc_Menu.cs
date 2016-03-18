using UnityEngine;
using System.Collections;

public class Esc_Menu : MonoBehaviour {

	[SerializeField] bool opened = false;
	[SerializeField] Healthbar healthBar;
	[SerializeField] Arena.ArenaManagement arena;
	[SerializeField] private float openTime;
	[SerializeField] private float closeTime;
	[SerializeField] Texture escMenu;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
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
			GUI.DrawTexture (new Rect (Screen.width * 0.4f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.4f), escMenu, ScaleMode.ScaleToFit);
			if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.1f), "Restart")) {
				Application.LoadLevel (Application.loadedLevelName);
			}
			if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.1f), "Exit")) {
				Application.LoadLevel ("Menu");
			}
		}
	}
}
