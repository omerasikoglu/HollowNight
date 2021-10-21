using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Destructable : Hittable
{
    public int health = 10;

    public int CurrentHealth { get; set; }
    public bool Invincible { get; set; }

    public event Action OnDestroyed;

    protected override void Awake()
    {
        base.Awake();
        CurrentHealth = health;
        Invincible = false;
    }
}
