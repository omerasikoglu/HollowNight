using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    [SerializeField] private KeyCode attackButton;
    [SerializeField] private KeyCode jumpButton;
    [SerializeField] private KeyCode holdButton;
    [SerializeField] private KeyCode dashButton;
    [SerializeField] private KeyCode blockButton;
    [SerializeField] private KeyCode weapon1Button;
    [SerializeField] private KeyCode weapon2Button;
    [SerializeField] private KeyCode mapButton;
    [SerializeField] private KeyCode specialPowerButton;

    [ReadOnly] public Vector3 mousePosition;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsHolding { get; private set; }
    public bool IsDashing { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsPressWeapon1Button { get; private set; }
    public bool IsPressWeapon2Button { get; private set; }
    public bool IsScrollingDown { get; private set; }
    public bool IsScrollingUp { get; private set; }
    public bool IsPressOpenMapButton { get; private set; }
    public bool IsPressSpecialPowerButton { get; private set; }

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Update()
    {
        ReceiveAxisInput();
        ReceiveButtonsInput();
        ReceiveMouseInput();
    }

    private void ReceiveMouseInput()
    {
        mousePosition = Input.mousePosition;
    }

    private void ReceiveAxisInput()
    {
        HorizontalInput = Input.GetAxis(HORIZONTAL);
        VerticalInput = Input.GetAxis(VERTICAL);
    }
    private void ReceiveButtonsInput()
    {
        IsAttacking = Input.GetKeyDown(attackButton);
        IsJumping = Input.GetKeyDown(jumpButton);
        IsHolding = Input.GetKey(holdButton);
        IsDashing = Input.GetKeyDown(dashButton);
        IsBlocking = Input.GetKey(blockButton);
        IsPressWeapon1Button = Input.GetKeyDown(weapon1Button);
        IsPressWeapon2Button = Input.GetKeyDown(weapon2Button);
        IsScrollingDown = Input.mouseScrollDelta.y < 0;
        IsScrollingUp = Input.mouseScrollDelta.y > 0;
        IsPressOpenMapButton = Input.GetKeyDown(mapButton);
        IsPressSpecialPowerButton = Input.GetKeyDown(specialPowerButton);
    }
}
