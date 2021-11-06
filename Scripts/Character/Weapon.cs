using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Weapon : MonoBehaviour
{
    [Header("[Weapon Script]")]

    //both
    [ReadOnly] public string weaponName;
    [ReadOnly] public float attackingInterval; //sald�r�lar aras� s�re
    [ReadOnly] public int weaponDamage;
    [ReadOnly] public bool isRangedWeapon;


    //ranged
    [ReadOnly] public float attackRange;
    [ReadOnly] public int projectileDamage;
    [ReadOnly] public int maxAmmo;
    [ReadOnly] public int currentAmmo;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackAudio;
    [SerializeField] private AudioClip attackAudio2;


    //melee


    #region Both
    protected virtual void Awake()
    {

    }
    public virtual void Attack()
    {
        PlayAudioClip(GetAttackAudioClip());
    }


    public bool GetIsRangedWeapon()
    {
        return isRangedWeapon;
    }
    public int GetTotalWeaponDamage()
    {
        return GetOnlyWeaponDamage() + GetProjectileDamage();
    }
    public int GetOnlyWeaponDamage()
    {
        return weaponDamage;
    }

    protected void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Stop();
        audioSource.Play();
    }
    private AudioClip GetAttackAudioClip()
    {
        return UnityEngine.Random.Range(0, 2) % 2 == 0 ? attackAudio : attackAudio2;
    }

    public string GetWeaponName()
    {
        return weaponName;
    }
    #endregion


    #region Ranged
    public virtual bool HasEnoughAmmo()
    {
        return true;
    }
    public virtual void Reload()
    {
        currentAmmo = maxAmmo;
    }
    public int GetProjectileDamage()
    {
        return projectileDamage;
    }
    public float GetShootingInterval()
    {
        return attackingInterval;
    }

    #endregion


    #region Melee

    #endregion



















}

