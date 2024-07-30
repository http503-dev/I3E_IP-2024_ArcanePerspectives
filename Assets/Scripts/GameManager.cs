/*
 * Author: Muhammad Farhan
 * Date: 23/7/2024
 * Description: Script related to the Game Manager
 */
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// references the game manager
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// references the player
    /// </summary>
    public GameObject player;

    /// <summary>
    /// indicates player's reputation and whether they have obtained the collectible
    /// </summary>
    public int reputation;
    public bool hasCollectible = false;

    /// <summary>
    /// player's health attributes
    /// </summary>
    public float maxHealth = 100f;
    private float currentHealth;

    /// <summary>
    /// ui stuff
    /// </summary>
    public TextMeshProUGUI repText;
    public TextMeshProUGUI inventoryText;
    public Slider healthSlider;
    public GameObject playerUI;

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

        DontDestroyOnLoad(gameObject);
        currentHealth = maxHealth; // Initialize health here
    }

    /// <summary>
    /// to initialize player and load scene
    /// </summary>
    private void Start()
    {
        InitializePlayer();
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene loaded event
    }

    /// <summary>
    /// initialize player and ui after changing scenes
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializePlayer();
        UpdateHealthUI();
    }

    /// <summary>
    /// logic for initializing player
    /// </summary>
    private void InitializePlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            return;
        }

        // Reapply the current health to the player
        UpdateHealthUI();
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    /// <summary>
    /// logic to update healthbar
    /// </summary>
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    /// <summary>
    /// logic for taking damage
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
    }

    /// <summary>
    /// Logic to load main menu
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    /// <summary>
    /// function to determine if player has pickaxe
    /// </summary>
    /// <param name="collectibleValue"></param>
    public void SetHasCollectible(bool collectibleValue)
    {
        hasCollectible = collectibleValue;
        inventoryText.text += "Collectible";
    }

    /// <summary>
    /// function to return bool value of crystal
    /// </summary>
    /// <returns></returns>
    public bool HasCollectible()
    {
        return hasCollectible;
    }

    /// <summary>
    /// function to gain reputation after completing quests
    /// </summary>
    /// <param name="amount"></param>
    public void AddReputation(int amount)
    {
        reputation += amount;
        repText.text += reputation;
        Debug.Log("Reputation increased by " + amount + ". Total reputation: " + reputation);
    }

    /// <summary>
    /// function to reduce reputation
    /// </summary>
    /// <param name="amount"></param>
    public void ReduceReputation(int amount)
    {
        reputation -= amount;
        repText.text = "Reputation: " + reputation; // Update text properly
        Debug.Log("Reputation decreased by " + amount + ". Total reputation: " + reputation);
    }
}
