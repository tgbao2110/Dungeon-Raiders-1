using UnityEngine;

public class SingleBulletAttack : AttackType
{

    public override void ExecuteAttack(GameObject bulletPrefab, float bulletSpeed,Transform shootingPoint, Vector3 shootDirection, float angle)
    {
        GameObject bullet = Object.Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        // Set velocity based on shoot direction and bullet speed
        bulletRigidbody.velocity = shootDirection * bulletSpeed;

        // Set rotation of the bullet
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Object.Destroy(bullet, 2f);
    }
}