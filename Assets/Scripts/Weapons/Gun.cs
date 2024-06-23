using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] GameObject bulletPrefab; // Prefab for the bullet
    protected Transform shootingPoint; // Point from where the bullet is shot
    protected float bulletSpeed;

    // Start is called before the first frame update

    protected override void AttackAction()
    {
        Enemy nearestEnemy = NearestEnemy();

        Vector3 direction = new Vector3();
        // If a slime is found, aim towards it
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

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        // Get bullet rigidbody
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        // Set bullet velocity based on shooting point's rotation
        bulletRigidbody.velocity = direction * bulletSpeed;
        

        // Set bullet rotation to match its direction
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

    private void Update() {
        Vector3 direction = player.joystick.Direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
