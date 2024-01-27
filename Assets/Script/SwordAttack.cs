using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float swordDamage = 1f;

    public float knockbackForce = 50f;
    
    public Collider2D swordCollider;

    Vector2 rightAttackOffset;
    
    private Status playerStatus;

    private void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
        rightAttackOffset = transform.position;
        playerStatus = GetComponentInParent<Status>();
    }

    //Checks for a enemy physics rigidbody and sends on hit damage to the GameObject
    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damagableObject = collider.gameObject.GetComponent<IDamageable>();

        if (damagableObject != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
        
            Vector2 direction = (Vector2)(collider.gameObject.transform.position - parentPosition).normalized;

            Vector2 knockback = direction * knockbackForce;
        
            //collider.SendMessage("OnHit", swordDamage, knockback);

            damagableObject.OnHit(swordDamage, knockback);
        }
        else
        {
            Debug.LogWarning("Collider does not implement IDamageble");
        }
        
        
    }


    public void AttackRight()
    {
        if (playerStatus.isEnergyZero)
            return;
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        if (playerStatus.isEnergyZero)
            return;
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }
    
    

}
