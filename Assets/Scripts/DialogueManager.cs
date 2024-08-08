/*
 * Author: Jarene Goh
 * Date: 30/7/2024
 * Description: Script related to the Dialogue Manager
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    public FirstPersonController firstPersonController; // Reference to the FirstPersonController script
    public PlayerInput playerInput; // Reference to the PlayerInput script

    private Queue<string> sentences;
    public bool isDisplayingDialogue = false;

    private Interactable currentNPC;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        sentences = new Queue<string>();
    }

    public void StartDialogue(string[] dialogueLines, Interactable npc)
    {
        Debug.Log("Starting Dialogue");
        sentences.Clear();
        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }
        if (firstPersonController != null)
        {
            firstPersonController.enabled = false;
        }
        if (playerInput != null)
        {
            playerInput.enabled = false;
        }

        currentNPC = npc;
        if (npc is NPCQuest)
        {
            ((NPCQuest)npc).ForceIdleState(true);
        }
        else if (npc is JesterQuest)
        {
            ((JesterQuest)npc).ForceIdleState(true);
        }
        else if (npc is FarmerMaleQuest)
        {
            ((FarmerMaleQuest)npc).ForceIdleState(true);
        }
        else if (npc is FarmerFemaleQuest)
        {
            ((FarmerFemaleQuest)npc).ForceIdleState(true);
        }
        else if (npc is KnightQuest)
        {
            ((KnightQuest)npc).ForceIdleState(true);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        dialogueBox.SetActive(true);
        isDisplayingDialogue = true;
        Debug.Log("Displaying sentence: " + sentence);
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = null;
        isDisplayingDialogue = false;
        if (firstPersonController != null)
        {
            firstPersonController.enabled = true;
        }
        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
        Debug.Log("Ending Dialogue");

        if (currentNPC != null)
        {
            if (currentNPC is NPCQuest)
            {
                ((NPCQuest)currentNPC).ForceIdleState(false);
            }
            else if (currentNPC is JesterQuest)
            {
                ((JesterQuest)currentNPC).ForceIdleState(false);
            }
            else if (currentNPC is FarmerMaleQuest)
            {
                ((FarmerMaleQuest)currentNPC).ForceIdleState(false);
            }
            else if (currentNPC is FarmerFemaleQuest)
            {
                ((FarmerFemaleQuest)currentNPC).ForceIdleState(false);
            }
            else if (currentNPC is KnightQuest)
            {
                ((KnightQuest)currentNPC).ForceIdleState(false);
            }
            currentNPC = null;
        }
    }

    public bool IsDisplayingDialogue()
    {
        return isDisplayingDialogue;
    }

    private void Update()
    {
        if (isDisplayingDialogue && Input.GetKeyDown(KeyCode.Space)) // Assuming Space advances dialogue
        {
            DisplayNextSentence();
        }
    }
}
