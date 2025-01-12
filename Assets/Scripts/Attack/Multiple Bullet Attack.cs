using UnityEngine;

public class MultipleBulletAttack : AttackType
{
    public override void ExecuteAttack(GameObject bulletPrefab, float bulletSpeed, Transform shootingPoint, Vector3 shootDirection, int damage)
    {
        shootDirection.Normalize();
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        int numberOfBullets = 12; // Number of bullets to instantiate
        float angleStep = 360f / numberOfBullets; // Angle step between each bullet

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the current angle for this bullet
            float currentAngle = angle + i * angleStep;

            // Calculate the shoot direction for this bullet based on the current angle
            Vector3 currentShootDirection = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0f);

            // Instantiate the bullet
            GameObject bullet = Object.Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDamage(damage);
            PauseManager.Instance.RegisterPausable(bulletScript); // Register bullet with PauseManager

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
