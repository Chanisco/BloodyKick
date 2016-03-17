using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ControllList;

public class CharacterBehaviour : PlayerBase{
    [SerializeField] private HitboxElement AttackHitbox;


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
                    Hit(0.5f, 10, HitPosition.TOP);
                }
                if (attack() == ControllsLibrary.HIGHPUNCHLEFT)
                {
                    animator.PlayAnimation(ControllsLibrary.HIGHPUNCHLEFT);
                    Hit(0.5f, 10, HitPosition.TOP);
                }
                if (attack() == ControllsLibrary.HIGHPUNCHRIGHT)
                {
                    animator.PlayAnimation(ControllsLibrary.HIGHPUNCHRIGHT);
                    Hit(0.5f, 10, HitPosition.TOP);
                }

                if (attack() == ControllsLibrary.LOWKICK)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWKICK);
                    Hit(0.5f, 10, HitPosition.BOT);
                }
                if (attack() == ControllsLibrary.LOWKNEE)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWKNEE);
                    Hit(0.5f, 10, HitPosition.BOT);
                }
                if (attack() == ControllsLibrary.LOWPUNCHLEFT)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWPUNCHLEFT);
                    Hit(0.5f, 10, HitPosition.BOT);
                }
                if (attack() == ControllsLibrary.LOWPUNCHRIGHT)
                {
                    animator.PlayAnimation(ControllsLibrary.LOWPUNCHRIGHT);
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
