using System.Collections;
using System.Collections.Generic;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public class BeamSpitter : Enemy, IPausable
{
    public Transform shootingPoint;
    float interval = 0.5f;
    bool isEnabled = true;

    void Start()
    {
        Initialize();
        InitializeHealth();
        RegisterWithPauseManager();
    }

    void Update()
    {
        if (isEnabled)
        {
            HandleMovement();
            HandleAttack();
        }
    }

    void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        room = this.transform.parent.transform.parent.GetComponent<EnemyRoom>();
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

    [SerializeField] GameObject coinPrefab;

    void Shoot()
    {
        Vector3 directionToPlayer = (player.position - shootingPoint.position).normalized;
        attackType.ExecuteAttack(enemyData.bulletPrefab, enemyData.bulletSpeed, shootingPoint, directionToPlayer, enemyData.damage);
    }

    public override void SetAttackType()
    {
        attackType = new SingleBulletAttack();
    }

    protected override void Die()
    {
        room.KillEnemy();
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void RegisterWithPauseManager()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.RegisterPausable(this);
        }
        else
        {
            StartCoroutine(RetryRegisterWithPauseManager());
        }
    }

    private IEnumerator RetryRegisterWithPauseManager()
    {
        while (PauseManager.Instance == null)
        {
            yield return null; // Wait until the next frame
        }
        PauseManager.Instance.RegisterPausable(this);
    }

    private void OnDestroy()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.UnregisterPausable(this);
        }
    }

    public void SetPaused(bool isPaused)
    {
        isEnabled = !isPaused;
        rb.velocity = Vector2.zero; // Stop the enemy's movement while paused
    }
}
