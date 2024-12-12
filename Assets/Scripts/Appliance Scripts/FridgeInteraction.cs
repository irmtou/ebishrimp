using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeInteraction : MonoBehaviour
{
    public Animator animator; // Reference to the Animator
    private bool isPlayerNear = false; // Track if the player is close to the fridge

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component on the fridge
    }

    void Update()
    {
        // Check if the player is near and presses the interaction key (F)
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            OpenDoorRight();
        }
    }

    private void OpenDoorRight()
    {
        animator.SetTrigger("OpenDoorRight"); // Trigger the animation
    }

    // Detect if the player is near the fridge (requires a trigger collider)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            isPlayerNear = true;
            Debug.Log("Player is near the fridge.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the fridge.");
        }
    }
}
