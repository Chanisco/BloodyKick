using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBehaviour : PlayerBase{
    [SerializeField] private GameObject AreaCheckHitBox;
    [SerializeField] private HitboxElement AttackHitbox;


    void Update()
    {
        if (Alive() == true)
        {
            if (attack() == "Light")
            {
                animator.PlayAnimation("Punch");
                Hit(0.5f, 10, HitPosition.TOP);
            }
            else if (attack() == "Heavy")
            {
                animator.PlayAnimation("Kick");
                Hit(0.5f, 10, HitPosition.TOP);
            }
            LookAtOpponent();
            BasicMovement();
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
