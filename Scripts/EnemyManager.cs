using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Model
{
    [SerializeField] int attackDamage;
    [SerializeField] Transform attackPosition;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
