using UnityEngine;

public abstract class AttackType
{
    public abstract void ExecuteAttack(GameObject bulletPrefab, float bulletSpeed, Transform shootingPoint, Vector3 shootDirection, int damage);
}
