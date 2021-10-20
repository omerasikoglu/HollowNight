using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public event System.EventHandler<OnDamagedEventArgs> OnDamaged;
    public class OnDamagedEventArgs : EventArgs
    {
        public int knockbackDirection;
    }
    private int currentHealth;
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void Damage(AttackDetails attackDetails)
    {
        //vuran soldan vuruyor, vurulan saða knockback'lencekse 1
        int direction = attackDetails.position.x < transform.position.x ? 1 : -1;

        currentHealth -= attackDetails.damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnDamaged?.Invoke(this, new OnDamagedEventArgs { knockbackDirection = direction });

    }
}
