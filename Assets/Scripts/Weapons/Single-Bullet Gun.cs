using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBulletGun : Gun
{
    public override void SetAttackType()
    {
        attackType = new SingleBulletAttack();
    }
}
