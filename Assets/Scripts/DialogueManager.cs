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
    /// <summary>
    /// references dialogue manager
    /// </summary>
    public static DialogueManager instance;

    /// <summary>
    /// ui elements
    /// </summary>
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    /// <summary>
    /// references FirstPersonController/PlayerInput script
    /// </summary>
    public FirstPersonController firstPersonController;
    public PlayerInput playerInput;

    /// <summary>
    /// references dialogue
    /// </summary>
    private Queue<string> sentences;

    /// <summary>
    /// bool to check if dialogue is displaying
    /// </summary>
    public bool isDisplayingDialogue = false;

    /// <summary>
    /// current npc
    /// </summary>
    private Interactable currentNPC;

    /// <summary>
    /// initializes dialogue manager
    /// </summary>
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

    /// <summary>
    /// logic to start dialgue sequence
    /// </summary>
    /// <param name="dialogueLines"></param>
    /// <param name="npc"></param>
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

    /// <summary>
    /// function to show dialogue
    /// </summary>
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

    /// <summary>
    /// function to end dialogue sequence
    /// </summary>
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

    /// <summary>
    /// bool to check if dialogue is showing
    /// </summary>
    /// <returns></returns>
    public bool IsDisplayingDialogue()
    {
        return isDisplayingDialogue;
    }

    /// <summary>
    /// checks if space is hit to progress dialogue
    /// </summary>
    private void Update()
    {
        if (isDisplayingDialogue && Input.GetKeyDown(KeyCode.Space)) // Assuming Space advances dialogue
        {
            DisplayNextSentence();
        }
    }
}
