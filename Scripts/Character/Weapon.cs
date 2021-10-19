using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Script")]
    [SerializeField] protected float range;
    [SerializeField] private int damage;
    [SerializeField] private float attackingInterval; //sald�r�lar aras� s�re

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackAudio;
    [SerializeField] private AudioClip attackAudio2;


    protected virtual void Awake()
    {

    }

    protected virtual void Attack()
    {
        PlayAudioClip(GetAttackAudioClip());
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
}