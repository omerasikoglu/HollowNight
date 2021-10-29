using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackDetails
{
    //public string attackName;
    //public float movementSpeed;
    public int damageAmount;
    
    public Vector2 position; //ataðýn geldiði yön ve konumu için
    public float knockbackStrength; //geri tepme þiddeti için

}
