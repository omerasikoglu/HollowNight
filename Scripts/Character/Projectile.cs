using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO AYARLA
    public static Projectile Create(Vector3 creatingPosition, Vector3 direction)
    {
        Transform projectileTransform = Instantiate(GameAssets.Instance.pfProjectileGold, Vector3.zero, Quaternion.identity);

        Projectile projectile = projectileTransform.GetComponent<Projectile>();
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
    public void Init(RangedWeapon weapon)
    {
        this.damage = weapon.GetWeaponDamage();
        projectileDisappearTime = weapon.GetProjectileDisappearTime();
        this.projectileImpactAudio = weapon.GetImpactAudio();
        rb.velocity = UtilsClass.GetMouseDirection(weapon.transform) * weapon.GetProjectileSpeed();

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
            Destroy(gameObject);


        }
    }

}
