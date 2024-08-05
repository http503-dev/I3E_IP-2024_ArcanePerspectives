/*
 * Author: Muhammad Farhan
 * Date: 30/7/2024
 * Description: Script related to the boss's health
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    /// <summary>
    /// references boss health/ ui elements
    /// </summary>
    public float maxHealth = 100;
    private float currentHealth;
    public Slider healthBar;
    public GameObject healthBarUI;

    /// <summary>
    /// initialize boss health stuff
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBarUI.SetActive(true);
    }

    /// <summary>
    /// logic for boss to take damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// function for him to die
    /// </summary>
    private void Die()
    {
        // Add death logic here
        healthBarUI.SetActive(false);
        Destroy(gameObject);
    }
}
