/*
 * Author: Johnathan wang
 * Date: 8/8/2024
 * Description: Script to check if shield has been destroyed
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    /// <summary>
    /// bool to check if shield has been destroyed
    /// </summary>
    public bool isDestroyed = false;

    /// <summary>
    /// logic for when it collides with throwable item
    /// </summary>
    /// <param name="collision"></param>

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            isDestroyed = true;
            GameManager.instance.SetDestroyed(isDestroyed);
            Destroy(gameObject);
        }
    }
}
