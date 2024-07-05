using UnityEngine;

public class Blasterfury : Enemy
{
    public Transform shootingPoint;
    float interval = 0.5f;

    private int currentAttackCount = 0;
    private int maxAttackCount = 0;
    private bool usingSingleBullet = true;
    private bool isEnabled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        room = this.transform.parent.transform.parent.GetComponent<EnemyRoom>();
        InitializeHealth();
        SetAttackType();
        SetNextAttackCount();

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
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

    protected override void Die()
    {
        room.KillEnemy();
        Destroy(this.gameObject);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Playing)
        {
            isEnabled = true;
        }

        else
        {
            isEnabled = false;
            rb.velocity = Vector2.zero;
        }
    }
}
