using UnityEngine;

public class SingleBulletAttack : AttackType
{

    public override void ExecuteAttack(GunData data, Transform shootingPoint, Vector3 shootDirection, float angle)
    {
        GameObject bullet = Object.Instantiate(data.bulletPrefab, shootingPoint.position, Quaternion.Euler(0f, 0f, angle));
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.velocity = shootDirection * data.bulletSpeed;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Object.Destroy(bullet, 2f);
    }
}