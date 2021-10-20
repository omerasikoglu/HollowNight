using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private InputReceiver inputReceiver;
    private Rigidbody2D rb;
    private PlayerEnergyManager energyManager;
    private Animator animator;

    [Header("Test")] //test için public
    [ReadOnly] public bool canMove = true;
    [ReadOnly] public bool isWalking;
    //[ReadOnly] public bool isFacingRight = true;
    [ReadOnly] public float facingDirection = 1;
    [ReadOnly] public bool canFlip = true;


    private void Awake()
    {
        inputReceiver = GetComponent<InputReceiver>();
        rb = GetComponent<Rigidbody2D>();
        energyManager = GetComponent<PlayerEnergyManager>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckSpriteDirection();
    }

    private void CheckSpriteDirection()
    {
        if (canFlip && (facingDirection > 0 && inputReceiver.HorizontalInput < 0) ||
            (facingDirection < 0 && inputReceiver.HorizontalInput > 0)) Flip();
    }

    private void Flip()
    {
        facingDirection = inputReceiver.HorizontalInput;
        transform.Rotate(0f, 180f, 0f);

    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * inputReceiver.HorizontalInput, rb.velocity.y);
    }




    //animator için public
    public void ChangeFlipEnability()
    {
        canFlip = !canFlip;
    }
    public float GetFacingDirection()
    {
        return facingDirection;
    }
}
