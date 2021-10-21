using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : RangedWeapon
{
    [Header("Magnet Script")]
    [SerializeField] private Projectile projectilePrefab;

    protected override void Awake()
    {
        base.Awake();
        weaponType = WeaponType.Magnet;

    }
    public override void Attack()
    {
        base.Attack();

        Projectile projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);
        projectile.Init(this);
    }
    
}
