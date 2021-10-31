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
        //TODO: switch case'le takt���n merminin t�r�ne g�re create'ledi�i mermi de�i�sin
    }
    
}
