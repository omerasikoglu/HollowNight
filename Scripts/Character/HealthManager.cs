using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    //Base Stuff
    [SerializeField] private int maxHealth;
    [ReadOnly] public int currentHealth;

    //Hit Protection
    [ReadOnly] public float hitProtectionDuration = 1.0f;
    [ReadOnly] public float hitProtectionTimer = 1.0f;
    [ReadOnly] public bool invincible;
    private bool isProtectionOver => hitProtectionTimer <= 0f;

    //Observer
    public event System.EventHandler<OnDamagedEventArgs> OnDamaged;
    public event System.EventHandler OnKilled;
    public event System.EventHandler<OnStunnedEventArgs> OnStunned;
    public class OnDamagedEventArgs : EventArgs
    {
        public int knockbackDirection;
        public float knockbackPowerX;
    }
    public class OnStunnedEventArgs : EventArgs
    {
        public bool isStunned;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (hitProtectionTimer > 0)
        {
            hitProtectionTimer -= Time.deltaTime;
        }
    }
    public void Hurt(AttackDetails attackDetails)
    {
        //vuran soldan vuruyor, vurulan saða knockback'lencekse 1
        int direction = attackDetails.position.x < transform.position.x ? 1 : -1;
        float knockbackPowerX = attackDetails.knockbackPowerX;

        if (isProtectionOver)
        {
            currentHealth -= attackDetails.damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnDamaged?.Invoke(this, new OnDamagedEventArgs { knockbackDirection = direction, knockbackPowerX = knockbackPowerX });
            if (currentHealth <= 0)
            {
                OnKilled?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            // TODO: Saldýrý aldýðýnda bloklama iþareti çýkacak bunu playercontroller'a gönderen eventi yaz
        }
    }
}
