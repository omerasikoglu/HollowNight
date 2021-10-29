using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackDetails
{
    //public string attackName;
    //public float movementSpeed;
    public int damageAmount;
    
    public Vector2 position; //ata��n geldi�i y�n ve konumu i�in
    public float knockbackStrength; //geri tepme �iddeti i�in

}
