using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO AYARLA
    public static Projectile Create(RangedWeapon weapon)
    {
        Transform projectileTransform = Instantiate(GameAssets.Instance.pfProjectileGold, weapon.GetFirePoint(), Quaternion.identity);

        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.Setup(weapon);
        return projectile;
    }


    private int damage;
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
    public void Setup(RangedWeapon weapon)
    {
        this.damage = weapon.GetWeaponDamage();
        projectileDisappearTime = weapon.GetProjectileDisappearTime();
        this.projectileImpactAudio = weapon.GetImpactAudio();
        rb.velocity = UtilsClass.GetNormalizeMouseDirection(weapon.transform) * weapon.GetProjectileSpeed();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManager healthManager = collision.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            //düþmana vurunca obje yok olsun
            healthManager.GetComponent<HealthManager>().Hurt(
                new AttackDetails { damageAmount = damage, position = this.transform.position, knockbackStrength = 1f });
            AudioSource.PlayClipAtPoint(projectileImpactAudio, transform.position);
            DamagePopup.Create(this.transform.position, damage, Random.Range(0, 2) % 2 == 0);
            Destroy(gameObject);


        }
    }

}
