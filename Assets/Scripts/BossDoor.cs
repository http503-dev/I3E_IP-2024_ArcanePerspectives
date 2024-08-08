/*
 * Author: Muhammad Farhan
 * Date: 8/8/2024
 * Description: Script related to entering the boss room
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoor : Interactable
{
    public int requiredRep = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.GetReputation() >= requiredRep)
            {
                UIManager.instance.ShowInteractPrompt("No turning back now! Hit 'E' to open door");
            }
            else
            {
                UIManager.instance.ShowInteractPrompt("You need more reputation to enter the castle!");
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

    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        if (GameManager.instance.GetReputation() >= requiredRep)
        {
            SceneManager.LoadScene(2);
            UIManager.instance.HideInteractPrompt();
        }
    }
}
