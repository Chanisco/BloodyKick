using UnityEngine;
using System.Collections;

public class WinLoseScreen : MonoBehaviour {
    private Healthbar healthbar;
    void Start()
    {
        healthbar = GetComponent<Healthbar>();
    }
    /// <summary>
    /// After the game is finished the loser gets picked and the manager start a new game
    /// </summary>
    /// <param name="loser"></param>
	public void EndGame(int loser){
		if (loser == -1) {
		} else if (loser == 0) {
			if (healthbar.pl2won == false) {
                healthbar.pl2won = true;
			} else {
                healthbar.pl2wonTwice = true;
			}
		} else if (loser == 1) {
			if (healthbar.pl1won == false) {
                healthbar.pl1won = true;
			} else {
                healthbar.pl1wonTwice = true;
			}
		}
	}
}
