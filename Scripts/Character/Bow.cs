using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : RangedWeapon
{
    [Header("Bow Script")]
    [SerializeField] private Projectile projectilePrefab;

    protected override void Attack()
    {
        base.Attack();
        Projectile projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
        projectile.Init(this);
    }
   

}
