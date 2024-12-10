using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFryerInteraction : MonoBehaviour
{
    private Animator animator; // Reference to the Animator
    public bool isPlayerNear = false; // To track if the player is near the air fryer
    private bool isTrayOpen = false; // To track the air fryer tray state
    private bool isTrayClosed = true; // To ensure the tray is closed before running
    private bool isPlayerInside = false; // To track if the player is inside the air fryer
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlayerNear)
        {
            // Open the tray when F is pressed and it's closed
            if (Input.GetKeyDown(KeyCode.F) && !isTrayOpen) 
            {
                OpenTray();
            }
            // Close the tray when F is pressed and it's open
            else if (Input.GetKeyDown(KeyCode.F) && isTrayOpen)
            {
                CloseTray();
            }

            // Turn on air fryer only if the tray is closed and the player is inside
            if (Input.GetKeyDown(KeyCode.E) && isTrayClosed && isPlayerInside)
            {
                StartAirFryer();
            }
        }
    }

    private void OpenTray()
    {
        animator.SetTrigger("OpenTray"); // Trigger the animation to open the tray
        isTrayOpen = true; // Mark the tray as open
        isTrayClosed = false; // Mark the tray as not closed
        Debug.Log("Air fryer tray opened.");
    }

    private void CloseTray()
    {
        animator.SetTrigger("CloseTray"); // Trigger the animation to close the tray
        isTrayOpen = false; // Mark the tray as closed
        isTrayClosed = true; // Mark the tray as closed
        Debug.Log("Air fryer tray closed.");
    }

    private void StartAirFryer()
    {
        if (isTrayClosed && isPlayerInside)
        {
            Debug.Log("Air fryer started with player inside.");
            // Add logic for affecting health, gameplay, or other effects here
        }
        else
        {
            Debug.Log("Cannot start air fryer. Tray must be closed, and the player must be inside.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the air fryer.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the air fryer.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isTrayOpen && Input.GetKeyDown(KeyCode.G)) // Example key for entering the air fryer
        {
            isPlayerInside = true;
            Debug.Log("Player is inside the air fryer.");
        }
    }
}
