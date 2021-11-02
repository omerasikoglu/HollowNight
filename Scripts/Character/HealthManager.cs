using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    //Base Stuff
    [SerializeField] private int maxHealth;
    [SerializeField] private Transform immuneTextTransform;

    [ReadOnly] public int currentHealth;

    //Hit Protection
    [ReadOnly] public float hitProtectionDuration = 4.0f;
    [ReadOnly] public float hitProtectionTimer = 1.0f;
    [ReadOnly] public bool isProtectionTimerOver;

    [ReadOnly] public float blockTimer = 0.5f;
    [ReadOnly] public bool isBlockTimerOver;
    [ReadOnly] public bool invincible;

    bool isTooltipTimerActive = false;

    //Observer
    public event System.EventHandler<OnDamagedEventArgs> OnDamaged;
    public event System.EventHandler OnKilled;
    public event System.EventHandler<OnStunnedEventArgs> OnStunned;
    public event System.EventHandler<OnStunnedEventArgs> OnBlocked;
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
        Timers();
        ShowProtectionTimerText();
    }

    private void Timers()
    {
        if (hitProtectionTimer > 0)
        {
            hitProtectionTimer -= Time.deltaTime;
        }
        isProtectionTimerOver = hitProtectionTimer <= 0f;

        //if (blockTimer > 0)
        //{
        //    blockTimer -= Time.deltaTime;
        //}
        //isBlockTimerOver = blockTimer <= 0f;
    }

    private void ShowProtectionTimerText()
    {
        if (isTooltipTimerActive)
        {
            //aktive olunca sürekli kaç sn kaldýðýný gösterir protection'ýn bitmesine
            TooltipUI.Instance.Show("You re immune " + hitProtectionTimer.ToString("0.0") + " seconds",
                new TooltipUI.TooltipTimer { timer = 0.1f }, immuneTextTransform);
        }
        if (isProtectionTimerOver) { isTooltipTimerActive = false; }
    }

    public void Hurt(AttackDetails attackDetails)
    {
        //taken damage
        if (isProtectionTimerOver)
        {
            //vuran soldan vuruyor, vurulan saða knockback'lencekse 1
            int direction = attackDetails.position.x < transform.position.x ? 1 : -1;
            float knockbackPowerX = attackDetails.knockbackPowerX;

            currentHealth -= attackDetails.damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnDamaged?.Invoke(this, new OnDamagedEventArgs
            {
                knockbackDirection = direction,
                knockbackPowerX = knockbackPowerX
            });
            if (currentHealth <= 0)
            {
                OnKilled?.Invoke(this, EventArgs.Empty);
            }
            hitProtectionTimer = hitProtectionDuration;
        }
        else
        {

        }

        //kaç sn hasar yemiceðin yazýsý
        ActivateTooltipTimer();
        //kaç hasar aldýðýn yazýsý
        DamagePopup.Create(attackDetails.position, attackDetails.damageAmount, attackDetails.isCritical, !isProtectionTimerOver);
    }
    private void ActivateTooltipTimer()
    {
        isTooltipTimerActive = true;
    }
    public bool CheckIsProtectionActive()
    {
        return !isProtectionTimerOver;
    }
}
