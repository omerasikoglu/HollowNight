using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyAction : Action
{
    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected HealthManager destructable;
    [SerializeField] protected PlayerController player;

    private const string ALIVE = "alive";
    private const string PLAYER = "Player";

    public override void OnAwake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = transform.Find(ALIVE).GetComponent<Animator>();
        destructable = GetComponent<HealthManager>();
        if (!player) GameObject.Find(PLAYER);
    }
}
