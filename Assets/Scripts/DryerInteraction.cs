using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerInteraction : MonoBehaviour
{
    private Animator animator; // Reference to the Animator
    private bool isPlayerNear = false; // To track if the player is near the dryer
    private bool isDoorOpen = false; // To track the door's state
    private bool isPlayerInside = false; // To track if the player is inside
    private bool isDoorClosedWithPlayerInside = false; // To track if the dryer is ready to run

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlayerNear)
        {
            // Open or close the door when F is pressed
            if (Input.GetKeyDown(KeyCode.F) && !isDoorOpen)
            {
                OpenDoor();
            }
            else if (Input.GetKeyDown(KeyCode.F) && isDoorOpen && !isPlayerInside)
            {
                CloseDoor();
            }

            // Enter the dryer when the door is open
            if (isDoorOpen && Input.GetKeyDown(KeyCode.E)) // Press E to enter
            {
                EnterDryer();
            }

            // Turn on the dryer when F is pressed, and the player is inside with the door closed
            if (isDoorClosedWithPlayerInside && Input.GetKeyDown(KeyCode.F))
            {
                StartDryer();
            }
        }
    }

    private void OpenDoor()
    {
        animator.SetTrigger("OpenDoor"); // Trigger the door open animation
        isDoorOpen = true;
        Debug.Log("Dryer door opened.");
    }

    private void CloseDoor()
    {
        animator.SetTrigger("CloseDoor"); // Trigger the door close animation
        isDoorOpen = false;
        isDoorClosedWithPlayerInside = isPlayerInside; // If the player is inside, mark as ready to run
        Debug.Log("Dryer door closed.");
    }

    private void EnterDryer()
    {
        if (!isPlayerInside)
        {
            isPlayerInside = true;
            Debug.Log("Player entered the dryer.");
        }
    }

    private void StartDryer()
    {
        animator.SetTrigger("StartDryer"); // Trigger the dryer running animation
        Debug.Log("Dryer started.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the dryer.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            isPlayerInside = false; // Reset player inside state
            isDoorClosedWithPlayerInside = false; // Reset dryer ready state
            Debug.Log("Player left the dryer.");
        }
    }
}

