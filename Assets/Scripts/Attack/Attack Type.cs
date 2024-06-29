using UnityEngine;

public abstract class AttackType
{
    public abstract void ExecuteAttack(GameObject bulletPrefab, float fireRate, Transform shootingPoint, Vector3 shootDirection, float angle, int damage);
}
