using UnityEngine;
using System.Collections;

public class MenuItemScript : MonoBehaviour {

	public int index;
	public Texture image;
	public bool backButton = false;
	[SerializeField] bool placed = false;
	public bool options = false;
	[SerializeField] Vector3 loc;
	[SerializeField] Vector3 backButtonLoc = new Vector3(5,3.7f,0);
	[SerializeField] Vector3 offset = new Vector3(0,-2,0);
	[SerializeField] Vector3 drawScale;
	[SerializeField] public float insertTime;

	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex",image);
		if (options) {
			loc = new Vector3 (-4.2f, -3, 0);
			drawScale = new Vector3 (-0.6f, 0.12f, 0.12f);
		} else {
			loc = new Vector3 (0, 3, 0);
			drawScale = new Vector3 (-1, 0.2f, -0.2f);
		}
	}

	public void checkPlacement(){
		if (options) {
			loc = new Vector3 (-4.2f, -3, 0);
			drawScale = new Vector3 (-0.6f, 0.12f, 0.12f);
		} else {
			loc = new Vector3 (0, -3, 0);
			drawScale = new Vector3 (-1, 0.2f, -0.2f);
		}
	}

	void Update(){
		if (!placed) {
			if (!backButton) {
				transform.position = Vector3.LerpUnclamped (transform.position, loc + (offset * index), insertTime);
				transform.localScale = drawScale;
				if (transform.position == loc + (offset * index)) {
					placed = true;
				}
			} else {
				transform.position = Vector3.LerpUnclamped (transform.position, backButtonLoc, insertTime);
				transform.localScale = new Vector3 (-0.3f, 0.15f, 0.15f);
				if (transform.position == backButtonLoc - (offset * index)) {
					placed = true;
				}
			}
		}
	}
}
