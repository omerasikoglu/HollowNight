using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float disappearTime = 2f;
    [SerializeField] private float knockbackPowerX = 3f;


    protected sealed override void Awake()
    {
        base.Awake();

        //projectileType = GameAssets.Instance.pfProjectileGold;
        projectileDamage = damage;
        projectileDisappearTime = disappearTime;
        projectileSpeed = speed;
        projectileKnockbackPowerX = knockbackPowerX;


    }
    
}