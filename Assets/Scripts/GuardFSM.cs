/*
 * Author: Muhammad Farhan
 * Date: 25/7/2024
 * Description: Finite State Machine for Guards
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardFSM : MonoBehaviour
{
    [SerializeField] private AudioClip hurtAudio;
    /// <summary>
    /// strings to switch states
    /// </summary>
    string currentState;
    string nextState;

    /// <summary>
    /// references
    /// </summary>
    public Transform player;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float loseSightDistance = 15f;
    public int reputationThreshold = 100;
    public float attackCooldown = 1.5f;
    public NavMeshAgent agent;
    public Animator animator;

    /// <summary>
    /// starts FSM
    /// </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = "Idle";
        nextState = "Idle";

        SwitchState();
    }

    /// <summary>
    /// updates FSM
    /// </summary>
    private void Update()
    {
        if (currentState != nextState)
        {
            currentState = nextState;
            SwitchState();
        }
    }

    /// <summary>
    /// function to switch states
    /// </summary>
    void SwitchState()
    {
        StopAllCoroutines(); // Stop any existing state coroutine
        StartCoroutine(currentState);
    }

    /// <summary>
    /// function to handle guard death
    /// </summary>
    public void Die()
    {
        nextState = "Dead";
        animator.SetBool("isDead", true);
        StopAllCoroutines(); // Stop all ongoing coroutines
        StartCoroutine(Dead()); // Directly call the Dead coroutine
    }

    /// <summary>
    /// idle coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", false);

        while (true)
        {
            if (player != null && Vector3.Distance(transform.position, player.position) <= chaseDistance && GameManager.instance.reputation < reputationThreshold)
            {
                nextState = "Chase";
            }
            yield return null;
        }
    }

    /// <summary>
    /// chase coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator Chase()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", false);

        while (true)
        {
            if (player == null)
            {
                nextState = "Idle";
            }
            else if (Vector3.Distance(transform.position, player.position) > loseSightDistance)
            {
                nextState = "Idle";
            }
            else if (Vector3.Distance(transform.position, player.position) <= attackDistance)
            {
                nextState = "Attack";
            }
            else
            {
                agent.SetDestination(player.position);
            }
            yield return null;
        }
    }

    /// <summary>
    /// attack coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", true);
        animator.SetBool("isDead", false);

        while (true)
        {
            if (player == null)
            {
                nextState = "Idle";
            }
            if (Vector3.Distance(transform.position, player.position) > attackDistance)
            {
                nextState = "Chase";
            }
            else
            {
                // Implement attack logic here
                Debug.Log("Attacking the player");
                GameManager.instance.TakeDamage(20);

                if (hurtAudio != null)
                {
                    AudioManager.instance.PlaySFX(hurtAudio, transform.position);
                }

                yield return new WaitForSeconds(attackCooldown); // Add delay between attacks
            }
            yield return null;
        }
    }

    /// <summary>
    /// dead coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator Dead()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);

        Debug.Log("Guard is dead");
        agent.isStopped = true;
        yield return new WaitForSeconds(4f); // Wait for the death animation to play out
        Destroy(gameObject);
    }
}
