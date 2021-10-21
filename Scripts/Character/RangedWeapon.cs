using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
   
    [Header("Ranged Weapon Script")]
    [SerializeField] protected Transform firePoint;

    [SerializeField] private int maxAmmo;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private float projectileDisappearTime;

    [SerializeField] private AudioClip projectileImpactAudio;
    [SerializeField] private AudioClip weaponReloadAudio;

    [ReadOnly]public int currentAmmo;

    protected override void Awake()
    {
        base.Awake();
        weaponDistanceType = WeaponDistanceType.Ranged;
        currentAmmo = maxAmmo==0 ? Mathf.FloorToInt(Mathf.Infinity) :  maxAmmo; //ammosuz ranged ise hep ammosu var
    }

    public override void Attack()
    {
        base.Attack();
        currentAmmo--;
        
    }
    public override bool HasEnoughAmmo()
    {
        return currentAmmo > 0 ? true : false;
    }
    public int GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    public AudioClip GetImpactAudio()
    {
        return projectileImpactAudio;
    }
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
  
    public float GetProjectileDisappearTime()
    {
        return projectileDisappearTime;
    }


}
