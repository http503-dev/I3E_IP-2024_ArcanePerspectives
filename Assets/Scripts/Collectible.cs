/*
 * Author: Muhammad Farhan
 * Date: 22/7/2024
 * Description: Script related to the collectible
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{
    /// <summary>
    /// sound for picking up pickaxe
    /// </summary>
    [SerializeField]
    private AudioClip collectAudio;

    /// <summary>
    /// bool to determine whether collectible has been collected
    /// </summary>
    public bool hasCollectible = false;

    /// <summary>
    /// function for what happens on intereacting
    /// </summary>
    /// <param name="thePlayer"></param>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        hasCollectible = true;
        GameManager.instance.SetHasCollectible(hasCollectible);
        Destroy(gameObject);
        if (collectAudio != null)
        {
            AudioManager.instance.PlaySFX(collectAudio, transform.position);
        }
    }
}
