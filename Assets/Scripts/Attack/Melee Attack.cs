using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 0.5f;
    public int damage = 10;
    public float attackCooldown = 0.23f;  // Cooldown duration in seconds
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    private bool canAttack = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
    }

    public void PerformAttack()
    {
        if (!canAttack)
        {
            return;
        }

        Debug.Log("PerformAttack");
        Vector2 attackOrigin = player.transform.position;
        animator.SetTrigger("attack");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackOrigin, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Melee Hit " + enemy.gameObject.name);
                enemy.TakeDamage(damage);
            }
        }

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.transform.position, attackRange);
        }
    }
}
