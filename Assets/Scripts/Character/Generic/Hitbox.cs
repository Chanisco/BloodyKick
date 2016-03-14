using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {

    public float lifetime;
    public float damage;
    public HitPosition hitArea;

    IEnumerator TurnOff(float TimeUntilDeath)
    {
        yield return new WaitForSeconds(TimeUntilDeath);
        gameObject.SetActive(false);
    }

	void OnEnable()
    {
        StartCoroutine("TurnOff",lifetime);
    }
}

[System.Serializable]
public class HitboxElement
{
    public GameObject objectGameObject;
    public Hitbox hitboxClass;

    public HitboxElement(GameObject ObjectGameObject)
    {
        this.objectGameObject   = ObjectGameObject;
        this.hitboxClass        = ObjectGameObject.GetComponent<Hitbox>();
    }
}

public enum HitPosition
{
    TOP,
    MID,
    BOT
}