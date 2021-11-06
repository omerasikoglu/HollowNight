using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public static Projectile Create(RangedWeapon rangedWeapon)
    {
        Transform projectileTransform = Instantiate(rangedWeapon.GetHoldingProjectile(), rangedWeapon.GetFirePoint(), Quaternion.identity);

        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.Setup(rangedWeapon);
        return projectile;
    }

    [Header("[Projectile Script]")]
    [SerializeField] private AudioClip projectileImpactAudio;

    private Rigidbody2D rigidbody;

    //protected Transform projectileType;

    protected int projectileDamage;
    protected float projectileDisappearTime;
    protected float projectileSpeed;
    protected float projectileKnockbackPowerX;



    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        //sonsuza kadar yaþamasýn obje
        projectileDisappearTime -= Time.deltaTime;
        if (projectileDisappearTime < 0f) Destroy(gameObject);
    }

    private void Setup(RangedWeapon rangedWeapon)
    {
        rigidbody.velocity = UtilsClass.GetNormalizeMouseDirection(rangedWeapon.transform) * projectileSpeed;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            //düþmana vurunca obje yok olsun
            healthManager.Hurt(
                new AttackDetails
                {
                    damageAmount = projectileDamage,
                    position = this.transform.position,
                    knockbackPowerX = projectileKnockbackPowerX,
                    isCritical = UnityEngine.Random.Range(0, 2) % 2 == 0
                });
            AudioSource.PlayClipAtPoint(projectileImpactAudio, transform.position);
            //DamagePopup.Create(this.transform.position, totalDamage, Random.Range(0, 2) % 2 == 0,
            //                    healthManager.CheckIsProtectionActive());
            Destroy(gameObject);
        }
    }

    public virtual int GetProjectileDamage()
    {
        return projectileDamage;
    }
    public virtual float GetProjectileDisappearTime()
    {
        return projectileDisappearTime;
    }
    public virtual AudioClip GetImpactAudio()
    {
        return projectileImpactAudio;
    }
}
