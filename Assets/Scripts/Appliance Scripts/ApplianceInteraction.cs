using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceInteraction : MonoBehaviour
{

    [Header("Sounds")]
    public AudioManager audioManager;
    public AudioClip sound;

    [Header("Gameplay Variables")]
    [SerializeField] private CookingAppliance chef;  // the chef shall decide the cooking functions :)
    public int shrimpCount = 3;             // the amount of shrimp that can go into the mixer
    private float cookTime;
    public bool isPlayerNear = false;       // To track if the player is near the mixer
    public bool currentlyCooking = false;   // tracks whether or not the appliance is busy cooking

    [Header("Appearance")]
    public Animator animator;              // Reference to the Animator
    public AnimationClip animationClip;
    public Renderer applianceRenderer;
    public Color cookingColor = Color.red; // Color while cooking
    public Color idleColor = Color.white;  // Color when idle

    [Header("Not Yet Used")]
    public bool isDoorOpen; // if you implement opening functionality
    
     private void Start()
    {
        // Initialize Animator and Renderer
        animator = GetComponent<Animator>();

        if (applianceRenderer == null)
        {
            applianceRenderer = GetComponent<Renderer>();
            if (applianceRenderer == null)
            {
                Debug.LogWarning($"No Renderer found on {gameObject.name}. Material changes will not work.");
            }
        }
         // Set initial material color
        if (applianceRenderer != null)
        {
            applianceRenderer.material.color = idleColor;
        }

        if (chef == null)
        {
            chef = GetComponent<CookingAppliance>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Invisible"))
        {
            isPlayerNear = true;
            Debug.Log($"Player near {gameObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Invisible"))
        {
            isPlayerNear = false;
            Debug.Log($"Player left {gameObject.name}");
        }
    }
    private IEnumerator Cooking()
    {
        cookTime = shrimpCount * chef.cookingTimePerShrimp * Time.deltaTime; //the time it takes for the shrimp to cook + the amt of shrimp
        yield return new WaitForSeconds(cookTime);
    }
    

    private void OnTriggerStay(Collider other)
    {
        // Ensure the player presses F and the appliance isn't already cooking
        if (other.CompareTag("Invisible") && Input.GetKeyDown(KeyCode.F) && !chef.isCooking)
        {
            Debug.Log($"Player interacted with {gameObject.name}");
            StartCoroutine(CookDaShrimp());
        }
    }

    private IEnumerator CookDaShrimp()
    {
        currentlyCooking = true;
        // Update the material color to show cooking state
        if (applianceRenderer != null)
        {
            applianceRenderer.material.color = cookingColor;
        }

        // Play cooking sound
        if (audioManager != null && sound != null)
        {
            audioManager.PlaySound(sound);
        }

        // Start cooking animation
        if (animator != null)
        {
            animator.SetBool("IsCooking", true);
        }

        // Start the cooking process
        chef.maxShrimpCapacity = shrimpCount;
        chef.shrimpInsertPoint = transform;
        chef.DepositShrimp();

        // Wait for the cooking time to complete
        float cookTime = shrimpCount * chef.cookingTimePerShrimp;
        yield return new WaitForSeconds(cookTime);

        // End cooking process
        if (animator != null)
        {
            animator.SetBool("IsCooking", false);
        }

        // Reset material color to idle state
        if (applianceRenderer != null)
        {
            applianceRenderer.material.color = idleColor;
        }

        Debug.Log($"Cooking completed for {gameObject.name}");
        currentlyCooking = false;
    }


    private IEnumerator OpenDoor()
    {
         if (animator != null && animator.HasState(0, Animator.StringToHash("OpenDoor")))
        {
            animator.SetBool("OpenDoor", true);
            isDoorOpen = true;

            // Wait for the door-opening animation to complete
            yield return new WaitForSeconds(GetAnimationClipLength("OpenDoor"));
            Debug.Log($"Door opened for {gameObject.name}");
        }
    }

    private IEnumerator CloseDoor()
    {
        if (animator != null && animator.HasState(0, Animator.StringToHash("CloseDoor")))
        {
            animator.SetBool("CloseDoor",true);
            isDoorOpen = false;

            // Wait for the door-closing animation to complete
            yield return new WaitForSeconds(GetAnimationClipLength("CloseDoor"));
            Debug.Log($"Door closed for {gameObject.name}");
        }
    }
    private float GetAnimationClipLength(string animationName)
    {
        if (animator == null) return 0f;

        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"Animation '{animationName}' not found on {gameObject.name}.");
        return 0f;
    }
}
