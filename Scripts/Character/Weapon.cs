using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponDistanceType
    {
        Null, Ranged, Melee
    }
    public enum WeaponType
    {
        Null, Bow, Gun, Magnet
    }
    public enum BulletType
    {
        Null, Projectile, Coin
    }
    [Header("Weapon Script")]
    [ReadOnly] public WeaponDistanceType weaponDistanceType;
    [ReadOnly] public WeaponType weaponType;
    [ReadOnly] public BulletType bulletType;

    [ReadOnly] public string weaponName;
    [ReadOnly] public float attackRange;
    [ReadOnly] public float attackingInterval; //sald�r�lar aras� s�re

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackAudio;
    [SerializeField] private AudioClip attackAudio2;

    protected virtual void Awake()
    {

    }
    public virtual bool HasEnoughAmmo()
    {
        return true;
    }
    public virtual void Attack()
    {
        PlayAudioClip(GetAttackAudioClip());
    }
    public virtual void Reload()
    {

    }
    private AudioClip GetAttackAudioClip()
    {
        return UnityEngine.Random.Range(0, 1) % 2 == 0 ? attackAudio : attackAudio2;
    }

    private void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Stop();
        audioSource.Play();
    }

    public float GetShootingInterval()
    {
        return attackingInterval;
    }
    public string GetWeaponName()
    {
        return weaponName;
    }
    public int GetTotalWeaponDamage()
    {
        return GetWeaponDamage() + GetBulletDamage();
    }
    public int GetWeaponDamage() => weaponType switch
    {
        WeaponType.Magnet => 2,
        WeaponType.Bow => 1,
        WeaponType.Null => throw new System.NotImplementedException(),
        WeaponType.Gun => throw new System.NotImplementedException(),
        _ => 0,
    };

    public int GetBulletDamage() => bulletType switch
    {
        BulletType.Projectile => 1,
        BulletType.Coin => 2,
        _ => 1
    };
}
