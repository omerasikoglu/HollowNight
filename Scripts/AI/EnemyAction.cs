using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : Action
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Destructable destructable;

    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.Find("alive").GetComponent<Animator>();
        destructable = GetComponent<Destructable>();
    }
}
