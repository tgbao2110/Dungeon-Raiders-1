using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : Weapon
{
    protected Transform shootingPoint; // Point from where the bullet is shot
    [SerializeField] GunData gunData;

    // Start is called before the first frame update
    private void Awake()
    {
        shootingPoint = this.transform;
    }
    private void Update() 
    {
        RotateWeapon();
    }

    public override void AttackAction()
    {
        Enemy nearestEnemy = NearestEnemy();

        Vector3 direction = new Vector3();

        if (nearestEnemy != null)
        {
            Vector3 currentEnemyPosition = nearestEnemy.transform.position;
            // Calculate direction vector towards the nearest slime
            direction = (currentEnemyPosition - shootingPoint.position).normalized;
        }
        else
        {
            direction = player.joystick.Direction;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(gunData.bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * gunData.bulletSpeed;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(bullet, 2f);
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


    void RotateWeapon()
    {
        Vector3 direction = player.joystick.Direction;

        if (direction.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (direction.x < 0)
            {
                angle += 180f; // Add 180 degrees to flip the weapon
            }

            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }



}
