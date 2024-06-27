using UnityEngine;

public interface IAttacker
{
    void PerformAttack(Transform shootingPoint, Vector3 shootDirection, float angle);
}

