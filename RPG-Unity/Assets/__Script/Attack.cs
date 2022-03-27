using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Properties for the 2 basic attacks
    public float attackDist = 1f;
    
    // Properties for attack 1
    [Header("Attack 1")]
    public float attack1Range = 1f;
    public float attack1Damage = 50f;
    public float attackDelay = 1f;

    // Properties for attack 2
    [Header("Attack 2")]
    public float attack2Range = 2f;
    public float attack2Damage = 50f;
    public float attack2Delay = 1f;
    public float attack2Speed = 2000f;
    
    public LayerMask enemyLayers;
    public GameObject attack1Sprite;
    public GameObject attack2Sprite;

    // Variables set by the script
    [Header("Set Dynamically")] 
    public Vector2 attack1Point;
    public Vector2 attack2Point;
    public Vector2 lastMoveDir;
    public bool canAttack = true;

    private void Start()
    {
        // Enemys are triggers so need to be able to interact with them
        Physics2D.queriesHitTriggers = true;
    }

    void FixedUpdate()
    {
        // Updating attack point
        if (GetComponent<Movement>().moveDir != Vector2.zero)
        {
            lastMoveDir = GetComponent<Movement>().moveDir;
        }
        attack1Point = lastMoveDir * attackDist + new Vector2(transform.position.x, transform.position.y);
        attack2Point = lastMoveDir * attackDist * attack2Range / 2 +
                       new Vector2(transform.position.x, transform.position.y);
        
        // Listening for attack 1
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(Attack1());
            }
        }

        // Listening for attack 2
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(Attack2());
            }
        }
    }
    
    // Attack 1
    IEnumerator Attack1()
    {
        // Display animation
        GameObject attack1Sprite = Instantiate(this.attack1Sprite);
        attack1Sprite.transform.position = attack1Point;
        attack1Sprite.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x));
        Destroy(attack1Sprite, 0.1f);

        // Gets enemys hit by attack
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attack1Point, attack1Range, enemyLayers);

        // Damages enemies
        foreach (Collider2D enemy in enemyHits)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attack1Damage);
        }

        // attack cooldown
        yield return new WaitForSecondsRealtime(attackDelay);
        canAttack = true;
    }
    
    // Attack 2
    IEnumerator Attack2()
    {
        // Display animation
        GameObject attack2Sprite = Instantiate(this.attack2Sprite);
        attack2Sprite.transform.position = attack1Point;
        attack2Sprite.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x));
            
        // Setting proporties of attack
        attack2Sprite.GetComponent<Rigidbody2D>().velocity = lastMoveDir * attack2Speed * attack2Range / 0.4f * Time.fixedDeltaTime;
        attack2Sprite.GetComponent<ProjectileAttack>().SetDamage(attack2Damage);
            
        // Destroying attack
        Destroy(attack2Sprite, 0.3f);
        
        yield return new WaitForSecondsRealtime(attack2Delay);
        canAttack = true;
    }
}
