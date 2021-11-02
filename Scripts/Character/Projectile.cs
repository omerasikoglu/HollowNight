using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Projectile Create(RangedWeapon rangedWeapon)
    {
        Transform projectileTransform = Instantiate(GameAssets.Instance.pfProjectileGold, rangedWeapon.GetFirePoint(), Quaternion.identity);

        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.Setup(rangedWeapon);
        return projectile;
    }

    private int totalDamage;
    private float projectileDisappearTime;
    private Rigidbody2D rb;
    private AudioClip projectileImpactAudio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //sonsuza kadar yaþamasýn obje
        projectileDisappearTime -= Time.deltaTime;
        if (projectileDisappearTime < 0f) Destroy(gameObject);
    }
    public void Setup(RangedWeapon rangedWeapon)
    {
        this.totalDamage = rangedWeapon.GetTotalWeaponDamage();
        projectileDisappearTime = rangedWeapon.GetProjectileDisappearTime();
        this.projectileImpactAudio = rangedWeapon.GetImpactAudio();
        rb.velocity = UtilsClass.GetNormalizeMouseDirection(rangedWeapon.transform) * rangedWeapon.GetProjectileSpeed();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            //düþmana vurunca obje yok olsun
            healthManager.Hurt(
                new AttackDetails
                {
                    damageAmount = totalDamage,
                    position = this.transform.position,
                    knockbackPowerX = 1f,
                    isCritical = Random.Range(0, 2) % 2 == 0
                });
            AudioSource.PlayClipAtPoint(projectileImpactAudio, transform.position);
            //DamagePopup.Create(this.transform.position, totalDamage, Random.Range(0, 2) % 2 == 0,
            //                    healthManager.CheckIsProtectionActive());
            Destroy(gameObject);
        }
    }
}