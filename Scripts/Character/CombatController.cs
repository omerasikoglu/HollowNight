using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* <<SUM>>
 * Yerden kýlýç alma. Özel güçler ekleme. Combatta yapabilceðin her þeyi ekler.
 * 
 */
public class CombatController : MonoBehaviour
{
    public event EventHandler<OnActiveWeaponTypeChangedArgs> OnActiveWeaponTypeChanged;
    public class OnActiveWeaponTypeChangedArgs : EventArgs
    {
        public Weapon weapon;
    }

    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private Weapon[] holdingWeaponsArray; //karakterimizin üzerinde olan weaponlar


    private WeaponListSO weaponList; //tüm weapon datasý
    private Dictionary<Weapon, string> weaponDic;

    private Weapon holdingWeapon; //o an elimizde tuttuðumuz weapon


    private InputReceiver inputReceiver;
    private PlayerEnergyManager energyManager;
    private int currentWeaponIndex;
    private float shootWaitTime;

    private void Awake()
    {
        //SetData();
        energyManager = GetComponent<PlayerEnergyManager>();
        inputReceiver = GetComponent<InputReceiver>();
    }

    private void SetDics()
    {
        weaponDic = new Dictionary<Weapon, string>();
        weaponList = Resources.Load<WeaponListSO>(typeof(WeaponListSO).Name);

        foreach (Weapon weapon in weaponList.list)
        {
            weaponDic[weapon] = weapon.GetWeaponName();
        }
    }

    private void Start()
    {
        SetActiveWeapon();
        OnActiveWeaponTypeChanged?.Invoke(this, new OnActiveWeaponTypeChangedArgs { weapon = holdingWeapon });
    }

    private void SetActiveWeapon()
    {
        holdingWeapon = holdingWeaponsArray[currentWeaponIndex];
        foreach (Weapon weapon in holdingWeaponsArray)
        {
            weapon.gameObject.SetActive(false);
        }
        holdingWeapon.gameObject.SetActive(true);
    }

    private void Update()
    {
        HandleWeaponSwitch();
        HandleRightClick();
        HandleShooting();
        HandleReloading();

        //if (holdingWeapon.GetIsRangedWeapon())
        //{

        //}
        /*  TODO
         *   Tuttuðumuz silaha göre girdiðimiz inputlar, VFXler, animasyonlar da
         *  göre deðiþir
         * 
         */
    }

    private void HandleReloading()
    {
        if (holdingWeapon.GetIsRangedWeapon() && inputReceiver.IsReloading)
        {
            holdingWeapon.Reload();
        }
    }

    #region HandleWeaponSwitch
    private void HandleWeaponSwitch()
    {
        HandleSwitchingWeaponsWithMouseScroll();
        HandleSwitchingWeaponsWithAlphaNumbers();
    }
    private void HandleSwitchingWeaponsWithAlphaNumbers()
    {
        if (inputReceiver.IsPressWeapon1Button)
        {
            if (holdingWeaponsArray.Length >= 1)
            {
                currentWeaponIndex = 0;
                SetActiveWeapon();
                OnActiveWeaponTypeChanged?.Invoke(this, new OnActiveWeaponTypeChangedArgs { weapon = holdingWeapon });
            }
        }
        if (inputReceiver.IsPressWeapon2Button)
        {
            if (holdingWeaponsArray.Length >= 2)
            {
                currentWeaponIndex = 1;
                SetActiveWeapon();
                OnActiveWeaponTypeChanged?.Invoke(this, new OnActiveWeaponTypeChangedArgs { weapon = holdingWeapon });
            }
        }
    }

    private void HandleSwitchingWeaponsWithMouseScroll()
    {
        if (inputReceiver.IsScrollingDown)
        {
            SwitchToPreviousWeapon();
            OnActiveWeaponTypeChanged?.Invoke(this, new OnActiveWeaponTypeChangedArgs { weapon = holdingWeapon });
        }
        if (inputReceiver.IsScrollingUp)
        {
            SwitchToNextWeapon();
            OnActiveWeaponTypeChanged?.Invoke(this, new OnActiveWeaponTypeChangedArgs { weapon = holdingWeapon });
        }
    }

    private void SwitchToPreviousWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
            currentWeaponIndex = holdingWeaponsArray.Length - 1;
        SetActiveWeapon();
    }

    private void SwitchToNextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex > holdingWeaponsArray.Length - 1)
            currentWeaponIndex = 0;
        SetActiveWeapon();
    }
    #endregion
    private void HandleRightClick()
    {
        if (inputReceiver.IsClickRightMouseButton)
        {
            //test
            bool isCritical = UnityEngine.Random.Range(0, 100) > 70;
            DamagePopup.Create(UtilsClass.GetScreenToWorldPosition(), 2990, isCritical, true);
        }
    }

    private void HandleShooting()
    {
        shootWaitTime += Time.deltaTime;
        if (CheckIfCanShoot())
        {
            shootWaitTime = 0;
            holdingWeapon.Attack();
        }
    }

    private bool CheckIfCanShoot()
    {
        //TODO: rangedWeapondan HasEnoughAmmoya ulaþ   
        return inputReceiver.IsAttacking && holdingWeapon.HasEnoughAmmo() && shootWaitTime > holdingWeapon.GetShootingInterval();
    }
}
