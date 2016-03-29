using UnityEngine;
using System.Collections;

public class StartScreenAnimator : MonoBehaviour {

	bool animating = false;
	Texture bottom;
	Texture top;
	Vector2 startPos;
	Vector2 EndPos;
	Vector2 usingPos;
	Vector2 usingScale;
	float startDistance;
	float distance;
	float speed = 0;
	public float animateSpeed=1;
	[SerializeField] float scale = 0.5f;

	void OnGUI(){
		if (animating) {
			usingPos.x += speed * animateSpeed;
			distance = EndPos.x - usingPos.x;
			Debug.Log ("" + distance);
			if (distance > 0.54f) {
				speed = 0.05f;
			} 
			if (distance > 0.48f &&
				distance <= 0.54f) {
				speed = 0.00028f;
			} 
			if (distance > 0 &&
				distance <= 0.48f) {
				speed = 0.05f;
			} 
			if (distance <= 0 ){
				animating = false;
			}
			GUI.DrawTexture (new Rect (Screen.width * (usingPos.x - (usingScale.x/2)), Screen.height * usingPos.y, Screen.width* usingScale.x, Screen.height*usingScale.y), top, ScaleMode.ScaleToFit);
			GUI.DrawTexture (new Rect (Screen.width * (1 - (usingPos.x + (usingScale.x/2))), Screen.height * (usingPos.y+usingScale.y-0.002f), Screen.width * usingScale.x, Screen.height * usingScale.y), bottom, ScaleMode.ScaleToFit);
		}
	}

	public void AnimateScreen(Texture topPart, Texture bottomPart){
		scale = 0.5f;
		animating = true;
		top = topPart;
		bottom = bottomPart;
		usingScale = new Vector2((topPart.width/(float)Screen.width)*scale,(topPart.height/(float)Screen.height)*scale);
		startPos = new Vector2 (-usingScale.x, 0.5f-usingScale.y);
		EndPos = new Vector2 (1, startPos.y);
		startDistance = EndPos.x - startPos.x;
		usingPos = startPos;
	}
}

//usingscale = %width,%height
//startpos = %-width, %0.5-usingscale.y
//endpos = %1+usingscale.x, startpos.y
//usingpos = startpos + adjustment
