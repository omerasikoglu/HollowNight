using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/WeaponList")]
public class WeaponListSO : ScriptableObject
{
    public List<Weapon> list;
}
