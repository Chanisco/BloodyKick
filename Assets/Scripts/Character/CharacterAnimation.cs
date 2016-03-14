﻿using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {

    private Animator animator;
    private bool locked;
    public CharacterAnimationsStates currentAnimation = CharacterAnimationsStates.Idle;
    private int IdleString = Animator.StringToHash("Idle");

	void Awake ()
    {
        animator = GetComponent<Animator>();
        ConditionsOff();
    }
    private void Update()
    {
        AnimatorStateInfo currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        
        if (currentBaseState.IsName("Idle"))
        {
            currentAnimation = CharacterAnimationsStates.Idle;
        }
     
    }

    private void SetAnimationState(string targetAnimation)
    {
        switch (targetAnimation)
        {
            case "Hit":
                currentAnimation = CharacterAnimationsStates.Hit;
                break;
            case "Death":
                currentAnimation = CharacterAnimationsStates.Death;
                break;
            case "Kick":
                currentAnimation = CharacterAnimationsStates.Kick;
                break;
            case "Punch":
                currentAnimation = CharacterAnimationsStates.Punch;
                break;
            case "Movement":
                currentAnimation = CharacterAnimationsStates.Walk;
                break;
            case "Block":
                currentAnimation = CharacterAnimationsStates.Block;
                break;
        }
    }
    public void TurnAnimationOn(string targetbool)
    {
        if(locked == true)
        {
            return;
        }
        ConditionsOff();
        SetAnimationState(targetbool);
        animator.SetBool(targetbool, true);

    }
    public void TurnAnimationOff(string targetbool)
    {
        if (locked == true)
        {
            return;
        }
        animator.SetBool(targetbool, false);
    }

    public void PlayAnimation(string targetAnimation)
    {
        if (locked == true)
        {
            return;
        }
        SetAnimationState(targetAnimation);
        animator.Play(targetAnimation);
    }
    public void LockAnimationWithAnimation(string targetAnimation)
    {
        animator.Play(targetAnimation);
        SetAnimationState(targetAnimation);
        locked = true;
    }
    private void ConditionsOff()
    {
        TurnAnimationOff("Movement");
        TurnAnimationOff("Block");
    }
}
public enum CharacterAnimationsStates
{
    Idle,
    Walk,
    Punch,
    Kick,
    Hit,
    Death,
    Block
}