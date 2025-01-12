using UnityEngine;

public class SingleBulletAttack : AttackType
{
    public override void ExecuteAttack(GameObject bulletPrefab, float bulletSpeed, Transform shootingPoint, Vector3 shootDirection, int damage)
    {
        // Ensure shootDirection is normalized
        shootDirection.Normalize();
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        GameObject bullet = Object.Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(damage);
        PauseManager.Instance.RegisterPausable(bulletScript); // Register bullet with PauseManager

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = shootDirection * bulletSpeed;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Destroy the bullet after 2 seconds
        Object.Destroy(bullet, 2f);
    }
}
