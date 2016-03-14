using UnityEngine;
using System.Collections;

public class InsertCoin : MonoBehaviour {

	[SerializeField] float offsetTime = 0;
	[SerializeField] float timer = 0;
	[SerializeField] bool on = false;
	public GameObject nextScreen;

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
		if (Input.anyKeyDown) {
			Destroy (GameObject.FindGameObjectWithTag ("Title"));
			Destroy (gameObject);
			Instantiate (nextScreen);
		}
	}
}
