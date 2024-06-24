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

    public override void AttackAction()
    {
        Enemy nearestEnemy = NearestEnemy();
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(gunData.bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (direction == Vector3.zero) direction = new Vector3(1,1,1);
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






}
