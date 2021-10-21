using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  TODO
 *  OBSERVER AKTÝF ÝLERDE KARAR VER
 *   Silahý taktýðýmýzda onun ok mu yoksa kalkan mý olduðuna göre 
 *  input kontrollerini deðiþtirir sonra da combat manager'a gönderir
 */

public class WeaponController : MonoBehaviour
{

    private PlayerCombatManager combatManager;

    private Weapon weapon;

    private void Awake()
    {
        combatManager = GetComponent<PlayerCombatManager>();
    }
    private void Start()
    {
        combatManager.OnActiveWeaponTypeChanged += CombatManager_OnActiveWeaponTypeIsChanged;
    }

    private void CombatManager_OnActiveWeaponTypeIsChanged(object sender, PlayerCombatManager.OnActiveWeaponTypeChangedArgs e)
    {
        weapon = e.weapon;
    }

    private void Update()
    {
        switch (weapon.weaponType)
        {
            case Weapon.WeaponType.Null:
                break;
            case Weapon.WeaponType.Bow:
                break;
            case Weapon.WeaponType.Gun:
                break;
            case Weapon.WeaponType.Magnet:
                HandleMagnetShooting();
                break;
            default:
                break;
        }
    }

    private void HandleMagnetShooting()
    {
    }
}
