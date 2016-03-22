//** IDEA SCRAPT **\\
using UnityEngine;
using System.Collections;

public class StrockBehaviour : MonoBehaviour {

    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        DetermineIdle();
    }
    
    void Update()
    {
        if (AnimationComplete(animator.GetCurrentAnimatorStateInfo(0)))
        {

        }
    }
    private void DetermineIdle()
    {
        animator.SetInteger("Idle", Random.Range(0, 2));
    }

    private bool AnimationComplete(AnimatorStateInfo animInfo)
    {
    
        return true;
    }
}

public enum StrockEmotions
{
    Idle,
    Hammer,
    Punch,
    Throw
}
