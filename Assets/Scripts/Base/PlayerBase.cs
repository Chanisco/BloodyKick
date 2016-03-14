using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Arena;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] public List<FoundObject> Intuition = new List<FoundObject>();
    [SerializeField] public float lifePoints = 100;
    [SerializeField] public PlayerCommands playerCommands;
    private Vector3 originalSize;
	[SerializeField] public bool gameRunning = true;
    private bool blocking;

    public CharacterAnimation animator;

    enum PositionAgainstPlayer
    {
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
                    animator.TurnAnimationOn("Movement");
                    transform.Translate(-1 * speed, 0, 0);
                    if (opponent == null)
                    {
                        transform.localScale = new Vector2(-originalSize.x, originalSize.y);
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
                JumpCommand();
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
                transform.localScale = new Vector2(-originalSize.x, originalSize.y);
            }
            else
            {
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
                animator.PlayAnimation("Hit");
                CameraManagement.Instance.StartCoroutine("CameraBounce",0.1f);
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

    private void JumpCommand()
    {
        if (OnPlatform)
        {
            ownRigidbody.velocity = new Vector3(0, 5, 0);
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
            animator.TurnAnimationOn("Block");
        }
        else
        {
            blocking = false;
            animator.TurnAnimationOff("Block");
        }
    }
    public string attack()
    {
        if (animator.currentAnimation == CharacterAnimationsStates.Idle)
        {
            if (gameRunning)
            {
                if (Input.GetKeyDown(playerCommands.lightAttack))
                {
                    return "Light";
                }
                else if (Input.GetKeyDown(playerCommands.heavyAttack))
                {
                    return "Heavy";
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
        else
        {
            return "Idle";
        }
    }
}

