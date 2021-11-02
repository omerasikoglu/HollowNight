using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* <<SUM>>
 * Yerden k�l�� alma. �zel g��ler ekleme. Combatta yapabilce�in her �eyi ekler.
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
    [SerializeField] private Weapon[] holdingWeaponsArray; //karakterimizin �zerinde olan weaponlar

    private WeaponListSO weaponList; //t�m weapon datas�
    private Dictionary<Weapon, string> weaponDic;

    private Weapon holdingWeapon; //o an elimizde tuttu�umuz weapon

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

    private void SetData()
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
        HandleSwitchingWeaponsWithMouseScroll();
        HandleSwitchingWeaponsWithAlphaNumbers();

        HandleRightClick();

        if (holdingWeapon.weaponDistanceType == Weapon.WeaponDistanceType.Ranged)
        {

        }
        /*  TODO
         *   Tuttu�umuz silaha g�re girdi�imiz inputlar, VFXler, animasyonlar da
         *  g�re de�i�ir
         * 
         */
        switch (holdingWeapon.weaponType)
        {
            case Weapon.WeaponType.Null:
                if (Input.GetKeyDown(KeyCode.V)) CinemachineShake.Instance.ShakeCamera(2f, 2f); //test
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

    private void HandleRightClick()
    {
        if (inputReceiver.IsClickRightMouseButton)
        {
            //test
            bool isCritical = UnityEngine.Random.Range(0, 100) > 70;
            DamagePopup.Create(UtilsClass.GetScreenToWorldPosition(), 2990, isCritical, true);
        }
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

    private void HandleMagnetShooting()
    {
        shootWaitTime += Time.deltaTime;
        if (CheckIfCanShootWithMagnet())
        {
            shootWaitTime = 0;
            holdingWeapon.Attack();
        }
    }

    private bool CheckIfCanShootWithMagnet()
    {
        return inputReceiver.IsAttacking && holdingWeapon.HasEnoughAmmo() && shootWaitTime > holdingWeapon.GetShootingInterval();
    }
}
