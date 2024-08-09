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
    /// <summary>
    /// does damage to boss or player on collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        float scale = transform.localScale.magnitude; // Use magnitude to get a single value from the scale vector
        float calculatedDamage = scale * 5f;

        BossAI bossHealth = collision.gameObject.GetComponent<BossAI>();
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(calculatedDamage);
            Destroy(gameObject); // Destroy the projectile after it hits the boss
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(calculatedDamage);
            Destroy(gameObject); // Destroy the projectile after it hits the player
        }
        else
        {
            Destroy(gameObject, 5f); // Destroy the projectile after 5 seconds if it hits something else
        }
    }
}
