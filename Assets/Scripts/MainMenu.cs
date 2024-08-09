/*
 * Author: Johnathan wang
 * Date: 23/7/2024
 * Description: Script related the Main Menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// references sliders for setting volume levels
    /// </summary>
    public Slider musicSlider;
    public Slider sfxSlider;

    /// <summary>
    /// Function to start game/go to main scene
    /// </summary>
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    /// <summary>
    /// Function to quit the application
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// starts volume settings
    /// </summary>
    private void Start()
    {
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
