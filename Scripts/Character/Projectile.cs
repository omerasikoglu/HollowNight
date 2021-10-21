using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            //düþmana vurunca obje yok olsun
            destructable.GetComponent<HealthSystem>().Damage(new AttackDetails { damageAmount = damage, position = this.transform.position });
            AudioSource.PlayClipAtPoint(projectileImpactAudio, transform.position);
            Destroy(gameObject);


        }
    }

}
