using UnityEngine;

public abstract class AttackType
{
    public abstract void ExecuteAttack(GunData data, Transform shootingPoint, Vector3 shootDirection, float angle);
}
