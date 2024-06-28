using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBulletAttack : AttackType
{
    public override void ExecuteAttack(GameObject bulletPrefab, float bulletSpeed, Transform shootingPoint, Vector3 shootDirection, float angle)
    {
        float angleOffset = 15f; // Offset between each bullet's angle

        // Shoot three bullets with different angles
        for (int i = 0; i < 3; i++)
        {
            // Calculate the current angle for this bullet
            float currentAngle = angle + (i - 1) * angleOffset;

            // Calculate the shoot direction for this bullet based on the current angle
            Vector3 currentShootDirection = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0f);

            // Instantiate the bullet
            GameObject bullet = Object.Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            // Set velocity based on shoot direction and bullet speed
            bulletRigidbody.velocity = currentShootDirection * bulletSpeed;

            // Set rotation of the bullet
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

            // Destroy the bullet after 2 seconds
            Object.Destroy(bullet, 2f);
        }
    }
}
