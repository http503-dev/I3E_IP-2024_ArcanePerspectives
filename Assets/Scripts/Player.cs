/*
 * Author: Muhammad Farhan
 * Date: 22/7/2024
 * Description: Script related the player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// references interactable from interactable class
    /// </summary>
    Interactable currentInteractable;

    /// <summary>
    /// reference to currently held object
    /// </summary>
    private Transform heldObject;


    /// <summary>
    /// to access player camera's transform position
    /// </summary>
    [SerializeField] Transform playerCamera;

    /// <summary>
    /// to set intearaction distance
    /// </summary>
    [SerializeField] float interactionDistance;

    /// <summary>
    /// throwing force
    /// </summary>
    [SerializeField] float throwForce = 20f;

    /// <summary>
    /// using raycast to detect interactable
    /// </summary>
    private void DetectInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactionDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.RemovePlayerInteractable(this);
                    }
                    currentInteractable = interactable;
                    currentInteractable.UpdatePlayerInteractable(this);
                }
            }
            else if (currentInteractable != null)
            {
                currentInteractable.RemovePlayerInteractable(this);
                currentInteractable = null;
            }
        }
        else if (currentInteractable != null)
        {
            currentInteractable.RemovePlayerInteractable(this);
            currentInteractable = null;
        }
    }

    /// <summary>
    /// to update interactable
    /// </summary>
    /// <param name="newInteractable"></param>
    public void UpdateInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
    }

    /// <summary>
    /// to draw raycast line
    /// </summary>
    void Update()
    {
        Debug.DrawLine(playerCamera.position, playerCamera.position + (playerCamera.forward * interactionDistance), Color.red);
        DetectInteractable();
    }

    /// <summary>
    /// for hitting 'e' to interact
    /// </summary>
    void OnInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
    }

    /// <summary>
    /// for picking up objects
    /// </summary>
    /// <param name="objectToHold"></param>
    public void PickUpObject(Transform objectToHold)
    {
        heldObject = objectToHold;
        objectToHold.GetComponent<Rigidbody>().isKinematic = true;
        objectToHold.position = playerCamera.position + playerCamera.forward * interactionDistance / 2;
        objectToHold.parent = playerCamera;
    }

    /// <summary>
    /// for throwing objects
    /// </summary>
    void OnThrow()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = playerCamera.forward * throwForce;
            heldObject.parent = null;
            heldObject = null;
        }
    }
}
