using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherInteraction : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component
    public bool isPlayerNear = false; // To track if the player is near the washer
    private bool isDoorOpen = false; // To track the washer door state
    [SerializeField] private GameObject promptMessage; // the press e to interact message that pops up
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F)) // Press F to interact
        {
            ToggleWasherDoor();
        }
    }

    private void ToggleWasherDoor()
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
        animator.SetTrigger("washeropen"); // Trigger the open animation
        isDoorOpen = true; // Update the state to open
        Debug.Log("Washer door opened.");
    }

    private void CloseDoor()
    {
        animator.SetTrigger("washerclose"); // Trigger the close animation
        isDoorOpen = false; // Update the state to closed
        Debug.Log("Washer door closed.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player enters the interaction zone
        {
            isPlayerNear = true; // Player is now near the washer
            Debug.Log("Player is near the washer.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player exits the interaction zone
        {
            isPlayerNear = false; // Player is no longer near the washer
            Debug.Log("Player left the washer.");
        }
    }
}

