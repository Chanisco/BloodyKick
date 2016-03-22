using UnityEngine;
using System.Collections;

public class WinLoseScreen : MonoBehaviour {

    /// <summary>
    /// After the game is finished the loser gets picked and the manager start a new game
    /// </summary>
    /// <param name="loser"></param>
	public void EndGame(int loser){
		if (loser == -1) {
		} else if (loser == 0) {
			if (GetComponent<Healthbar> ().pl2won == false) {
				GetComponent<Healthbar> ().pl2won = true;
			} else {
				GetComponent<Healthbar> ().pl2wonTwice = true;
			}
		} else if (loser == 1) {
			if (GetComponent<Healthbar> ().pl1won == false) {
				GetComponent<Healthbar> ().pl1won = true;
			} else {
				GetComponent<Healthbar> ().pl1wonTwice = true;
			}
		}
	}
}
