/*
 * Author: Muhammad Farhan
 * Date: 22/7/2024
 * Description: Script related to interacting with stuff
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    /// <summary>
    /// updating what the player is looking at
    /// </summary>
    /// <param name="thePlayer"></param>
    public void UpdatePlayerInteractable(Player thePlayer)
    {
        thePlayer.UpdateInteractable(this);
    }

    /// <summary>
    /// removing the interactable if not looking at it
    /// </summary>
    /// <param name="thePlayer"></param>
    public void RemovePlayerInteractable(Player thePlayer)
    {
        thePlayer.UpdateInteractable(null);
    }

    /// <summary>
    /// function for interacting with collectible
    /// </summary>
    /// <param name="thePlayer"></param>
    public virtual void Interact(Player thePlayer)
    {
        Debug.Log(gameObject.name + " was interacted with");
    }
}
