using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer), typeof(HealthManager))]
public abstract class Model : MonoBehaviour
{
    protected Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected HealthManager healthManager;
    protected MaterialTintColor materialTintColor;


    #region Your Knockback

    private float knockbackSpeedX = 10f;
    //private float knockbackDeathSpeedX = 15f;
    private float knockbackSpeedY = 2f;
    //private float knockbackDeathSpeedY = 4f;
    private float knockbackDuration = 0.1f;

    private bool isKnockbacking = false;
    private float knockbackStartTime;


    #endregion

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find(Datalarimiz.ALIVE).GetComponent<SpriteRenderer>();
        animator = transform.Find(Datalarimiz.ALIVE).GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();
        materialTintColor = GetComponent<MaterialTintColor>();

       
    }
    protected virtual void Start()
    {
        healthManager.OnDamaged += HealthManager_OnDamaged;
        healthManager.OnKilled += HealthManager_OnKilled;

        

    }
    protected virtual void Update()
    {
        CheckKnockback();
    }
    private void Knockback(int directionX) //düþman soldan vurunca direction 1 oluyor
    {
        isKnockbacking = true;
        knockbackStartTime = Time.time;
        rigidbody.velocity = new Vector2(directionX * knockbackSpeedX, knockbackSpeedY);

    }
    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && isKnockbacking)
        {
            isKnockbacking = false;
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y); //geri tepmesi durur
        }
    }

    private void HealthManager_OnKilled(object sender, System.EventArgs e)
    {
        FlashRed();
    }
    private void HealthManager_OnDamaged(object sender, HealthManager.OnDamagedEventArgs e)
    {
        Knockback(e.knockbackDirection);
        FlashYellow();
    }

    #region Flash
    private void FlashGreen()
    {
        materialTintColor.SetTintColor(new Color(0, 1, 0, 1));
    }
    private void FlashRed()
    {
        materialTintColor.SetTintColor(new Color(1, 0, 0, 1));
    }
    protected void FlashBlue()
    {
        materialTintColor.SetTintColor(new Color(0, 0, 1, 1));
    }
    private void FlashYellow()
    {
        materialTintColor.SetTintColor(new Color(255, 255, 65, 1));
    } 
    #endregion
}
