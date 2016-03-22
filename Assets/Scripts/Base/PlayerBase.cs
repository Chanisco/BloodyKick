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
    private int comboOpportunity;

    private Vector3 originalSize;
    public bool topState;
	[SerializeField] public bool gameRunning = true;
    private bool blocking;
    private int knockbackTimer;

    public CharacterAnimation animator;
    public PositionAgainstPlayer playerDirection;
    private Vector2 borderPos;

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
        borderPos = ArenaManagement.Instance.borderPositions;
    }

    void Start()
    {

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
                    if (transform.localPosition.x > borderPos.x)
                    {
                        if (Input.GetKeyDown(playerCommands.left))
                        {
                            if (animator.currentAnimation != CharacterAnimationsStates.Walk)
                            {
                                animator.TurnAnimationOn("Movement");

                            }
                            else
                            {
                                if (playerDirection == PositionAgainstPlayer.RightOpponent)
                                {
                                    StartCoroutine(KnockBack(PositionAgainstPlayer.RightOpponent, 10));
                                    animator.PlayAnimation("Dodge");
                                }
                            }
                        }
                        else
                        {
                            transform.Translate(-1 * speed, 0, 0);
                            if (opponent == null)
                            {
                                transform.localScale = new Vector2(-originalSize.x, originalSize.y);
                            }
                        }
                    }
                }
                else if (Input.GetKey(playerCommands.right))
                {
                    Debug.Log(transform.localPosition.x + " " + borderPos.y);
                    if (transform.localPosition.x < borderPos.y)
                    {
                        if (Input.GetKeyDown(playerCommands.right))
                        {
                            if (animator.currentAnimation != CharacterAnimationsStates.Walk)
                            {
                                animator.TurnAnimationOn("Movement");

                            }
                            else
                            {
                                if (playerDirection == PositionAgainstPlayer.LeftOpponent)
                                {
                                    StartCoroutine(KnockBack(PositionAgainstPlayer.LeftOpponent, 10));
                                    animator.PlayAnimation("Dodge");
                                }
                            }
                        }
                        else
                        {
                            transform.Translate(1 * speed, 0, 0);
                            if (opponent == null)
                            {
                                transform.localScale = new Vector2(-originalSize.x, originalSize.y);
                            }
                        }
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

    /// <summary>
    /// Character Looks at assigned opponent
    /// It was written so if we get multiple opponents we could look at the closest
    /// (This idea got scrept)
    /// </summary>
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

    /// <summary>
    /// The Collition that makes sure that we hit the invisible ground of the battle field
    /// This was writen so that the player could jump
    /// **--IDEA SCRAPTED--**
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Platform")
        {
            OnPlatform = true;
        }

    }
    /// <summary>
    /// The Collition that makes sure that we could leave the invisible ground of the battle field
    /// This was writen so that the player could jump
    /// **--IDEA SCRAPTED--**
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Platform")
        {
            OnPlatform = false;
        }
    }

    /// <summary>
    /// Collition Trigger to show that you hit the opponent
    /// This function also calls the camera to shacke, Life to go down and the player to knock back
    /// </summary>
    /// <param name="col"></param>
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

  
    /// <summary>
    /// Script that shows if the player has health left to live.
    /// </summary>
    /// <returns>Returns boolean if he's still alive</returns>
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
    
    /// <summary>
    /// Script that blocks so he can't receive damage anymore
    /// </summary>
    /// <param name="activation">The boolean that calls this option</param>
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
        if (gameRunning)
        {
            if (animator.currentAnimation == CharacterAnimationsStates.Idle)
            {
                if (topState)
                {
                    /*if (Input.GetKeyDown(playerCommands.punchAttack))
                    {
                        return ControllList.ControllsLibrary.HIGHPUNCHLEFT;
                    }
                    if (Input.GetKeyDown(playerCommands.kickAttack))
                    {
                        return ControllList.ControllsLibrary.HIGHKICK;
                    }
                    return "Idle";*/
                    if (Input.GetKeyDown(playerCommands.punchAttack))
                    {
                        if (comboOpportunity > 0)
                        {
                            if (comboOpportunity == 1)
                            {
                                return ControllList.ControllsLibrary.HIGHPUNCHLEFT;

                            }
                            else if (comboOpportunity > 1)
                            {
                                return ControllList.ControllsLibrary.HIGHKICK;
                            }
                        }
                        else
                        {

                            return ControllList.ControllsLibrary.HIGHPUNCHLEFT;
                        }
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
                        if (comboOpportunity > 0)
                        {
                            if(comboOpportunity == 1)
                            {
                                return ControllList.ControllsLibrary.LOWPUNCHRIGHT;

                            }
                            else if(comboOpportunity > 1)
                            {
                                return ControllList.ControllsLibrary.LOWKICK;
                            }
                        }
                        else
                        {

                            return ControllList.ControllsLibrary.LOWPUNCHLEFT;
                        }
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

    public IEnumerator OpeningToCombo()
    {
        comboOpportunity += 1;
        Debug.Log(comboOpportunity);
        yield return new WaitForSeconds(0.1f);
        comboOpportunity = 0;
    }
    
}


public enum Gender
{
    MALE,
    FEMALE
}