/*
 * Author: Muhammad Farhan
 * Date: 25/7/2024
 * Description: Script for guard health
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHealth : MonoBehaviour
{
    /// <summary>
    /// health and damage it does
    /// </summary>
    public int health = 100;
    public int damage = 25;
    private bool isDead = false;

    /// <summary>
    /// checks if it can die
    /// </summary>
    void Update()
    {
        if (health <= 0 && !isDead)
        {
            GetComponent<GuardFSM>().Die();
        }
    }

    /// <summary>
    /// function for it to take damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    /// <summary>
    /// if it gets hit by throwable objects it dies
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            Die();
        }
    }

    /// <summary>
    /// logic to handle death
    /// </summary>
    private void Die()
    {
        isDead = true; // Set the flag to true to prevent multiple calls
        GetComponent<GuardFSM>().Die();
    }
}
