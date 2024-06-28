using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    
    protected AttackType attackType = new SingleBulletAttack();
    public abstract void SetAttackType();

    private void Start() {
        SetAttackType();
    }
}
