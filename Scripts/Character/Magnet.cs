using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : RangedWeapon
{
    [Header("[Magnet Script]")]
    [SerializeField] protected Transform firePoint;
    [ReadOnly] public int magnetWeaponDamage = 2; //totalDamage = weaponDmg + bulletDmg
    [ReadOnly] public int magnetMaxAmmo = 2;

    protected sealed override void Awake()
    {
        base.Awake();
        weaponDamage = magnetWeaponDamage; //TODO: SO'dan çek bunu
        
        currentAmmo = magnetMaxAmmo;
        maxAmmo = magnetMaxAmmo;
    }

    public sealed override void Attack()
    {
        base.Attack();
        currentAmmo--;
        Projectile.Create(this);
    }

    public sealed override Vector3 GetFirePoint()
    {
        return firePoint.transform.position;
    }
}
