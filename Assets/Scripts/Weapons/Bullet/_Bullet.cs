using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
