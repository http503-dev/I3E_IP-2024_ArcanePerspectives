/*
 * Author: Jarene Goh
 * Date: 8/8/2024
 * Description: Script to track if well has been scaled up
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellItem : MonoBehaviour
{
    /// <summary>
    /// references objects and it's scale
    /// </summary>
    public Transform objectToScale; // Reference to the object you want to track
    public Vector3 originalScale;   // Store the original scale of the object
    public Vector3 targetScale;     // The target scale that indicates the object has been scaled up

    /// <summary>
    /// Flag to check if the object has been scaled up
    /// </summary>
    public bool isScaledUp = false;

    /// <summary>
    /// saves original scale on start
    /// </summary>
    void Start()
    {
        if (objectToScale != null)
        {
            originalScale = objectToScale.localScale; // Store the original scale
        }
    }

    /// <summary>
    /// update to check if scaled up
    /// </summary>
    void Update()
    {
        if (objectToScale != null && !isScaledUp)
        {
            CheckIfScaledUp();
        }
    }

    /// <summary>
    /// function to tell game manager the object has been scaled
    /// </summary>
    void CheckIfScaledUp()
    {
        if (objectToScale.localScale.x > originalScale.x ||
            objectToScale.localScale.y > originalScale.y ||
            objectToScale.localScale.z > originalScale.z)
        {
            isScaledUp = true;
            GameManager.instance.SetScaledUp(isScaledUp);
        }
    }
}
