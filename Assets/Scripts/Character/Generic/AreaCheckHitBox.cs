using UnityEngine;
using System.Collections;

public class AreaCheckHitBox : MonoBehaviour {
    private string Collision;

    [SerializeField]
    private PlayerBase ownPlayer;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Item")
        {
            Transform foundItem = col.transform;
            ownPlayer.FindObject(new FoundObject(col.name, FindAbleObjectType.Item, foundItem.transform.localPosition));
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ownPlayer.opponent = col.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ownPlayer.opponent = null;
        }

        if (col.gameObject.tag == "Item")
        {
            Transform lostItem = col.transform;
            ownPlayer.LoseObject(new FoundObject(col.name, FindAbleObjectType.Item, lostItem.transform.localPosition));

        }
    }
}

[System.Serializable]
public class FoundObject{
    public string objectName;
    public FindAbleObjectType objectType;
    public Vector2 objectPos;

    public FoundObject(string ObjectName, FindAbleObjectType ObjectType,Vector2 ObjectPos)
    {
        this.objectName = ObjectName;
        this.objectType = ObjectType;
        this.objectPos  = ObjectPos;
    }
}

public enum FindAbleObjectType{
    Player,
    Item
}