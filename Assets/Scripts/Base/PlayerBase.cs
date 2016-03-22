using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ControllList;
using Arena;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] public List<FoundObject> Intuition = new List<FoundObject>();
    [SerializeField] public float lifePoints = 100;
    [SerializeField] public PlayerCommands playerCommands;
	[SerializeField] GameObject particleHigh;
	[SerializeField] GameObject particleLow;
    private Vector3 originalSize;
    public bool topState;
	[SerializeField] public bool gameRunning = true;
    private bool blocking;
    private int knockbackTimer;

    public CharacterAnimation animator;
    public PositionAgainstPlayer playerDirection;

    public enum PositionAgainstPlayer
    {
        Undefined,
        LeftOpponent,
        RightOpponent
    }

    private float speed = 0.1f;

    private bool OnPlatform = false;

    [SerializeField] private Rigidbody2D ownRigidbody;
    [SerializeField] public Transform opponent;

    void Awake()
    {
        originalSize = transform.localScale;
    }

    public void FindObject(FoundObject target)
    {
        if (Intuition.Count == 0)
        {
            Intuition.Add(target);
        }
        else
        {
            for (int i = 0; i < Intuition.Count; i++)
            {
                if (Intuition[i].objectType == target.objectType)
                {
                    break;
                }

                if (i == Intuition.Count)
                {
                    Intuition.Add(target);
                }
            }
        }
    }

    public void LoseObject(FoundObject target)
    {
        for (int i = 0;i < Intuition.Count;i++)
        {
            if(Intuition[i].objectName == target.objectName)
            {
                Intuition.RemoveAt(i);
            }

        }
    }
    

    //Commands
    public virtual void BasicMovement()
    {
		if (gameRunning == true)
        {
            if (blocking == false)
            {
                if (Input.GetKey(playerCommands.left))
                {
                    if (animator.currentAnimation == CharacterAnimationsStates.Walk && playerDirection == PositionAgainstPlayer.RightOpponent)
                    {
                        StartCoroutine(KnockBack(PositionAgainstPlayer.RightOpponent, 3));
                        animator.PlayAnimation("Dodge");

                    }
                    else
                    {
                        animator.TurnAnimationOn("Movement");
                        transform.Translate(-1 * speed, 0, 0);
                        if (opponent == null)
                        {
                            transform.localScale = new Vector2(-originalSize.x, originalSize.y);
                        }
                    }
                }
                else if (Input.GetKey(playerCommands.right))
                {
                    animator.TurnAnimationOn("Movement");
                    transform.Translate(1 * speed, 0, 0);
                    if (opponent == null)
                    {
                        transform.localScale = new Vector2(originalSize.x, originalSize.y);
                    }
                }
                else
                {
                    animator.TurnAnimationOff("Movement");
                }
            }

            
           if (Input.GetKeyDown(playerCommands.up))
            {
                topState = true;
            }

            if (Input.GetKeyUp(playerCommands.up))
            {
                topState = false;

            }

            if (Input.GetKey(playerCommands.down))
            {
                block(true);
            }

            if (Input.GetKeyUp(playerCommands.down))
            {
                block(false);
            }
        }
    }

    public virtual void LookAtOpponent()
    {
        
        if (opponent != null)
        {
            Vector2 t = this.transform.position;
            Vector2 o = this.opponent.position;
            if (t.x > o.x)
            {
                playerDirection = PositionAgainstPlayer.LeftOpponent;
                transform.localScale = new Vector2(-originalSize.x, originalSize.y);
            }
            else
            {
                playerDirection = PositionAgainstPlayer.RightOpponent;
                transform.localScale = new Vector2(originalSize.x, originalSize.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Platform")
        {
            OnPlatform = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (blocking == false)
        {
            if (col.transform.tag == "Damage" && gameRunning)
            {
				if (col.GetComponent<Hitbox> ().hitArea == HitPosition.BOT) {
					if (transform.localScale.x == 0.5f) {
						Debug.Log ("Bot0.5" + col.GetComponentInParent<PlayerBase>().name);
						particleLow.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 90));
					} else if (transform.localScale.x == -0.5f) {
						Debug.Log ("Bot-0.5"+col.GetComponentInParent<PlayerBase>().name);
						particleLow.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 90));
					}
					particleLow.GetComponent<ParticleSystem>().Emit (30 + (int)((100-lifePoints)/5));
				} else if (col.GetComponent<Hitbox> ().hitArea == HitPosition.TOP) {
					if (transform.localScale.x == 0.5f) {
						Debug.Log ("High0.5"+col.gameObject.transform.name);
						particleHigh.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 90));
					} else if (transform.localScale.x == -0.5f) {
						Debug.Log ("High-0.5"+col.gameObject.transform.name);
						particleHigh.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 90));
					}
					particleHigh.GetComponent<ParticleSystem>().Emit (30 + (int)((100-lifePoints)/5));
				}
                CameraManagement.Instance.shakeDuration = 0.04f;
                StartCoroutine(KnockBack(playerDirection,5));
                lifePoints = lifePoints - col.gameObject.GetComponent<Hitbox>().damage;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Platform")
        {
            OnPlatform = false;
        }
    }

    public bool Alive()
    {
        if(lifePoints > 0)
        {
            return true;
        }
        else
        {
            animator.LockAnimationWithAnimation("Death");
            return false;
        }
    }
    
    private void block(bool activation)
    {
        if(activation == true)
        {
            blocking = true;
            if (topState == true)
            {
                animator.TurnAnimationOn("HighBlock");
            }
            else
            {
                animator.TurnAnimationOn("LowBlock");

            }
        }
        else
        {
            blocking = false;
            animator.ConditionsOff();
        }
    }
    public string attack()
    {
        if (animator.currentAnimation == CharacterAnimationsStates.Idle)
        {
            if (gameRunning)
            {
                if (topState)
                {
                    if (Input.GetKeyDown(playerCommands.punchAttack))
                    {
                        return ControllList.ControllsLibrary.HIGHPUNCHLEFT;
                    }
                    if (Input.GetKeyDown(playerCommands.kickAttack))
                    {
                        return ControllList.ControllsLibrary.HIGHKICK;
                    }
                    return "Idle";
                }
                else
                {
                    if (Input.GetKeyDown(playerCommands.punchAttack))
                    {
                        return ControllList.ControllsLibrary.LOWPUNCHLEFT;
                    }
                    if (Input.GetKeyDown(playerCommands.kickAttack))
                    {
                        return ControllList.ControllsLibrary.LOWKICK;
                    }
                    return "Idle";
                }
            }
            else
            {
                return "Idle";
            }
        }
        else
        {
            return "Idle";
        }
    }

    public IEnumerator KnockBack(PositionAgainstPlayer targetPosition,int timesOfForce)
    {
        timesOfForce--;
        if (timesOfForce > 0)
        {
            if (targetPosition == PositionAgainstPlayer.RightOpponent)
            {
                transform.Translate(-0.2f, 0, 0);
                yield return new WaitForEndOfFrame();
                StartCoroutine(KnockBack(targetPosition, timesOfForce));
            }
            else if (targetPosition == PositionAgainstPlayer.LeftOpponent)
            {
                transform.Translate(0.2f, 0, 0);
                yield return new WaitForEndOfFrame();
                StartCoroutine(KnockBack(targetPosition, timesOfForce));
            }
        }
        else
        {
            yield break;
        }
    }

    
}

