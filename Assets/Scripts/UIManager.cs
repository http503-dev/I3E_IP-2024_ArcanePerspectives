/*
 * Author: Jarene Goh
 * Date: 30/7/2024
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
    public GameObject congrats;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            interactBackground.SetActive(false);
            interactPrompt.text = null;
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
    /// function to show congrats message
    /// </summary>
    public void ShowCongrats()
    {
        congrats.SetActive(true);
    }

    public void HideCongrats()
    {
        congrats.SetActive(false);
    }
}
