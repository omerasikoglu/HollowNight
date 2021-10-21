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

    [Header("Weapon Script")]
    [ReadOnly] public WeaponDistanceType weaponDistanceType;
    [ReadOnly] public WeaponType weaponType;
    [SerializeField] protected string weaponName;
    [SerializeField] protected float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private float attackingInterval; //sald�r�lar aras� s�re

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
    protected virtual void Reload()
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
    public int GetWeaponDamage()
    {
        return damage;
    }
    public float GetShootingInterval()
    {
        return attackingInterval;
    }
    public string GetWeaponName()
    {
        return weaponName;
    }
   
}
