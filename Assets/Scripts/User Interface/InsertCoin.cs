using UnityEngine;
using System.Collections;

public class InsertCoin : MonoBehaviour {

	[SerializeField] float offsetTime = 0;
	[SerializeField] float timer = 0;
	[SerializeField] bool on = false;
	public GameObject nextScreen;
	public MainMenuCameraMovement camMovement;
	private bool clicked = false;

	void Start(){
		gameObject.GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer++;
		if (timer > offsetTime) {
			on = !on;
			timer = 0;
			gameObject.GetComponent<Renderer> ().enabled = on;
		}
		if (Input.anyKeyDown && !clicked) {
			camMovement.MoveCam (1);
			camMovement.menu = Instantiate (nextScreen).GetComponent<MainMenu> ();
			camMovement.menu.AddOptionItems ();
			clicked = true;
		}
	}
}
