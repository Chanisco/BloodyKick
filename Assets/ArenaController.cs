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
    }

    void Start()
    {
        InsertPlayers(Player1);
        InsertPlayers(Player2);
    }
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

    public void ClearPlayerObjectList()
    {
        PlayerObjects.Clear();
    }
}
