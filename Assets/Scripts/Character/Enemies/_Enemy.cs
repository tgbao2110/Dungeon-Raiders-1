using System.Collections.Generic;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected Animator animator;
    public Transform player;
    protected EnemyRoom room;
    public float detectionRange = 10f;
    public float stopDistance = 0f;
    public float moveSpeed = 2f;
    protected AttackType attackType = new SingleBulletAttack();
    protected Rigidbody2D rb;
    [SerializeField] protected SimpleFlash flash;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected MiniHealthBar healthBar;
    private bool isDead = false;

    public float separationDistance = 1f; // Distance to maintain from other enemies

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
            ApplySeparation();
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

    protected void ApplySeparation()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, separationDistance);
        Vector2 separationForce = Vector2.zero;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.gameObject != gameObject)
            {
                Vector2 direction = transform.position - collider.transform.position;
                separationForce += direction.normalized / direction.magnitude;
            }
        }

        rb.velocity += separationForce * moveSpeed;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, separationDistance);
    }

    protected void InitializeHealth()
    {
        maxHealth = enemyData.maxHealth;
        currentHealth = maxHealth;
        healthBar.SetHealth(enemyData.name, currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(enemyData.name, currentHealth, maxHealth);
        flash.Flash();
        if (currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }

    protected abstract void Die();
}
