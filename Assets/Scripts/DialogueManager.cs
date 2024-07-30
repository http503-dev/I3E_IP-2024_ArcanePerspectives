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

    public void StartDialogue(string[] dialogueLines)
    {
        Debug.Log("Starting Dialogue");
        sentences.Clear();
        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }
        if (firstPersonController != null)
        {
            firstPersonController.enabled = false; // Disable player movement
        }
        if (playerInput != null)
        {
            playerInput.enabled = false; // Disable player input
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
            firstPersonController.enabled = true; // Re-enable player movement
        }
        if (playerInput != null)
        {
            playerInput.enabled = true; // Re-enable player input
        }
        Debug.Log("Ending Dialogue");
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
