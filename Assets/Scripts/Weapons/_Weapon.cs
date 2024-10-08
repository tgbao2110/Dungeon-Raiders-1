using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    protected GameObject player;
    public PlayerController playerController;
    protected Health health;
    protected float lastAtkTime = -99f;
    virtual public void AttackAction() { }
    protected AttackType attackType;
    bool isAtacking = false;
    protected Vector3 direction;
    int detectionRange = 30;


    public void Initialize(WeaponData data)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponentInChildren<PlayerController>();
        health = player.GetComponentInChildren<Health>();
        this.weaponData = data;        
        SetAttackType();
        FindObjectOfType<WeaponPanel>().SetWeapon(weaponData);
    }

    private void Update()
    {
        Rotate();
    }

    public void Attack()
    {
        if ((Time.time - lastAtkTime > weaponData.coolDown) && PlayerStats.Instance.Energy >= weaponData.energy)
        {
            AttackAction();
            health.UseEnergy(weaponData.energy);
            lastAtkTime = Time.time;
        }
    }

    public abstract void SetAttackType();

    void Rotate()
    {
        bool isFacingTarget = false;
        Transform target = FindClosestEnemy();
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
            isFacingTarget = true;
        }
        else
        {
            direction = playerController.joystick.Direction.normalized;
            isFacingTarget = false;
        }

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            Vector2 facingDirection = playerController.GetFacingDirection();
            if (!isFacingTarget)
            {
                transform.localScale = (direction.x >= 0) ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, -1f, 1f);
            }
            else
            {
                if (direction.x >= 0)
                {
                    transform.localScale = (facingDirection.x >= 0) ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    transform.localScale = (facingDirection.x >= 0) ? new Vector3(1f, -1f, 1f) : new Vector3(-1f, -1f, 1f);
                }
            }

        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < detectionRange && distance < minDistance)
            {
                closestEnemy = enemy.transform;
                minDistance = distance;
            }
        }

        return closestEnemy;
    }
}



