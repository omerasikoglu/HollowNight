using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform[] weapons;

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
        RotateCharacterAndWeaponsWithMousePosition();
    }
    private void RotateCharacterAndWeaponsWithMousePosition()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        if (inputReceiver.mousePosition.x < screenPoint.x)
        {
            foreach (Transform weaponTransform in weapons)
            {
                weaponTransform.localScale = new Vector3(-1f, -1f, 1f);

            }
            transform.localScale = Vector3.one;
        }
        else
        {
            foreach (Transform weaponTransform in weapons)
            {
                weaponTransform.localScale = Vector3.one;
            }
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        Vector2 offset = new Vector2(inputReceiver.mousePosition.x - screenPoint.x,
                                     inputReceiver.mousePosition.y - screenPoint.y);
        float angle = UtilsClass.GetAngleFromVector(offset);

        foreach (Transform weaponTransform in weapons)
        {
            weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
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

    private void CheckSpriteDirection()
    {
        if (canFlip && (facingDirection > 0 && inputReceiver.HorizontalInput < 0) ||
            (facingDirection < 0 && inputReceiver.HorizontalInput > 0)) Flip(transform);
    }

    public void Flip(Transform transform)
    {
        facingDirection = inputReceiver.HorizontalInput;
        transform.Rotate(0f, 180f, 0f);

    }
}
