using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Healthbar : MonoBehaviour {
	
	public List<float> playerHealth;
	[SerializeField] WinLoseScreen winLose;
	[SerializeField] public Arena.ArenaManagement arena;
	[SerializeField] Texture healthBarFrontLeft;
	[SerializeField] Texture healthBarFrontRight;
	[SerializeField] public Texture characterIcon0;
	[SerializeField] public Texture characterIcon1;
	[SerializeField] Texture healthBarBack;
	[SerializeField] Texture roundWon;
	[SerializeField] List<float> showHealth;
	[SerializeField] float dropSpeed;
	[SerializeField] int time = 10;
	[SerializeField] GUIStyle style;
	[SerializeField] Font font;
	[SerializeField] string add0string = "";
	[SerializeField] public bool pl1won = false;
	[SerializeField] public bool pl2won = false;
	[SerializeField] private bool end = false;
	[SerializePrivateVariables] int startTime;

	void Awake(){
		style = new GUIStyle ();
		style.font = font;
		style.normal.textColor = Color.red;
		style.fontSize = (100 * Screen.width)/1920;
		winLose = GetComponent<WinLoseScreen> ();
		startTime = (int)Time.time;
	}

	public void Init(int playerAmount){
		for (float i=0; i<playerAmount; i++) {
			playerHealth.Add(100);
			showHealth.Add(100);
		}
	}

	public void ChangeHealth(int playerNumber, float health){
		playerHealth [playerNumber] = health;
		if (playerHealth [playerNumber] <= 0) {
			playerHealth [playerNumber] = 0;
			arena.Players [0].playerInformation.animator.TurnAnimationOn ("Idle");
			arena.Players [1].playerInformation.animator.TurnAnimationOn ("Idle");
			winLose.EndGame (playerNumber);
			end = true;
		}
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect (Screen.width * 0.455f, -Screen.height * 0.047f, Screen.width * -(0.00325f*showHealth[0]), Screen.height * 0.25f), healthBarBack, ScaleMode.ScaleAndCrop);
		GUI.DrawTexture (new Rect (Screen.width * 0.545f, -Screen.height * 0.047f, Screen.width * (0.00325f*showHealth[1]), Screen.height * 0.25f), healthBarBack, ScaleMode.ScaleAndCrop);

		GUI.DrawTexture (new Rect (Screen.width * 0.014f, Screen.height * 0.013f, Screen.width * 0.1f, Screen.width * 0.1f), characterIcon0, ScaleMode.ScaleToFit);
		GUI.DrawTextureWithTexCoords(new Rect(Screen.width * 0.985f, Screen.height * 0.013f, Screen.width * -0.1f, Screen.width * 0.1f), characterIcon1, new Rect(0, 0, 1, 1));

		GUI.DrawTextureWithTexCoords (new Rect (0,Screen.width*-0.006f, Screen.width * 0.47f,Screen.width * 0.1234f), healthBarFrontLeft, new Rect(0, 0, 1, 1));
		GUI.DrawTextureWithTexCoords (new Rect (Screen.width*0.53f, Screen.width*-0.006f, Screen.width * 0.47f, Screen.width * 0.1234f), healthBarFrontLeft, new Rect(0, 0, -1, 1));

		GUI.TextField (new Rect (Screen.width * 0.475f, Screen.height * 0.05f, Screen.width * 0.06f, Screen.height * 0.05f), ""+add0string+time, style);
		if (pl1won) {
			GUI.DrawTexture (new Rect (Screen.width*0.05f,Screen.width*-0.078f, Screen.width * 0.47f,Screen.height * 0.47f), roundWon, ScaleMode.ScaleToFit);
		}
		if (pl2won) {
			GUI.DrawTexture (new Rect (Screen.width*0.165f, Screen.width*-0.078f, Screen.width * 0.47f, Screen.height * 0.47f), roundWon, ScaleMode.ScaleToFit);
		}
	}

	void Update(){
		if (!end) {
			if (time > 0) {
				time = 99 - (int)Time.time + startTime;
			} else {
				winLose.EndGame (-1);
				end = true;
				arena.Players [0].playerInformation.gameRunning = false;
				arena.Players [1].playerInformation.gameRunning = false;
				arena.Players [0].playerInformation.animator.TurnAnimationOn ("Idle");
				arena.Players [1].playerInformation.animator.TurnAnimationOn ("Idle");
			}
			if (time < 10) {
				add0string = "0";
			}
		}
		showHealth [0] = Mathf.SmoothStep (showHealth [0], playerHealth [0], dropSpeed);
		showHealth [1] = Mathf.SmoothStep (showHealth [1], playerHealth [1], dropSpeed);
	}
}
