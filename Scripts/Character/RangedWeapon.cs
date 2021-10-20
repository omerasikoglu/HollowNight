using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Ranged Weapon Script")]

    
    [SerializeField] private int maxAmmo;
    [SerializeField] private int projectileSpeed;

    [SerializeField] private AudioClip projectileImpactAudio;
    [SerializeField] private AudioClip weaponReloadAudio;

    private int currentAmmo;

    protected override void Awake()
    {
        base.Awake();
        currentAmmo = maxAmmo==0 ? Mathf.FloorToInt(Mathf.Infinity) :  maxAmmo; //ammosuz ranged ise hep ammosu var
    }

    protected override void Attack()
    {
        base.Attack();
        currentAmmo--;
        
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
    public bool HasEnoughAmmo()
    {
        return currentAmmo > 0 ? true : false;
    }


}
