using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ControllList;

public class CharacterBehaviour : PlayerBase{
    [SerializeField] private HitboxElement AttackHitbox;
    private AudioController AudioControll;
    public Gender gender;

    void Start()
    {
        AudioControll = AudioController.Instance;

    }

    void Update()
    {
        if (Alive() == true)
        {
            LookAtOpponent();
            if (animator.currentAnimation == CharacterAnimationsStates.Idle)
            {
                if (attack() == ControllsLibrary.HIGHKICK)
                {
                    animator.PlayAnimation(ControllsLibrary.HIGHKICK);
                    AudioControll.PlaySound(gender, SoundEffect.ATTACK);
                    Hit(0.5f, 10, HitPosition.TOP);
                }
                if (attack() == ControllsLibrary.HIGHPUNCHLEFT)
                {
                    animator.PlayAnimation(ControllsLibrary.HIGHPUNCHLEFT);
                    AudioControll.PlaySound(gender, SoundEffect.ATTACK);
                    Hit(0.5f, 10, HitPosition.TOP);
                }
                if (attack() == ControllsLibrary.HIGHPUNCHRIGHT)
                {
                    animator.PlayAnimation(ControllsLibrary.HIGHPUNCHRIGHT);
                    AudioControll.PlaySound(gender, SoundEffect.ATTACK);
                    Hit(0.5f, 10, HitPosition.TOP);
                }


                if (attack() == ControllsLibrary.LOWKICK)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWKICK);
                    AudioControll.PlaySound(gender, SoundEffect.ATTACK);
                    Hit(0.5f, 10, HitPosition.BOT);
                }
                if (attack() == ControllsLibrary.LOWKNEE)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWKNEE);
                    AudioControll.PlaySound(gender, SoundEffect.ATTACK);
                    Hit(0.5f, 10, HitPosition.BOT);
                }
                if (attack() == ControllsLibrary.LOWPUNCHLEFT)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWPUNCHLEFT);
                    Hit(0.5f, 10, HitPosition.BOT);
                    AudioControll.PlaySound(gender, SoundEffect.ATTACK);
                }
                if (attack() == ControllsLibrary.LOWPUNCHRIGHT)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWPUNCHRIGHT);
                    AudioControll.PlaySound(gender, SoundEffect.ATTACK);
                    Hit(0.5f, 10, HitPosition.BOT);
                }

            }
            if (animator.currentAnimation != CharacterAnimationsStates.Hit)
            {
                BasicMovement();
            }
        }
      
    }
    
    void Hit(float Lifetime,float Damage,HitPosition HitArea)
    {
        AttackHitbox.hitboxClass.hitArea = HitArea;
        AttackHitbox.hitboxClass.damage = Damage;
        AttackHitbox.hitboxClass.lifetime = Lifetime;
        AttackHitbox.objectGameObject.SetActive(true);
    }
}
