using System.Collections;
using System.Collections.Generic;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected Animator animator;
    public Transform player;
    public float detectionRange = 10f;
    public float stopDistance = 0f;
    public float moveSpeed = 2f;
    protected AttackType attackType = new SingleBulletAttack();
    protected Rigidbody2D rb;
    [SerializeField] protected SimpleFlash flash;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;

    public abstract void SetAttackType();

    protected void HandleMovement()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned in the inspector");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            MoveTowardsPlayer(distanceToPlayer);
        }
        else
        {
            animator.SetBool("isWalking", false);
            rb.velocity = Vector2.zero; // Stop moving if the player is out of range
        }
        
    }

    protected void MoveTowardsPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            animator.SetBool("isWalking", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
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

    protected void InitializeHealth()
    {
        maxHealth = enemyData.maxHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        flash.Flash();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
