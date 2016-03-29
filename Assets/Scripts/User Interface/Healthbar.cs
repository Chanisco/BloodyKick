using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Arena;

public class Healthbar : MonoBehaviour {
	
	public List<float> playerHealth;
	public int time = 10;
	[SerializeField] WinLoseScreen winLose;
	[SerializeField] public ArenaManagement arena;
	[SerializeField] StartScreenAnimator screensAnimator;
	[SerializeField] Texture healthBarFront;
	[SerializeField] Texture healthBarRed;
	[SerializeField] Texture heart;
	[SerializeField] Texture healthBarBack;
	[SerializeField] Texture roundWon;
	[SerializeField] Texture roundNotWon;
	[SerializeField] Texture timeDisplay;
	[SerializeField] Texture nameHolder;
	[SerializeField] Texture pl1winsTop;
	[SerializeField] Texture pl1winsBottom;
	[SerializeField] Texture pl2winsTop;
	[SerializeField] Texture pl2winsBottom;
	[SerializeField] List<float> showHealth;
	[SerializeField] float dropSpeed;
	[SerializeField] Font karate;
	[SerializeField] Font shanghai;
	[SerializeField] public bool pl1won = false;
	[SerializeField] public bool pl2won = false;
	[SerializeField] public bool pl1wonTwice = false;
	[SerializeField] public bool pl2wonTwice = false;
	[SerializeField] private bool end = false;
	[SerializeField] private bool finalRound = false;
	[SerializeField] float animationSpeed;
	[SerializeField] public int startTime;
	string add0string = "";
	GUIStyle style;
	float heartIndex = 0;
	float counter = 0;
	Vector2 heartOffset = Vector2.zero;

	void Awake(){
		style = new GUIStyle ();
		style.normal.textColor = Color.black;
		style.fontSize = (60 * Screen.width)/1920;
		winLose = GetComponent<WinLoseScreen> ();
		startTime = (int)Time.time +4;
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
			arena.players [0].playerInformation.animator.TurnAnimationOn ("Idle");
			arena.players [1].playerInformation.animator.TurnAnimationOn ("Idle");
			arena.gameRunning = false;
			end = true;
			//Debug.Log ("Check"+player);
			if (playerNumber == 1) {
				if (!pl1won) {
					if (pl2won) {
						arena.finalRound = true;
					}
					pl1won = true;
					StartCoroutine (NewRoundDelay());
				}
				else{
					pl1wonTwice = true;
					Debug.Log ("PL1 WINS!");
					StartCoroutine (EndGame ());
				}
			} else if (playerNumber == 0) {
				if (!pl2won) {
					if (pl1won) {
						arena.finalRound = true;
					}
					pl2won = true;
					StartCoroutine (NewRoundDelay());
				}
				else{
					pl2wonTwice = true;
					Debug.Log ("PL2 WINS!");
					StartCoroutine (EndGame ());
				}
			}
		}
	}

	IEnumerator NewRoundDelay(){
		yield return new WaitForSeconds (2);
		NewRound ();
		arena.NewRound();
	}

	IEnumerator EndGame(){
		screensAnimator.animateSpeed = 0.5f;
		if (pl1wonTwice) {
			screensAnimator.AnimateScreen (pl1winsTop, pl1winsBottom);
		} else {
			screensAnimator.AnimateScreen (pl2winsTop, pl2winsBottom);
		}
		yield return new WaitForSeconds (5);
		Application.LoadLevel (0);
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
		GUI.DrawTexture (new Rect (Screen.width * 0.45f, Screen.height * 0.0418f, Screen.width * 0.1f, Screen.width * 0.054348f), timeDisplay, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (Screen.width * 0.08f, Screen.height * 0.1195f, Screen.width * 0.84f, Screen.height*0.055f), nameHolder, ScaleMode.StretchToFill);
		style.font = karate;
		style.alignment = TextAnchor.MiddleLeft;
		GUI.TextField (new Rect (Screen.width * 0.09f, Screen.height * 0.122f, Screen.width * 0.2f, Screen.height * 0.055f), arena.players [0].playerInformation.name, style);
		style.alignment = TextAnchor.MiddleRight;
		GUI.TextField (new Rect (Screen.width * 0.7f, Screen.height * 0.122f, Screen.width * 0.2f, Screen.height * 0.055f), arena.players [1].playerInformation.name, style);

		GUI.DrawTexture (new Rect (Screen.width * 0.444f, Screen.height * 0.052f, Screen.width * -0.4f, Screen.height * 0.068f), healthBarBack, ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (Screen.width * 0.556f, Screen.height * 0.052f, Screen.width * 0.4f, Screen.height * 0.068f), healthBarBack, ScaleMode.ScaleToFit);

		GUI.DrawTexture (new Rect (Screen.width * 0.045f, Screen.height * 0.052f, Screen.width * (0.004f*showHealth[0]), Screen.height * 0.068f), healthBarRed, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (Screen.width * 0.955f, Screen.height * 0.052f, Screen.width * -(0.004f*showHealth[1]), Screen.height * 0.068f), healthBarRed, ScaleMode.StretchToFill);

		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.017f, Screen.height * 0.041f, Screen.width * 0.46f, Screen.height * 0.092f), healthBarFront, new Rect(0, 0, 1, 1));
		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.519f, Screen.height * 0.041f, Screen.width * 0.46f, Screen.height * 0.092f), healthBarFront, new Rect(1, 0, -1, 1));

		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.0f, Screen.height * -0.085f, Screen.width * 0.12f, Screen.height * 0.28f), heart, new Rect(heartOffset.x*0.1666667f, heartOffset.y*0.2f, 0.1666667f, 0.2f));
		GUI.DrawTextureWithTexCoords (new Rect (Screen.width * 0.88f, Screen.height * -0.085f, Screen.width * 0.12f, Screen.height * 0.28f), heart, new Rect((heartOffset.x+1)*0.1666667f, heartOffset.y*0.2f, -0.1666667f, 0.2f));
		style.font = shanghai;
		style.alignment = TextAnchor.MiddleCenter;
		GUI.TextField (new Rect (Screen.width * 0.4725f, Screen.height * 0.065f, Screen.width * 0.05f, Screen.height * 0.05f), ""+add0string+time, style);

		if (pl1won) {
			GUI.DrawTexture (new Rect (Screen.width * 0.4f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundWon, ScaleMode.ScaleToFit);
			if (pl1wonTwice) {
				GUI.DrawTexture (new Rect (Screen.width * 0.35f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundWon, ScaleMode.ScaleToFit);
			} else {
				GUI.DrawTexture (new Rect (Screen.width * 0.35f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundNotWon, ScaleMode.ScaleToFit);
			}
		} else {
			GUI.DrawTexture (new Rect (Screen.width * 0.4f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundNotWon, ScaleMode.ScaleToFit);
			GUI.DrawTexture (new Rect (Screen.width * 0.35f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundNotWon, ScaleMode.ScaleToFit);
		}

		if (pl2won) {
			GUI.DrawTexture (new Rect (Screen.width * 0.555f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundWon, ScaleMode.ScaleToFit);
			if (pl2wonTwice) {
				GUI.DrawTexture (new Rect (Screen.width * 0.605f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundWon, ScaleMode.ScaleToFit);
			} else {
				GUI.DrawTexture (new Rect (Screen.width * 0.605f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundNotWon, ScaleMode.ScaleToFit);
			}
		} else {
			GUI.DrawTexture (new Rect (Screen.width * 0.555f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundNotWon, ScaleMode.ScaleToFit);
			GUI.DrawTexture (new Rect (Screen.width * 0.605f, Screen.height * 0.13f, Screen.width * 0.04f, Screen.width * 0.0226f), roundNotWon, ScaleMode.ScaleToFit);
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

    public void CheckWinner()
    {
        Debug.Log(playerHealth[0] + " " + playerHealth[1]);
        if(playerHealth[0] == playerHealth[1])
        {
            StartCoroutine(NewRoundDelay());
            add0string = "";
        }
        else if(playerHealth[0] > playerHealth[1])
        {
            AudioController.Instance.PlaySound(AnnouncerSounds.WINNER);
            if (pl1won == false)
            {
                pl1won = true;
            }
            else
            {
                pl1wonTwice = true;
            }
        }
        else
        {
            AudioController.Instance.PlaySound(AnnouncerSounds.WINNER);
            if (pl2won == false)
            {
                pl2won = true;
            }
            else
            {
                pl2wonTwice = true;
            }
        }
    }
	void Update(){
		AnimateHeart ();
		if (!end) {
			if (time > 0)
            {
                time = 99 - (int)Time.time + startTime;
			} else {
                AudioController.Instance.PlaySound(AnnouncerSounds.TIMEUP);
                winLose.EndGame (-1);
				arena.players [0].playerInformation.gameRunning = false;
				arena.players [1].playerInformation.gameRunning = false;
                CheckWinner();
                arena.players [0].playerInformation.animator.TurnAnimationOn ("Win");
				arena.players [1].playerInformation.animator.TurnAnimationOn ("Win");
			}
			if (time < 10) {
				add0string = "0";
                switch (time)
                {
                    case 10:
                        AudioController.Instance.PlaySound(AnnouncerSounds.TEN);
                        break;
                    case 9:
                        AudioController.Instance.PlaySound(AnnouncerSounds.NINE);
                        break;
                    case 8:
                        AudioController.Instance.PlaySound(AnnouncerSounds.EIGHT);
                        break;
                    case 7:
                        AudioController.Instance.PlaySound(AnnouncerSounds.SEVEN);
                        break;
                    case 6:
                        AudioController.Instance.PlaySound(AnnouncerSounds.SIX);
                        break;
                    case 5:
                        AudioController.Instance.PlaySound(AnnouncerSounds.FIVE);
                        break;
                    case 4:
                        AudioController.Instance.PlaySound(AnnouncerSounds.FOUR);
                        break;
                    case 3:
                        AudioController.Instance.PlaySound(AnnouncerSounds.THREE);
                        break;
                    case 2:
                        AudioController.Instance.PlaySound(AnnouncerSounds.TWO);
                        break;
                    case 1:
                        AudioController.Instance.PlaySound(AnnouncerSounds.ONE);
                        break;
                }
            }
		}
		showHealth [0] = Mathf.SmoothStep (showHealth [0], playerHealth [0], dropSpeed);
		showHealth [1] = Mathf.SmoothStep (showHealth [1], playerHealth [1], dropSpeed);
	}
}
