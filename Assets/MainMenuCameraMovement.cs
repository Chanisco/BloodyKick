using UnityEngine;
using System.Collections;

public class MainMenuCameraMovement : MonoBehaviour {

	private bool moving = false;
	private Camera cam;
	public int startpos;
	public Transform[] positions;
	private Vector3 position = Vector3.zero;
	private Quaternion rotation;
	private Vector3 targetLoc;
	private Quaternion targetRot;
	public MainMenu menu;
	private bool menuAdded = false;
	public PlayerBase characater0;
	private float random;
	private float count=0;
	//	private Vector3 startLoc;
//	private Quaternion startRot;
	//private float speed = 0.05f;

	void Start(){
		cam = Camera.main;
		cam.transform.position = positions [startpos].position;
		cam.transform.rotation = positions [startpos].rotation;
		random = Random.Range (20, 100);
	}

	void Update(){
		if (moving) {
			position = Vector3.Lerp (position, targetLoc, 0.04f);
			rotation = Quaternion.Lerp (rotation, targetRot, 0.04f);
			cam.transform.position = position;
			cam.transform.rotation = rotation;
			if (Vector3.Distance(cam.transform.position,targetLoc)<3 && !menuAdded/* && cam.transform.rotation == targetRot*/) {
				menu.AddMenuItems ();
				menuAdded = true;
			}
			if (Vector3.Distance(cam.transform.position,targetLoc)<0.1f/* && cam.transform.rotation == targetRot*/) {
				moving = false;
			}
		}
		count++;
		if (count>random) {
			if (Random.Range (0, 100) < 30) {
				characater0.animator.PlayAnimation ("Punch");
				count = 0;
				random = Random.Range (20, 100);
			} else {
				characater0.animator.PlayAnimation ("Kick");
				count = 0;
				random = Random.Range (20, 100);
			}
		}
	}

	public void MoveCam(int target){
		moving = true;
		position = cam.transform.position;
		rotation = cam.transform.rotation;
		targetLoc = positions [target].position;
		targetRot = positions [target].rotation;
	}
}
