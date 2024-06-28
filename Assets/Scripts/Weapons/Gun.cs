using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Gun : Weapon
{
    [SerializeField] Transform shootingPoint; // Point from where the bullet is shot
    [SerializeField] public GunData gunData;

    protected override float coolDown => gunData.coolDown;

    private void Start() {
        InitializeWeaponData(gunData);
    }

    public override void AttackAction()
    {
        Enemy nearestEnemy = NearestEnemy();
        
        Vector3 shootDirection = transform.right;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        attackType.ExecuteAttack(gunData.bulletPrefab, gunData.bulletSpeed, shootingPoint, shootDirection, angle);

    }
    
    Enemy NearestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy nearestSlime = null;
        float minDistance = float.MaxValue;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                nearestSlime = enemy;
                minDistance = distance;
            }
        }

        return nearestSlime;
    }
}
