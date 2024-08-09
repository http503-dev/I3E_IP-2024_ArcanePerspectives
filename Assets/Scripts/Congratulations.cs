/*
 * Author: Muhammad Farhan
 * Date: 8/8/2024
 * Description: Script related to beating the game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congratulations : Interactable
{
    /// <summary>
    /// shows congratulatory message
    /// </summary>
    /// <param name="thePlayer"></param>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        PauseMenu.DisablePauseMenu();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.instance.ShowCongrats();
        UIManager.instance.HideInteractPrompt();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.IsBossDefeated() == true)
            {
                UIManager.instance.ShowInteractPrompt("Exit the castle!");
            }
            else
            {
                UIManager.instance.ShowInteractPrompt("No turning back now!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.HideInteractPrompt();
        }
    }



}
