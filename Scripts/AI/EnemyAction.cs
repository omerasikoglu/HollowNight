using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyAction : Action
{
    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected HealthManager destructable;
    [SerializeField] protected PlayerController player;

    public override void OnAwake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = transform.Find(Datalarimiz.ALIVE).GetComponent<Animator>();
        destructable = GetComponent<HealthManager>();
        if (!player) GameObject.Find(Datalarimiz.PLAYER);
    }
}
