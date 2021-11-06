using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void Attack()
    {
        base.Attack();
    }


    protected override void Awake()
    {
        base.Awake();
        isRangedWeapon = false;
    }
}
