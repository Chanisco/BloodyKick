using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Controlls;

namespace Arena
{
    public class ArenaManagement : MonoBehaviour
    {
        public static ArenaManagement Instance;

        [SerializeField] public int amountOfPlayers;
		[SerializeField] Esc_Menu escMenu;
		[SerializeField] StartScreenAnimator screensAnimator;
		[SerializeField] Texture[] screens;
        public List<PlayerData> players = new List<PlayerData>();
        public List<GameObject> chosenCharacters = new List<GameObject>();
        [SerializeField] public Healthbar healthBar;
		[SerializeField] public bool gameRunning=true;
		[SerializeField] public bool finalRound = false;

        [SerializeField] public Vector2 borderPositions;


        void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            StartTheFight();
            healthBar.Init(2);
        }
        #region //COMMENTED
        /*public void InsertPlayer(PlayerBase targetplayer)
        {
            Players.Add(new PlayerData(Players.Count, true,targetplayer));
        }

        public void InsertNPC(PlayerBase targetplayer)
        {
            Players.Add(new PlayerData(Players.Count, false,targetplayer));
        }*/
        #endregion

        /// <summary>
        /// When the game starts you instansiate the chosen players
        /// </summary>
        public void StartTheFight()
        {
			ArenaController.Instance.ChoosePlayer ();
            chosenCharacters.Add(ArenaController.Instance.PlayerObjects[0]);
            chosenCharacters.Add(ArenaController.Instance.PlayerObjects[1]);
            InstantiatePlayer();
			StartCoroutine (CountDown());
        }

        /// <summary>
        /// Short Countdown that starts the match
        /// </summary>
        /// <returns>Start the match</returns>
		IEnumerator CountDown(){
			PauseGame (true);
			healthBar.PauseGame (true);
			int index = 0;
			foreach (Texture img in screens) {
				if(index%2 == 0) {
					screensAnimator.AnimateScreen (img, screens[index+1]);
					yield return new WaitForSeconds (1);
				}
				index++;
			}
			index = 0;
			PauseGame (false);
			healthBar.PauseGame (false);
			escMenu.active = true;
		}

      
        /// <summary>
        /// The manager that keeps track of things
        /// </summary>
        void Update()
        {
            Application.targetFrameRate = 60;
            if (gameRunning) {
				CheckOnHealth ();
			}
        }

        /// <summary>
        /// The function that keeps track of who is still alive
        /// </summary>
        void CheckOnHealth()
        {
            healthBar.ChangeHealth(0, players[0].playerInformation.lifePoints);
            healthBar.ChangeHealth(1, players[1].playerInformation.lifePoints);
			if (players [0].playerInformation.lifePoints <= 0 || players [1].playerInformation.lifePoints<=0) {
				players [0].playerInformation.gameRunning = false;
				players [1].playerInformation.gameRunning = false;
				gameRunning = false;
			}
        }
        
        /// <summary>
        /// The pop up that pauses the game
        /// </summary>
        /// <param name="state">is the game on pause?</param>
		public void PauseGame(bool state){
			gameRunning = !state;
			players [0].playerInformation.gameRunning = !state;
			players [1].playerInformation.gameRunning = !state;
		}
        /// <summary>
        /// Function that starts a new round
        /// </summary>
		public void NewRound(){
			foreach (PlayerData player in players) {
				Destroy (player.playerInformation.gameObject);
			}
			players.Clear ();
			healthBar.ChangeHealth(0, 100);
			healthBar.ChangeHealth(1, 100);
			healthBar.time = 99;
			StartTheFight ();
			healthBar.startTime = (int)Time.time+4;
		}
        /// <summary>
        /// Function that checks the player and then instansiates it
        /// </summary>
        void InstantiatePlayer()
        {
            for (int i = 0;i < chosenCharacters.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        GameObject Player1 = Instantiate(chosenCharacters[i], new Vector3(-5, -4, 0.15f), Quaternion.identity) as GameObject;
                        Player1.name = chosenCharacters[i].name;
                        Player1.transform.parent = transform;
                        PlayerBase Player1Base = Player1.GetComponent<PlayerBase>();
                        Player1Base.playerCommands = PlayerControllBase.Player1Settings();
                        players.Add(new PlayerData(i,true, Player1Base));

                    break;
				case 1:
						GameObject Player2 = Instantiate (chosenCharacters [i], new Vector3 (5, -4, 0), Quaternion.identity) as GameObject;
						Player2.name = chosenCharacters [i].name;
						Player2.transform.parent = transform;
						Player2.transform.localScale = new Vector3 (-Player2.transform.localScale.x, Player2.transform.localScale.y, Player2.transform.localScale.z);
                        PlayerBase Player2Base = Player2.GetComponent<PlayerBase>();
                        Player2Base.playerCommands = PlayerControllBase.Player2Settings();
                        players.Add(new PlayerData(i, true, Player2Base));

                        players[0].playerInformation.opponent = players[1].playerInformation.transform;
                        players[1].playerInformation.opponent = players[0].playerInformation.transform;

                        break;

                }

            }
        }

    }


    /// <summary>
    /// The Class that shows the MUSTS for all characters on the field
    /// </summary>
    [System.Serializable]
    public class PlayerData
    {

        public int playerNumber;
        public bool Controllable;
        public PlayerBase playerInformation;

        public PlayerData(int PlayerNumber,bool isControllable, PlayerBase PlayerInformation)
        {
            this.playerNumber       = PlayerNumber;
            this.Controllable       = isControllable;
            this.playerInformation  = PlayerInformation;
        }

    }
}
