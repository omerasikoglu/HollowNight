using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : RangedWeapon
{
    [Header("Magnet Script")]
    [SerializeField] private BulletType holdingBulletType;
    [ReadOnly] public int weaponDamage; //totalDamage = weaponDmg + bulletDmg
    [ReadOnly] public int bulletDamage;

    protected sealed override void Awake()
    {
        base.Awake();
        weaponType = WeaponType.Magnet;
        bulletType = holdingBulletType;

        weaponDamage = GetWeaponDamage();
        bulletDamage = GetBulletDamage();
        projectileSpeed = 5f;

    }

    public sealed override void Attack()
    {
        base.Attack();

        switch (bulletType)
        {
            case BulletType.Projectile:
                Projectile.Create(this);
                break;
            case BulletType.Coin:
                Projectile.Create(this);
                break;
            case BulletType.Null:
                break;
            default:
                break;
        }
    }

}
