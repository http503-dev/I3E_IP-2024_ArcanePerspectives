/*
 * Author: Muhammad Farhan
 * Date: 30/7/2024
 * Description: Script related to the boss's AI
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    /// <summary>
    /// references for boss 'stats' and where/what he throws
    /// </summary>
    public float attackDistance = 20f;
    public float moveSpeed = 1.5f;
    public float throwForce = 20f;
    public float attackCooldown = 7f;
    public GameObject projectilePrefab;
    public Transform throwPoint;

    private float nextAttackTime;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Animator animator;

    /// <summary>
    /// references boss health/ ui elements
    /// </summary>
    public float maxHealth = 100;
    private float currentHealth;
    public Slider healthBar;
    public GameObject healthBarUI;
    public bool isDead = false;
    private bool isHurt = false;

    /// <summary>
    /// initialize nav mesh 
    /// </summary>
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        player = GameManager.instance.player.transform;
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBarUI.SetActive(true);
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

        if (isDead || isHurt)
        {
            return; // Do nothing if the boss is hurt or dead
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
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    /// <summary>
    /// logic for attacking
    /// </summary>
    private void AttackPlayer()
    {
        nextAttackTime = Time.time + attackCooldown;
        animator.SetBool("isAttacking", true);
        animator.SetBool("isWalking", false);
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        navMeshAgent.isStopped = true; // Stop moving when attacking

        // Wait for the throw animation to reach the throwing point
        yield return new WaitForSeconds(1.8f); // Adjust based on your animation timing

        ThrowProjectile();

        // Wait for the rest of the throw animation to complete
        yield return new WaitForSeconds(2.8f); // Adjust based on your animation timing

        if (!isDead && !isHurt) // Only resume moving if not dead or hurt
        {
            navMeshAgent.isStopped = false; // Resume moving after attacking
        }
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

    /// <summary>
    /// logic for boss to take damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth > 0)
        {
            StopAllCoroutines(); // Stop any ongoing actions (e.g., attack)
            isHurt = true;
            animator.SetBool("isHurt", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
            navMeshAgent.isStopped = true;
            StartCoroutine(ResumeAfterHurt());
        }

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
        isDead = true;
        animator.SetBool("isDead", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isHurt", false);
        healthBarUI.SetActive(false);
        navMeshAgent.isStopped = true;
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator ResumeAfterHurt()
    {
        yield return new WaitForSeconds(1.3f); // Adjust based on your hurt animation length
        if (!isDead) // Only resume if the boss is still alive
        {
            navMeshAgent.isStopped = false;
            isHurt = false;
            animator.SetBool("isHurt", false); // Transition back from hurt state
        }
    }

    /// <summary>
    /// Coroutine to wait for death animation to complete
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
