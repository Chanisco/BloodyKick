using UnityEngine;
using System.Collections;

public class Esc_Menu : MonoBehaviour {

	[SerializeField] bool opened = false;
	[SerializeField] Healthbar healthBar;
	[SerializeField] Arena.ArenaManagement arena;
	[SerializeField] private float openTime;
	[SerializeField] private float closeTime;

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
}
