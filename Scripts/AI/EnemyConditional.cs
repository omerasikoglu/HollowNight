using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyConditional : Conditional
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected HealthManager destructable;

    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.Find("alive").GetComponent<Animator>();
        destructable = GetComponent<HealthManager>();
    }

}
