using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private Weapon[] weaponsArray;

    private InputReceiver inputReceiver;
    private PlayerEnergyManager energyManager;
    private Weapon holdWeapon;
    private int currentWeaponIndex;
    private float shootWaitTime;

    private void Awake()
    {
        inputReceiver = GetComponent<InputReceiver>();
        energyManager = GetComponent<PlayerEnergyManager>();
    }
    private void Start()
    {
        SetActiveWeapon();
    }

    private void SetActiveWeapon()
    {
        holdWeapon = weaponsArray[currentWeaponIndex];
        foreach (Weapon weapon in weaponsArray)
        {
            weapon.gameObject.SetActive(false);
        }
        holdWeapon.gameObject.SetActive(true);
    }

    private void Update()
    {
        HandleSwitchingWeapons();
        HandleShooting();
    }

    private void HandleSwitchingWeapons()
    {
        if (inputReceiver.IsScrollingDown)
        {
            SwitchToPreviousWeapon();
        }
        if (inputReceiver.IsScrollingUp)
        {
            SwitchToNextWeapon();
        }
    }

    private void SwitchToPreviousWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
            currentWeaponIndex = weaponsArray.Length - 1;
        SetActiveWeapon();
    }

    private void SwitchToNextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex > weaponsArray.Length - 1)
            currentWeaponIndex = 0;
        SetActiveWeapon();
    }

    private void HandleShooting()
    {
        shootWaitTime += Time.deltaTime;
        if (CheckIfCanShoot())
        {
            shootWaitTime= 0;
            holdWeapon.Attack();
        }
    }

    private bool CheckIfCanShoot()
    {
        return inputReceiver.IsAttacking && shootWaitTime > holdWeapon.GetShootingInterval();
    }
}
