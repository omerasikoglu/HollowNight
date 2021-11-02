using UnityEngine;

public class Hazard : MonoBehaviour
{
    /*
     * Hasarý verecek olanda takýlý
     * 
     */
    [SerializeField] private int hazardDamage = 1;

    [ReadOnly] public float damageIntervalTimer = 1f;
    [ReadOnly] public float damageIntervalDuration = 1f;
    //private bool canTakeDamage => damageIntervalTimer <= 0f;
    [ReadOnly] public bool canTakeDamage;

    private void Start()
    {
        canTakeDamage = true;

        damageIntervalTimer = 1f; damageIntervalDuration = 1f;
    }
    private void Update()
    {
        DamageIntervalTimer();

    }

    private void DamageIntervalTimer()
    {
        canTakeDamage = damageIntervalTimer <= 0;

        if (damageIntervalTimer > 0)
        {
            damageIntervalTimer -= Time.deltaTime;
        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (canTakeDamage)
        {
            CheckCollider(collider.gameObject);
            

        }
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (canTakeDamage)
    //    {
    //        CheckCollider(collider.gameObject);
    //        damageIntervalTimer += damageIntervalDuration;

    //    }
    //}

    private void CheckCollider(GameObject collider)
    {
        HealthManager healthManager = collider.gameObject.GetComponent<HealthManager>();

        if (healthManager != null)
        {

            healthManager.Hurt(new AttackDetails
            {
                damageAmount = hazardDamage,
                position = transform.position,
                knockbackPowerX = 150f,
                isCritical = false
            });
            damageIntervalTimer = damageIntervalDuration;
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    CheckCollision(collision);
    //}
    //private void CheckCollision(Collision2D collision)
    //{
    //    HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
    //    if (healthManager != null)
    //    {

    //        //Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
    //        //float multiplier = knockbackDirection.y < 0 ? 1.0f : 500.0f;
    //        //Vector2 knockbackStrength = knockbackDirection * multiplier;

    //        healthManager.Hurt(new AttackDetails
    //        {
    //            damageAmount = hazardDamage,
    //            position = collision.transform.position,
    //            knockbackPowerX = 10f,
    //            isCritical = false
    //        });
    //        //DamagePopup.Create(transform.position, hazardDamage, true, healthManager.CheckIsProtectionActive());

    //    }

    //}

}