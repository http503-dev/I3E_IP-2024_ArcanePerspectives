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
    /// plays audio and shows congratulatory message
    /// </summary>
    /// <param name="thePlayer"></param>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        UIManager.instance.ShowCongrats();
    }

}
