using UnityEngine;
using System.Collections;

public class MenuItemScript : MonoBehaviour {

	public int index;
	public Texture image;
	public bool backButton = false;
	public float insertTime;
	public bool options = false;
	public bool placed = false;
	[SerializePrivateVariables] Vector3 loc;
	[SerializePrivateVariables] Vector3 backButtonLoc;
	[SerializePrivateVariables] Vector3 offset;
	[SerializePrivateVariables] Vector3 drawScale;

	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex",image);
		if (options) {
			loc = new Vector3 (5.96f, 2.61f, 35.43f);
			offset = new Vector3 (0, -2.1f, 0);
			drawScale = new Vector3 (-0.455f, -0.12f, 0.2f);
		} else {
			loc = new Vector3 (0, 2.2f, -1.77f);
			offset = new Vector3 (0, -1.3f, 0);
			drawScale = new Vector3 (-0.273f, 0.2f, -0.12f);
		}
		backButtonLoc = new Vector3 (12.4f, -4.1f, 30);
	}

	void Update(){
		///<summary>
		/// slides the menuitem into place
		/// </summary>
		if (!placed) {
			if (!backButton) {
				transform.position = Vector3.LerpUnclamped (transform.position, loc + (offset * index), insertTime);
				transform.localScale = drawScale;
				if (transform.position == loc + (offset * index)) {
					placed = true;
				}
			} else {
				transform.position = Vector3.LerpUnclamped (transform.position, backButtonLoc, insertTime);
				transform.localScale = new Vector3 (-0.3f, -0.15f, 0.15f);
				if (transform.position == backButtonLoc - (offset * index)) {
					placed = true;
				}
			}
		}
	}
}
