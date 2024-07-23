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

}
