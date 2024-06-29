using System.Data.Common;
using UnityEngine;

public class BeamSpitter : Enemy
{
    public Transform shootingPoint;
    float interval = 0.5f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected override void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleAttack()
    {
        if(interval>0)
        {
            interval-= Time.deltaTime;
        }
        else
        {
            Shoot();
            interval = enemyData.fireRate;
        }
    }

    void HandleMovement()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned in the inspector");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distanceToPlayer);

        if (distanceToPlayer < detectionRange)
        {
            MoveTowardsPlayer(distanceToPlayer);
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving if the player is out of range
        }
        
    }

    void Shoot()
    {
        // Get the direction vector from the enemy to the player
        Vector3 directionToPlayer = (player.position - shootingPoint.position).normalized;

        // Calculate the angle in degrees
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        attackType.ExecuteAttack(enemyData.bulletPrefab, enemyData.bulletSpeed, shootingPoint,directionToPlayer, angleToPlayer);
    }

    public override void SetAttackType()
    {
        attackType = new SingleBulletAttack();
    }

}
