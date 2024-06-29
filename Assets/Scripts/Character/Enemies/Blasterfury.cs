using UnityEngine;

public class Blasterfury : Enemy
{
    public Transform shootingPoint;
    float interval = 0.5f;

    private int currentAttackCount = 0;
    private int maxAttackCount = 0;
    private bool usingSingleBullet = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SetAttackType();
        SetNextAttackCount();
    }

    protected override void Update()
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

        // Calculate the angle in degrees
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        attackType.ExecuteAttack(enemyData.bulletPrefab, enemyData.bulletSpeed, shootingPoint, directionToPlayer, angleToPlayer);

        // Increment the current attack count and check if it's time to switch attack types
        currentAttackCount++;
        if (currentAttackCount >= maxAttackCount)
        {
            usingSingleBullet = !usingSingleBullet;
            SetAttackType();
            SetNextAttackCount();
        }
    }

    public override void SetAttackType()
    {
        if (usingSingleBullet)
        {
            attackType = new SingleBulletAttack();
        }
        else
        {
            attackType = new MultipleBulletAttack();
        }
    }

    private void SetNextAttackCount()
    {
        currentAttackCount = 0;
        maxAttackCount = Random.Range(2, 6); // Randomize between 2 to 5 (inclusive)
    }
}
