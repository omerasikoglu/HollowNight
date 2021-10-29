using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        CheckCollision(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        CheckCollision(other.gameObject);
    }

    private void CheckCollision(GameObject collider)
    {
        //if (collider.CompareTag("Player"))
        //{
        //    var healthSystem = PlayerController.Instance;
        //    if (!healthSystem.CanBeHit) return;

        //    var recoilDirection = (collider.transform.position - transform.position).normalized;
        //    float multiplier = recoilDirection.y < 0 ? 1.0f : 500.0f;
        //    Vector2 recoilForce = recoilDirection * multiplier;

        //    healthSystem.Hurt(damage, recoilForce, killRecoil: false);
        //}
        //}
    }
}
