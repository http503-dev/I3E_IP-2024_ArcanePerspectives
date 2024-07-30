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

    public string[] noQuestDialogue;
    public string[] questStartDialogue;
    public string[] questAcceptedDialogue;
    public string[] questDoneDialogue;

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
        }
        else
        {
            DialogueManager.instance.StartDialogue(new string[] { "Thank you again, but you already have your reward." });
            yield return new WaitUntil(() => !DialogueManager.instance.IsDisplayingDialogue());
        }
        nextState = "QuestDone";
    }
}
