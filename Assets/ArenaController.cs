using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Arena;

public class ArenaController : MonoBehaviour {
    public static ArenaController Instance;
    public CharacterEnum Player1;
    public CharacterEnum Player2;
    public GameObject Cena;
    public GameObject John;

    public List<GameObject> PlayerObjects;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
		Player1 = CharacterEnum.John;
		Player2 = CharacterEnum.Cena;
    }
    
    /// <summary>
    /// Player 1 and 2 are called
    /// </summary>
    public void ChoosePlayer()
    {
        ClearPlayerObjectList();
		InsertPlayers(Player1);
        InsertPlayers(Player2);
    }

    /// <summary>
    /// Insert the chosen Character to the main PlayersObjects
    /// </summary>
    /// <param name="targetCharacter">TargetCharacter</param>
    public void InsertPlayers(CharacterEnum targetCharacter)
    {
        switch (targetCharacter)
        {
            case CharacterEnum.Cena:
                PlayerObjects.Add(Cena);
            break;
            case CharacterEnum.John:
                PlayerObjects.Add(John);
            break;
        }
    }
    /// <summary>
    /// Clears the playerobject list
    /// </summary>
    public void ClearPlayerObjectList()
    {
        PlayerObjects.Clear();
    }
}
