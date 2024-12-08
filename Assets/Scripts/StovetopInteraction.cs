using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StovetopInteraction : MonoBehaviour
{
    private bool isStovetopOn = false; // To track if the stovetop is turned on
    public float damagePerSecond = 10f; // Amount of health to decrease per second

    void Update()
    {
        // Toggle stovetop on/off when the player presses a key (e.g., F)
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleStovetop();
        }
    }

    private void ToggleStovetop()
    {
        isStovetopOn = !isStovetopOn; // Toggle the state
        Debug.Log($"Stovetop is now {(isStovetopOn ? "ON" : "OFF")}");
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the collided object has a PlayerHealth component
        if (isStovetopOn && other.CompareTag("Player"))
        {
            //layerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
                //playerHealth.TakeDamage(damagePerSecond * Time.deltaTime); // Decrease health over time
            //}
        }
    }
}

