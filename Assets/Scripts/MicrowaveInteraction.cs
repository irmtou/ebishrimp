using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveInteraction : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component
    private bool isPlayerNear = false; // To track if the player is near the microwave
    private bool isDoorOpen = false; // To track the microwave door state

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the microwave
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F)) // Press F to interact
        {
            ToggleMicrowaveDoor();
        }
    }

    private void ToggleMicrowaveDoor()
    {
        if (isDoorOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        animator.SetTrigger("OvenOpen"); // Trigger the open animation
        isDoorOpen = true; // Update the state to open
        Debug.Log("Microwave door opened.");
    }

    private void CloseDoor()
    {
        animator.SetTrigger("OvenClose"); // Trigger the close animation
        isDoorOpen = false; // Update the state to closed
        Debug.Log("Microwave door closed.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player enters the interaction zone
        {
            isPlayerNear = true; // Player is now near the microwave
            Debug.Log("Player is near the microwave.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player exits the interaction zone
        {
            isPlayerNear = false; // Player is no longer near the microwave
            Debug.Log("Player left the microwave.");
        }
    }
}

