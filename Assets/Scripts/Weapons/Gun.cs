using UnityEngine;
public abstract class Gun : Weapon
{
    [SerializeField] Transform shootingPoint;

    public override void AttackAction()
    {
        Vector3 shootDirection = transform.right;

        attackType.ExecuteAttack(weaponData.bulletPrefab, weaponData.bulletSpeed, shootingPoint, shootDirection, weaponData.damage);

    }
}
