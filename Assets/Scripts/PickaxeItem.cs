/*
 * Author: Muhammad Farhan
 * Date: 7/8/2024
 * Description: Script related to the pickaxe collectible
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeItem : Interactable
{
    /// <summary>
    /// sound for picking up pickaxe
    /// </summary>
    [SerializeField]
    private AudioClip collectAudio;

    /// <summary>
    /// bool to determine whether collectible has been collected
    /// </summary>
    public bool hasPickaxe = false;

    /// <summary>
    /// function for what happens on interacting
    /// </summary>
    /// <param name="thePlayer"></param>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        hasPickaxe = true;
        GameManager.instance.SetHasPickaxe(hasPickaxe);
        Destroy(gameObject);
        if (collectAudio != null)
        {
            AudioManager.instance.PlaySFX(collectAudio, transform.position);
        }
    }
}
