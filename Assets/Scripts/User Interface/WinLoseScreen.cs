using UnityEngine;
using System.Collections;

public class WinLoseScreen : MonoBehaviour {

	//-1 = Tie Game, 0  = Pl1, 1= Pl2
	public void EndGame(int loser){
		string debug = "";
		if (loser == -1) {
			debug = "Tie game!";
		} else if (loser == 0) {
			debug = "Pl2 wins!";
			if (GetComponent<Healthbar> ().pl2won == false) {
				GetComponent<Healthbar> ().pl2won = true;
			} else {
				GetComponent<Healthbar> ().pl2wonTwice = true;
			}
		} else if (loser == 1) {
			debug = "Pl1 wins!";
			if (GetComponent<Healthbar> ().pl1won == false) {
				GetComponent<Healthbar> ().pl1won = true;
			} else {
				GetComponent<Healthbar> ().pl1wonTwice = true;
			}
		}
	//	Debug.Log (debug);
	}
}
