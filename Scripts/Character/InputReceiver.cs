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



    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsHolding { get; private set; }
    public bool IsDashing { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsPressWeapon1Button { get; private set; }
    public bool IsPressWeapon2Button { get; private set; }
    public bool IsPressOpenMapButton { get; private set; }
    public bool IsPressSpecialPowerButton { get; private set; }

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Update()
    {
        ReceiveButtonsInput();
        ReceiveAxisInput();

    }
    private void ReceiveButtonsInput()
    {
        HorizontalInput = Input.GetAxis(HORIZONTAL);
        VerticalInput = Input.GetAxis(VERTICAL);
    }
    private void ReceiveAxisInput()
    {
        IsAttacking = Input.GetKeyDown(attackButton);
        IsJumping = Input.GetKeyDown(jumpButton);
        IsHolding = Input.GetKeyDown(holdButton);
        IsDashing = Input.GetKeyDown(dashButton);
        IsBlocking = Input.GetKeyDown(blockButton);
        IsPressWeapon1Button = Input.GetKeyDown(weapon1Button);
        IsPressWeapon2Button = Input.GetKeyDown(weapon2Button);
        IsPressOpenMapButton = Input.GetKeyDown(mapButton);
        IsPressSpecialPowerButton = Input.GetKeyDown(specialPowerButton);
    }
}
