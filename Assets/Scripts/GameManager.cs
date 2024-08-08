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
    /// to get checkpoint positions
    /// </summary>
    public Vector3 lastCheckpoint;
    public Vector3 initialSpawn;

    /// <summary>
    /// indicates player's reputation and whether they have obtained the collectible
    /// </summary>
    public int reputation;
    public bool hasCollectible = false;
    public bool hasPickaxe = false;
    public bool isScaledUp = false;
    public bool isDestroyed = false;
    public bool bossDefeated = false;

    /// <summary>
    /// player's health attributes
    /// </summary>
    public float maxHealth = 100f;
    private float currentHealth;

    /// <summary>
    /// ui stuff
    /// </summary>
    public TextMeshProUGUI repText;
    public Slider healthSlider;
    public GameObject deathScreenUI;
    public GameObject pauseMenuUI;
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
            Die();
        }
        UpdateHealthUI();
    }

    /// <summary>
    /// logic for when player dies
    /// </summary>
    void Die()
    {
        if (deathScreenUI != null)
        {
            pauseMenuUI.SetActive(false);
            playerUI.SetActive(false);
            deathScreenUI.SetActive(true);
        }
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (player != null)
        {
            player.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    /// <summary>
    /// logic for setting checkpoints
    /// </summary>
    /// <param name="checkpointPosition"></param>
    public static void SetCheckpoint(Vector3 checkpointPosition)
    {
        if (instance != null)
        {
            instance.lastCheckpoint = checkpointPosition;
            instance.currentHealth = instance.maxHealth;
            instance.UpdateHealthUI();
        }
    }

    /// <summary>
    /// logic for respawning player
    /// </summary>
    public void Respawn()
    {
        if (player != null)
        {
            if (lastCheckpoint != null)
            {
                player.transform.position = lastCheckpoint;
            }
            else
            {
                player.transform.position = initialSpawn;
            }

            currentHealth = maxHealth;
            reputation = 0;
            UpdateHealthUI();
            deathScreenUI.SetActive(false);
            playerUI.SetActive(true);
            Time.timeScale = 1f; // Resume the game
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor
            player.GetComponent<FirstPersonController>().enabled = true;
        }
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

    public void ResetGameState()
    {
        currentHealth = maxHealth;
        hasCollectible = false;
        hasPickaxe = false;
        isScaledUp = false;
        isDestroyed = false;
        bossDefeated = false;

        lastCheckpoint = initialSpawn;
        player.transform.position = initialSpawn;
        UpdateHealthUI();
    }

    /// <summary>
    /// function to determine if player has collectible
    /// </summary>
    /// <param name="collectibleValue"></param>
    public void SetHasCollectible(bool collectibleValue)
    {
        hasCollectible = collectibleValue;
    }


    /// <summary>
    /// function to return bool value of collectible
    /// </summary>
    /// <returns></returns>
    public bool HasCollectible()
    {
        return hasCollectible;
    }

    /// <summary>
    /// function to determine if player has pickaxe
    /// </summary>
    /// <param name="pickValue"></param>
    public void SetHasPickaxe(bool pickValue)
    {
        hasPickaxe = pickValue;
    }

    /// <summary>
    /// function to return bool value of pickaxe
    /// </summary>
    /// <returns></returns>
    public bool HasPickaxe()
    {
        return hasPickaxe;
    }

    /// <summary>
    /// function to determine if well has been scaled up
    /// </summary>
    /// <param name="scaledUpValue"></param>
    public void SetScaledUp(bool scaledUpValue)
    {
        isScaledUp = scaledUpValue;
        Debug.Log(scaledUpValue);
    }

    /// <summary>
    /// function to return bool value of well
    /// </summary>
    /// <returns></returns>
    public bool HasScaledUp()
    {
        return isScaledUp;
    }

    /// <summary>
    /// function to determine if shield has been destroyed
    /// </summary>
    /// <param name="destroyed"></param>
    public void SetDestroyed(bool destroyed)
    {
        isDestroyed = destroyed;
    }

    /// <summary>
    /// function to return bool value of shield
    /// </summary>
    /// <returns></returns>
    public bool IsDestroyed()
    {
        return isDestroyed;
    }

    public void BossDefeated()
    {
        bossDefeated = true;
    }

    public bool IsBossDefeated()
    {
        return bossDefeated;
    }

    /// <summary>
    /// function to gain reputation after completing quests
    /// </summary>
    /// <param name="amount"></param>
    public void AddReputation(int amount)
    {
        reputation += amount;
        repText.text = reputation.ToString();
        Debug.Log("Reputation increased by " + amount + ". Total reputation: " + reputation);
    }

    /// <summary>
    /// function to return reputation
    /// </summary>
    /// <returns></returns>
    public int GetReputation()
    {
        return reputation;
    }
}
