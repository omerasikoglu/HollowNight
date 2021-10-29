using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Model
{
    private CombatController combatManager;
    private MovementManager movementManager;

    protected sealed override void Awake()
    {
        base.Awake();
        combatManager = GetComponent<CombatController>();
        movementManager = GetComponent<MovementManager>();
        
    }
    protected sealed override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) FlashBlue();
    }

   
}
