using System.Data.Common;
using UnityEngine;

public class BeamSpitter : Enemy
{
    public Transform shootingPoint;
    float interval = 0.5f;

    bool isEnabled = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        room = this.transform.parent.transform.parent.GetComponent<EnemyRoom>();
        InitializeHealth();
    }

    void Update()
    {
        if(isEnabled)
        {
            HandleMovement();
            HandleAttack();
        }
    }

    void HandleAttack()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;
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
        attackType.ExecuteAttack(enemyData.bulletPrefab, enemyData.bulletSpeed, shootingPoint, directionToPlayer, enemyData.damage);
    }

    public override void SetAttackType()
    {
        attackType = new SingleBulletAttack();
    }

    protected override void Die()
    {
        Debug.Log(this.name + " Died!");
        room.KillEnemy();

        Destroy(this.gameObject);
    }
}
