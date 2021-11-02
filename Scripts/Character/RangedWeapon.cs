using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    [Header("Ranged Weapon Script")]
    [SerializeField] protected Transform firePoint;

    [SerializeField] private int maxAmmo;
    [ReadOnly] public int currentAmmo;

    [ReadOnly] public float projectileSpeed = 10f;
    [ReadOnly] public float projectileDisappearTime = 2;

    [SerializeField] private AudioClip projectileImpactAudio;
    [SerializeField] private AudioClip weaponReloadAudio;

    

    protected override void Awake()
    {
        base.Awake();
        weaponDistanceType = WeaponDistanceType.Ranged;
        currentAmmo = maxAmmo == 0 ? Mathf.FloorToInt(Mathf.Infinity) : maxAmmo; //ammosuz ranged ise hep ammosu var
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
    public float GetProjectileSpeed()
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
    public Vector3 GetFirePoint()
    {
        return firePoint.transform.position;
    }
}
