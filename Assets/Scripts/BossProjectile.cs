/*
 * Author: Muhammad Farhan
 * Date: 5/8/2024
 * Description: Script related to the boss's projectile
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float damage = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile after it hits the boss
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile after it hits the player
        }
        else
        {
            Destroy(gameObject, 5f); // Destroy the projectile after 5 seconds if it hits something else
        }
    }
}
