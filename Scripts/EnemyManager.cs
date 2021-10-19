using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int attackDamage;
    [SerializeField] Transform attackPosition;

    private AttackDetails attackDetails;

    private void Awake()
    {
        attackDetails.damageAmount = attackDamage;
        attackDetails.position = this.transform.position;
    }
    private void TriggerAttack()
    {
       
    }

}
