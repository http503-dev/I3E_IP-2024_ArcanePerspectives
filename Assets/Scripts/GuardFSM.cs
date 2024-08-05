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
    public float chaseDistance = 6f;
    public float attackDistance = 2f;
    public float loseSightDistance = 10f;
    public int reputationThreshold = 10;
    public NavMeshAgent agent;
    public float attackCooldown = 1.5f;

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
        SwitchState();
    }

    /// <summary>
    /// idle coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
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
        Debug.Log("Guard is dead");
        agent.isStopped = true;
        yield return null;
    }
}
