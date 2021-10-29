using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model : MonoBehaviour
{
    protected Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected HealthManager healthManager;
    protected MaterialTintColor materialTintColor;



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

    private void HealthManager_OnKilled(object sender, System.EventArgs e)
    {
        FlashRed();
    }

    private void HealthManager_OnDamaged(object sender, HealthManager.OnDamagedEventArgs e)
    {
        FlashYellow();

    }
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
}
