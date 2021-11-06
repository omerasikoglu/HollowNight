using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance
    {
        get { if (instance == null)
            {
                instance = Resources.Load<GameAssets>("pfGameAssets");
            }
            return instance;
        }
    }

    [Header("Enemies")]
    public Transform pfEnemyBirdie;

    [Header("Projectiles")]
    public Transform pfProjectileGold;

    [Header("Pop-ups")]
    public Transform pfDamagePopup;

}
