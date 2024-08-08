/*
 * Author: Muhammad Farhan
 * Date: 5/8/2024
 * Description: Script related to the pause menu
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Bool to check if game is paused
    /// </summary>
    public static bool GameIsPaused = false;

    /// <summary>
    /// Reference to different UI panels, sliders and game manager
    /// </summary>
    public GameObject pauseMenuUI;
    public GameObject playerUI;
    public GameObject gameManager;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Update()
    {
        // Check for the Escape key press to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Initialize sliders with saved values
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        // Add listeners to handle slider value changes
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
    }

    /// <summary>
    /// Resumes the game from paused state
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        UIManager.instance.HideCongrats();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
    }

    /// <summary>
    /// function to restart game
    /// </summary>
    public void Restart()
    {
        // Reset the game state
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGameState();
        }

        Time.timeScale = 1f; // Resume game time
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
        SceneManager.LoadScene(1);
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
    }

    /// <summary>
    /// Loads the main menu
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Destroy(gameManager);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// to set music volume when slider value change
    /// </summary>
    /// <param name="value"></param>
    public void OnMusicSliderChanged(float value)
    {
        AudioManager.instance.SetMusicVolume(value);
    }

    /// <summary>
    /// to set sfx volume when slider value change
    /// </summary>
    /// <param name="value"></param>
    public void OnSFXSliderChanged(float value)
    {
        AudioManager.instance.SetSFXVolume(value);
    }
}
