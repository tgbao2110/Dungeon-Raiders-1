using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplewBulletGun : Gun
{
    public override void SetAttackType()
    {
        attackType = new TripleBulletAttack();
    }
}
