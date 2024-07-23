using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : Interactable
{
    [TextArea]
    public string dialogueText; // The text to display for the dialogue

    public int reputationReward = 10; // Reputation reward for completing the quest

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found. Make sure it exists in the scene.");
        }
    }


    public override void Interact(Player thePlayer)
    {
        bool hasCollectible = GameManager.instance.hasCollectible;
        base.Interact(thePlayer);
        if (hasCollectible == false)
        {
            StartQuest();
        }
        else if (hasCollectible == true)
        {
            RewardPlayer();
        }
        else
        {
            DisplayDialogue();
        }
    }

    void StartQuest()
    {
        Debug.Log(dialogueText);
        Debug.Log("Quest Started!");
    }

    void RewardPlayer()
    {
        if (gameManager != null)
        {
            gameManager.AddReputation(reputationReward);
            Debug.Log("Player rewarded with reputation!");
        }
    }

    void DisplayDialogue()
    {
        Debug.Log("You need to get me my thing!");
        
    }
}
