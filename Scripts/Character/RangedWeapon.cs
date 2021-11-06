using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    [Header("[Ranged Weapon Script]")]
    [ReadOnly] public Transform holdingProjectile;
    [SerializeField] protected List<Transform> holdingProjectileList;
    [SerializeField] private AudioClip weaponReloadAudio;

    protected override void Awake()
    {
        base.Awake();
        isRangedWeapon = true;


        holdingProjectile = holdingProjectileList[0];
        projectileDamage = holdingProjectile.GetComponent<Projectile>().GetProjectileDamage();
    }
    public virtual Vector3 GetFirePoint()
    {
        return Vector3.zero;
    }

    #region Overrides
    public override void Attack()
    {
        base.Attack();
    }

    public override void Reload()
    {
        base.Reload();
        PlayAudioClip(weaponReloadAudio);
    }

    public override bool HasEnoughAmmo()
    {
        return currentAmmo > 0;
    }

    #endregion

    public Transform GetHoldingProjectile()
    {
        return holdingProjectile;
    }
    private Transform ChangeHoldingProjectile()
    {
        int i = UnityEngine.Random.Range(0, holdingProjectileList.Count);
        holdingProjectile = holdingProjectileList[i];
        return holdingProjectileList[i];
    }

}
