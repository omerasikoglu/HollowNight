using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* <<SUM>>
 * TODO: Vent sistemini buraya ekle
 * 
 * Hareketle ilgili her þey burada 
 * Dash'ler gibi özel güçler dahil deðil
 * Animasyonlar dahil deðil
 * YOUR KNOCKBACK dahil
 */
public class MovementManager : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private InputReceiver inputReceiver;
    private Rigidbody2D rigidbody;
    private HealthManager healthManager;
    private PlayerEnergyManager energyManager;

    [Header("Test")]
    [ReadOnly] public bool canMove = true;
    [ReadOnly] public bool isWalking;
    [ReadOnly] public float facingDirection = 1; //1=sað, -1=sol
    [ReadOnly] public bool canFlip = true;

    private Transform carryingWeaponsRootTransform;

    private void Awake()
    {
        inputReceiver = GetComponent<InputReceiver>();
        rigidbody = GetComponent<Rigidbody2D>();
        energyManager = GetComponent<PlayerEnergyManager>();

        carryingWeaponsRootTransform = transform.Find(Datalarimiz.ALIVE);
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
            
            foreach (Transform eachChild in carryingWeaponsRootTransform)
            {

                eachChild.localScale = new Vector3(-1f, -1f, 1f);

            }

            transform.localScale = Vector3.one;
        }
        else
        {
            foreach (Transform eachChild in carryingWeaponsRootTransform)
            {

                eachChild.localScale = Vector3.one;

            }
            
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        Vector2 offset = new Vector2(inputReceiver.mousePosition.x - screenPoint.x,
                                     inputReceiver.mousePosition.y - screenPoint.y);
        float angle = UtilsClass.GetAngleFromVector(offset);

        foreach (Transform eachChild in carryingWeaponsRootTransform)
        {
            eachChild.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        rigidbody.velocity = new Vector2(movementSpeed * inputReceiver.HorizontalInput, rigidbody.velocity.y);
    }

   
    public void ChangeFlipEnability()
    {
        //animator için public
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
