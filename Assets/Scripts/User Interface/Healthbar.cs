using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Healthbar : MonoBehaviour {
	
	public List<float> playerHealth;
	[SerializeField] WinLoseScreen winLose;
	[SerializeField] public Arena.ArenaManagement arena;
	[SerializeField] Texture healthBarFront;
	[SerializeField] Texture healthBarRed;
	[SerializeField] Texture heart;
	/*[SerializeField] public Texture characterIcon0;
	[SerializeField] public Texture characterIcon1;*/
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
	[SerializeField] public bool pl1wonTwice = false;
	[SerializeField] public bool pl2wonTwice = false;
	[SerializeField] private bool end = false;
	[SerializeField] private bool finalRound = false;
	[SerializeField] float animationSpeed;
	[SerializeField] public int startTime;
	[SerializePrivateVariables] float heartIndex = 0;
	[SerializePrivateVariables] float counter = 0;
	[SerializePrivateVariables] Vector2 heartOffset = Vector2.zero;

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
			end = true;
			//yield return new WaitForSeconds (1);
			if (playerNumber == 0 && pl2won) {
				Debug.Log ("PL1 WINS!");
			} else if (playerNumber == 1 && pl1won) {
				Debug.Log ("PL2 WINS!");
			} else {
				NewRound ();
				arena.NewRound();
			}
			winLose.EndGame (playerNumber);
		}
	}

	public void NewRound(){
		Init (2);
		startTime = (int)Time.time;
		end = false;
	}

	public void PauseGame(bool state){
		end = state;
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect (Screen.width * 0.445f, Screen.height * 0.052f, Screen.width * -0.4f, Screen.height * 0.068f), healthBarBack, ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (Screen.width * 0.555f, Screen.height * 0.052f, Screen.width * 0.4f, Screen.height * 0.068f), healthBarBack, ScaleMode.ScaleToFit);

		GUI.DrawTexture (new Rect (Screen.width * 0.045f, Screen.height * 0.052f, Screen.width * (0.004f*showHealth[0]), Screen.height * 0.068f), healthBarRed, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (Screen.width * 0.955f, Screen.height * 0.052f, Screen.width * -(0.004f*showHealth[1]), Screen.height * 0.068f), healthBarRed, ScaleMode.StretchToFill);

		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.017f, Screen.height * 0.041f, Screen.width * 0.46f, Screen.height * 0.092f), healthBarFront, new Rect(0, 0, 1, 1));
		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.519f, Screen.height * 0.041f, Screen.width * 0.46f, Screen.height * 0.092f), healthBarFront, new Rect(1, 0, -1, 1));

		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.0f, Screen.height * -0.085f, Screen.width * 0.12f, Screen.height * 0.28f), heart, new Rect(heartOffset.x*0.1666667f, heartOffset.y*0.2f, 0.1666667f, 0.2f));
		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.88f, Screen.height * -0.085f, Screen.width * 0.12f, Screen.height * 0.28f), heart, new Rect((heartOffset.x+1)*0.1666667f, heartOffset.y*0.2f, -0.1666667f, 0.2f));

		GUI.TextField (new Rect (Screen.width * 0.475f, Screen.height * 0.05f, Screen.width * 0.05f, Screen.height * 0.05f), ""+add0string+time, style);
		if (pl1won) {
			GUI.DrawTexture (new Rect (Screen.width*0.05f,Screen.width*-0.078f, Screen.width * 0.47f,Screen.height * 0.47f), roundWon, ScaleMode.ScaleToFit);
			if (pl1wonTwice) {
				GUI.DrawTexture (new Rect (Screen.width*0.00f,Screen.width*-0.078f, Screen.width * 0.47f,Screen.height * 0.47f), roundWon, ScaleMode.ScaleToFit);
			}
		}
		if (pl2won) {
			GUI.DrawTexture (new Rect (Screen.width*0.165f, Screen.width*-0.078f, Screen.width * 0.47f, Screen.height * 0.47f), roundWon, ScaleMode.ScaleToFit);
			if (pl2wonTwice) {
				GUI.DrawTexture (new Rect (Screen.width*0.215f, Screen.width*-0.078f, Screen.width * 0.47f, Screen.height * 0.47f), roundWon, ScaleMode.ScaleToFit);
			}
		}
	}

	void AnimateHeart(){
		counter++;
		if (counter > animationSpeed) {
			heartIndex++;
			float x = heartIndex % 6;
			float y = Mathf.Floor (heartIndex / 6);
			y %= 4;
			heartOffset = new Vector2 (x, y);
			counter = 0;
		}
	}

	void Update(){
		AnimateHeart ();
		if (!end) {
			if (time > 0) {
				time = 99 - (int)Time.time + startTime;
			} else {
				winLose.EndGame (-1);
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
