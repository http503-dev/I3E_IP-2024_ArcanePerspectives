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
    public int health = 100;
    public int damage = 25;
    private bool isDead = false; // Flag to ensure reputation reduction happens only once


    void Update()
    {
        if (health <= 0 && !isDead)
        {
            GetComponent<GuardFSM>().Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true; // Set the flag to true to prevent multiple calls
        GetComponent<GuardFSM>().Die();
    }
}
