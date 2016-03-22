using UnityEngine;
using System.Collections;

public class MainMenuCameraMovement : MonoBehaviour {

	public bool moving = false;
	private Camera cam;
	public int startpos;
	public Transform[] positions;
	private Vector3 position = Vector3.zero;
	private Quaternion rotation;
	private Vector3 targetLoc;
	private Quaternion targetRot;
	public MainMenu menu;
	private bool menuAdded = false;
	public CharacterAnimation characater0;
	private float random;
	private float count=0;

	void Start(){
		cam = Camera.main;
		cam.transform.position = positions [startpos].position;
		cam.transform.rotation = positions [startpos].rotation;
		random = Random.Range (10, 40);
	}

	void Update(){
		if (moving) {
			position = Vector3.Lerp (position, targetLoc, 0.06f);
			rotation = Quaternion.Lerp (rotation, targetRot, 0.06f);
			cam.transform.position = position;
			cam.transform.rotation = rotation;
			if (Vector3.Distance(cam.transform.position,targetLoc)<9  && Vector3.Distance(cam.transform.rotation.eulerAngles,targetRot.eulerAngles)<10 ||
				Vector3.Distance(cam.transform.position,targetLoc)<9  && Vector3.Distance(cam.transform.rotation.eulerAngles,targetRot.eulerAngles)>350) {
				if (!menuAdded) {
					menu.AddMenuItems ();
					menu.AddOptionItems ();
					menuAdded = true;
				}
			}
			if (Vector3.Distance(cam.transform.position,targetLoc)<0.1f  && Vector3.Distance(cam.transform.rotation.eulerAngles,targetRot.eulerAngles)<0.1f){
				moving = false;
			}
		}
		count++;
		if (count>random) {
			if (Random.Range (0, 100) > 40) {
				characater0.PlayAnimation ("Punch");
				count = 0;
				random = Random.Range (10, 100);
			} else {
				characater0.PlayAnimation ("Kick");
				count = 0;
				random = Random.Range (10, 40);
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
