using UnityEngine;
public abstract class Gun : Weapon
{
    [SerializeField] Transform shootingPoint;

    public override void AttackAction()
    {
        Vector3 shootDirection = transform.right;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        attackType.ExecuteAttack(weaponData.bulletPrefab, weaponData.bulletSpeed, shootingPoint, shootDirection, angle, weaponData.damage);

    }
}
