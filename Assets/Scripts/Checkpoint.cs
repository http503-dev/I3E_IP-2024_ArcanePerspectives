/*
 * Author: Muhammad Farhan
 * Date: 5/8/2024
 * Description: Script related to the checkpoint system
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : Interactable
{
    /// <summary>
    /// to set audio when checkpoint reached
    /// </summary>
    [SerializeField] private AudioClip collectAudio;

    /// <summary>
    /// sets checkpoint for player script and plays audio
    /// </summary>
    /// <param name="thePlayer"></param>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        if (collectAudio != null)
        {
            AudioManager.instance.PlaySFX(collectAudio, transform.position);
        }
        UIManager.instance.ShowInteractPrompt("Checkpoint set. Health restored!");
        Vector3 offsetPosition = transform.position + new Vector3(-2.0f, 0.0f, 0.0f);
        GameManager.SetCheckpoint(offsetPosition);
    }

    /// <summary>
    /// hides ui elements
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.HideInteractPrompt();
        }
    }
}
