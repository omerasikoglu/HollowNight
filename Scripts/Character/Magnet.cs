using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : RangedWeapon
{
    //[Header("Magnet Script")]
    //private Transform pfProjectile;

    protected override void Awake()
    {
        base.Awake();
        weaponType = WeaponType.Magnet;

    }
    public override void Attack()
    {
        base.Attack();
        
        Projectile.Create(this);
        //TODO: switch case'le taktýðýn merminin türüne göre create'lediði mermi deðiþsin
    }
    
}
