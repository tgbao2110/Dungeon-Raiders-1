using System.Data.Common;
using UnityEngine;

public class BeamSpitter : Enemy
{
    public Transform shootingPoint;
    float interval = 0.5f;
    public Transform Player; 

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
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
        Vector3 directionToPlayer = (Player.position - shootingPoint.position).normalized;

        // Calculate the angle in degrees
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        attackType.ExecuteAttack(enemyData.bulletPrefab, enemyData.bulletSpeed, shootingPoint,directionToPlayer, angleToPlayer);
    }

    public override void SetAttackType()
    {
        attackType = new SingleBulletAttack();
    }

}
