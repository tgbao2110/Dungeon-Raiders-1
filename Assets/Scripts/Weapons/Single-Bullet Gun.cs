using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBulletGun : Gun
{
    protected override void Shoot(Transform shootingPoint, Vector3 shootDirection, float angle)
    {
        GameObject bullet = Instantiate(gunData.bulletPrefab, shootingPoint.position, Quaternion.Euler(0f, 0f, angle));
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.velocity = shootDirection * gunData.bulletSpeed;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(bullet, 2f);
    }
}
