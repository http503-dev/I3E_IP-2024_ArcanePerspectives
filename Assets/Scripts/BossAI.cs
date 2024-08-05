/*
 * Author: Muhammad Farhan
 * Date: 30/7/2024
 * Description: Script related to the boss's AI
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    /// <summary>
    /// references for boss 'stats' and where/what he throws
    /// </summary>
    public float attackDistance = 20f;
    public float moveSpeed = 2f;
    public float throwForce = 20f;
    public float attackCooldown = 4f;
    public GameObject projectilePrefab;
    public Transform throwPoint;

    private float nextAttackTime;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    /// <summary>
    /// initialize nav mesh 
    /// </summary>
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        player = GameManager.instance.player.transform;
    }

    /// <summary>
    /// function to update whether to chase or attack player
    /// </summary>
    private void Update()
    {
        if (player == null)
        {
            player = GameManager.instance.player.transform;
            if (player == null)
            {
                return; // Player is not yet initialized
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    /// <summary>
    /// logic for chasing
    /// </summary>
    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    /// <summary>
    /// logic for attacking
    /// </summary>
    private void AttackPlayer()
    {
        nextAttackTime = Time.time + attackCooldown;
        ThrowProjectile();
    }

    /// <summary>
    /// logic for throwing projectile
    /// </summary>
    private void ThrowProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - throwPoint.position).normalized;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }
}
