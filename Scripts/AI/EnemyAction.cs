using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : Action
{
    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected Destructable destructable;
    [SerializeField] protected PlayerController player;

    private const string ALIVE = "alive";

    public override void OnAwake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = transform.Find(ALIVE).GetComponent<Animator>();
        destructable = GetComponent<Destructable>();
    }
}
