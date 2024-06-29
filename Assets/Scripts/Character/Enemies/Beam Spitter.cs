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
        room = this.transform.parent.transform.parent.GetComponent<EnemyRoom>();
        Debug.Log("Room = "+ room.name);
        InitializeHealth();
    }

    void Update()
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


    void Shoot()
    {
        // Get the direction vector from the enemy to the player
        Vector3 directionToPlayer = (player.position - shootingPoint.position).normalized;

        // Calculate the angle in degrees
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        attackType.ExecuteAttack(enemyData.bulletPrefab, enemyData.bulletSpeed, shootingPoint,directionToPlayer, angleToPlayer, enemyData.damage);
    }

    public override void SetAttackType()
    {
        attackType = new SingleBulletAttack();
    }

    protected override void Die()
    {
        room.KillEnemy();
        Destroy(this.gameObject);
    }
}
