/*
 * Author: Muhammad Farhan
 * Date: 22/7/2024
 * Description: Finite State Machine for Quest NPCS
 */
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;

public class NPCQuest : Interactable
{
    /// <summary>
    /// strings to switch states
    /// </summary>
    string currentState;
    string nextState;

    /// <summary>
    /// flag to check if rep has been awarded
    /// </summary>
    bool reputationAwarded = false;

    /// <summary>
    /// dialogue for different states
    /// </summary>
    public string[] noQuestDialogue;
    public string[] questStartDialogue;
    public string[] questAcceptedDialogue;
    public string[] questDoneDialogue;

    /// <summary>
    /// waypoints for different states
    /// </summary>
    public Transform noQuestAreaMin;
    public Transform noQuestAreaMax;
    public Transform questDoneAreaMin;
    public Transform questDoneAreaMax;

    /// <summary>
    /// references for nav mesh
    /// </summary>
    private NavMeshAgent agent;
    private float moveInterval = 10f; // Interval for moving to a new point
    public Animator animator;

    /// <summary>
    /// starts FSM
    /// </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = "NoQuest";
        nextState = "NoQuest";

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

        // Check if the NPC is moving and update the animator
        if (agent.velocity.sqrMagnitude > 0)
        {
            animator.SetBool("isNPCIdle", false);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isNPCIdle", true);
            animator.SetBool("isWalking", false);
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
    /// function for interacting with NPCs
    /// </summary>
    /// <param name="thePlayer"></param>
    public override void Interact(Player thePlayer)
    {
        if (DialogueManager.instance.IsDisplayingDialogue())
        {

            return;
        }

        if (currentState == "NoQuest")
        {
            nextState = "QuestStart";
        }
        else if (currentState == "QuestAccepted")
        {
            if (GameManager.instance.HasCollectible())
            {
                nextState = "QuestDone";
            }
            else
            {
                nextState = "QuestAccepted";
            }
        }
        else if (currentState == "QuestDone")
        {
            nextState = "QuestDone";
        }

        SwitchState();
    }

    /// <summary>
    /// no quest coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator NoQuest()
    {
        DialogueManager.instance.StartDialogue(noQuestDialogue);
        yield return new WaitUntil(() => !DialogueManager.instance.IsDisplayingDialogue());
        while (currentState == "NoQuest")
        {
            MoveToRandomPoint(noQuestAreaMin, noQuestAreaMax);
            yield return new WaitForSeconds(moveInterval);
        }
    }

    /// <summary>
    /// quest start coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator QuestStart()
    {
        Debug.Log("Collect the thing");
        DialogueManager.instance.StartDialogue(questStartDialogue);
        yield return new WaitUntil(() => !DialogueManager.instance.IsDisplayingDialogue());
        nextState = "QuestAccepted";
    }

    /// <summary>
    /// quest accepted coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator QuestAccepted()
    {
        if (GameManager.instance.HasCollectible())
        {
            nextState = "QuestDone";
        }
        else
        {
            DialogueManager.instance.StartDialogue(questAcceptedDialogue);
            yield return new WaitUntil(() => !DialogueManager.instance.IsDisplayingDialogue());
        }
    }

    /// <summary>
    /// quest done coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator QuestDone()
    {
        if (!reputationAwarded)
        {
            DialogueManager.instance.StartDialogue(questDoneDialogue);
            yield return new WaitUntil(() => !DialogueManager.instance.IsDisplayingDialogue());
            GameManager.instance.AddReputation(10);
            reputationAwarded = true;
            MoveToRandomPoint(questDoneAreaMin, questDoneAreaMax);
        }
        else
        {
            DialogueManager.instance.StartDialogue(new string[] { "Thank you again, but you already have your reward." });
            yield return new WaitUntil(() => !DialogueManager.instance.IsDisplayingDialogue());
        }
        while (currentState == "QuestDone")
        {
            MoveToRandomPoint(questDoneAreaMin, questDoneAreaMax);
            yield return new WaitForSeconds(moveInterval);
        }
        nextState = "QuestDone";
    }

    /// <summary>
    /// Move the NPC to a random point within the specified area
    /// </summary>
    /// <param name="areaMin"></param>
    /// <param name="areaMax"></param>
    private void MoveToRandomPoint(Transform areaMin, Transform areaMax)
    {
        Vector3 randomPoint = new Vector3(Random.Range(areaMin.position.x, areaMax.position.x), transform.position.y, // Maintain the current Y position
        Random.Range(areaMin.position.z, areaMax.position.z));

        agent.SetDestination(randomPoint);
    }
}
