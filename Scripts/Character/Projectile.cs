using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private Rigidbody2D rb;
    private AudioClip projectileImpactAudio;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(RangedWeapon weapon)
    {
        this.damage = weapon.GetWeaponDamage();
        this.projectileImpactAudio = weapon.GetImpactAudio();
        rb.velocity = UtilsClass.GetMouseDirection(weapon.transform) * weapon.GetProjectileSpeed();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent(out EnemyManager enemy))
        {

            AudioSource.PlayClipAtPoint(projectileImpactAudio, transform.position);
            Destroy(gameObject);
        }
    }

}
