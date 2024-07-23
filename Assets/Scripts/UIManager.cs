/*
 * Author: Muhammad Farhan
 * Date: 23/7/2024
 * Description: Script related to the UI Manager
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// references ui manager
    /// </summary>
    public static UIManager instance;

    /// <summary>
    /// references ui elements
    /// </summary>
    public TextMeshProUGUI interactPrompt;
    public GameObject interactBackground;
    public TextMeshProUGUI warningPrompt;
    public GameObject warningBackground;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// function to show interct prompts
    /// </summary>
    /// <param name="message"></param>
    public void ShowInteractPrompt(string message)
    {
        interactPrompt.text = message;
        interactBackground.SetActive(true);
    }

    /// <summary>
    /// function to hide interact prompts
    /// </summary>
    public void HideInteractPrompt()
    {
        interactPrompt.text = null;
        interactBackground.SetActive(false);
    }

    /// <summary>
    /// function to show warning prompts
    /// </summary>
    /// <param name="message"></param>
    public void ShowWarningPrompt(string message)
    {
        warningPrompt.text = message;
        warningBackground.SetActive(true);
    }

    /// <summary>
    /// function to hide warning prompts
    /// </summary>
    public void HideWarningPrompt()
    {
        warningPrompt.text = null;
        warningBackground.SetActive(false);
    }

}
