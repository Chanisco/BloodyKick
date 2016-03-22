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
	[SerializeField] float scale;

	void Start(){
		scale = (scale*Screen.width)/1920;
	}

	void OnGUI(){
		if (animating) {
			distance = EndPos.x - usingPos.x;
			if (distance > startDistance * 0.599f) {
				speed = 0.05f * (startDistance*2);
			} else if (distance > startDistance * 0.48f) {
				speed = 0.0003f * (startDistance*2);
			} else if (distance > 0) {
				speed = 0.05f * (startDistance*2);
			} else {
				animating = false;
			}
			usingPos.x += speed;
			GUI.DrawTexture (new Rect (Screen.width * (usingPos.x-usingScale.x/2), Screen.height * usingPos.y, Screen.width * usingScale.x, Screen.height * usingScale.y), top, ScaleMode.ScaleToFit);
			GUI.DrawTexture (new Rect (Screen.width * (1 - (usingPos.x+usingScale.x/2)), Screen.height * (usingPos.y+usingScale.y-0.002f), Screen.width * usingScale.x, Screen.height * usingScale.y), bottom, ScaleMode.ScaleToFit);
		}
	}

	public void AnimateScreen(Texture topPart, Texture bottomPart){
		animating = true;
		top = topPart;
		bottom = bottomPart;
		usingScale = new Vector2(((float)topPart.width/(float)Screen.width)*scale,((float)topPart.height/(float)Screen.height)*scale);
		startPos = new Vector2 (-((float)topPart.width*scale/(float)Screen.width*scale)+0.04f, 0.5f - ((float)topPart.height*scale/(float)Screen.height*scale));
		EndPos = new Vector2 (1 - startPos.x-0.04f, startPos.y);
		startDistance = EndPos.x - startPos.x;
		usingPos = startPos;
	}
}
