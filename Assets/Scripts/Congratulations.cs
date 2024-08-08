using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congratulations : Interactable
{
    /// <summary>
    /// to set audio when escaping
    /// </summary>
    [SerializeField] private AudioClip escapeAudio;

    /// <summary>
    /// plays audio and shows congratulatory message
    /// </summary>
    /// <param name="thePlayer"></param>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        if (escapeAudio != null)
        {
            AudioManager.instance.PlaySFX(escapeAudio, transform.position);
        }
        UIManager.instance.ShowCongrats();

    }

}
