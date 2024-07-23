/*
 * Author: Muhammad Farhan
 * Date: 22/7/2024
 * Description: Finite State Machine for Quest NPCS
 */
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

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
    /// starts FSM
    /// </summary>
    private void Start()
    {
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
                nextState = "QuestAccepted"; // Player interacts without having the collectible
            }
        }
        else if (currentState == "QuestDone")
        {
            nextState = "QuestDone"; // Player interacts after completing the quest
        }
        SwitchState();
    }

    /// <summary>
    /// no quest coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator NoQuest()
    {
        Debug.Log("No quest available.");
        yield return null;
    }

    /// <summary>
    /// quest start coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator QuestStart()
    {
        Debug.Log("Collect the thing");
        yield return new WaitForEndOfFrame();
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
            Debug.Log("You don't have my thing");
            yield return null; // Stay in the same state
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
            Debug.Log("Thank you for collecting my thing");
            GameManager.instance.AddReputation(10); // Reward player with reputation
            reputationAwarded = true; // Ensure this reward is given only once
        }
        else
        {
            Debug.Log("Thank you again, but you already have your reward.");
        }
        yield return new WaitForEndOfFrame();
        nextState = "QuestDone"; // Stay in the QuestDone state
    }
}
