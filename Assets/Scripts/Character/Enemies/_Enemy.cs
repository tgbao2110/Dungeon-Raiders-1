using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    public Transform player;
    public float detectionRange = 10f;
    public float stopDistance = 0f;
    public float moveSpeed = 2f;
    protected AttackType attackType = new SingleBulletAttack(   );

    protected Rigidbody2D rb;

    public abstract void SetAttackType();

    private void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected void MoveTowardsPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw stop distance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
