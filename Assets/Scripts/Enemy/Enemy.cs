using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NPC
{

    public float attackRange = 1.5f;
    public float stopDistance = 0.5f;
    public float attackCooldown = 2f;
    private float lastAttackTime = 0;

    private bool isAttacking = false;
    private bool canMove = true;

    public LayerMask targetLayer;
    private Vector2 playerDirection;


    protected override void Update()
    {
        base.Update();

        //si no encuentra al jugador vuelve
        if (playerTransform == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= attackRange && !isAttacking & Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }


    }


    private void AttackPlayer()
    {
        isAttacking = true;
        canMove = false;
        navMeshAgent.ResetPath();

        playerDirection = playerTransform.position - transform.position;

        int attackDirection = GetAttackDirection(playerDirection);

        if (playerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        animator.SetInteger("AttackDirection", attackDirection);
        animator.SetTrigger("DoAttack");

        Invoke("ResetAttack", 0.5f);
    }

    private void ResetAttack()
    {
        isAttacking = false;
        canMove = true;
    }


    private void FixedUpdate()
    {
        navMeshAgent.isStopped = !canMove;
    }


    private int GetAttackDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? 0 : 1;
        }
        else
        {
            return direction.y > 0 ? 2 : 3;
        }
    }


    public void DetectAndDamageTargets()
    {
        Vector2 attackPoint = (Vector2)transform.position + playerDirection.normalized * attackRange * 0.1f;
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint, attackRange, targetLayer);

        HashSet<GameObject> damagedTargets = new HashSet<GameObject>();

        foreach (Collider2D target in hitTargets)
        {

            GameObject obj = target.gameObject;

            if (damagedTargets.Contains(obj))
            {
                continue;
            }

            int layer = obj.layer;

            if (layer == LayerMask.NameToLayer("Player"))
            {
                Vector2 hitDirection = target.transform.position - transform.position;
                //creamos hash porque al tener 2 box collider en el jugador el enemigo nos hace 2 de daño, asi que como en los componentes del hash los 
                //elementos no se pueden repetir solo nos hara uno de daño
                obj.GetComponent<DamageReceiverPlayer>().ApplyDamage(1, true, false, hitDirection);

                damagedTargets.Add(obj);

            }
            


        }
    }
}

