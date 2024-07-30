using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public float attackDistance = 10f;
    public float moveSpeed = 2f;
    public GameObject projectilePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float attackCooldown = 2f;

    private float nextAttackTime;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        player = GameManager.instance.player.transform;
    }

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

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        nextAttackTime = Time.time + attackCooldown;
        ThrowProjectile();
    }

    private void ThrowProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - throwPoint.position).normalized;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }
}
