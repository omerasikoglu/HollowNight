using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;
    public static GameAssets Instance
    {
        get { if (instance == null)
            {
                instance = Resources.Load<GameAssets>("GameAssets");
            }
            return instance;
        }
    }

    [Header("Enemies")]
    public Transform pfEnemyBirdie;

    [Header("Projectiles")]
    public Transform pfProjectileGold;


}
