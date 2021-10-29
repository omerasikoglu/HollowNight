using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{


    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            bool isCritical = Random.Range(0, 100) > 70;
            DamagePopup.Create(UtilsClass.GetMouseWorldPosition(), 2990, isCritical);

        }
    }

}
