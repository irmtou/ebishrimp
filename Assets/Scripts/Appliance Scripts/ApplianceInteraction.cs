using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceInteraction : MonoBehaviour
{
    public Animator animator;              // Reference to the Animator    
    public int shrimpCount = 3;             // the amount of shrimp that can go into the mixer
    [SerializeField] private CookingAppliance chef;  // the chef shall decide the cooking functions :)
    public bool isPlayerNear = false;       // To track if the player is near the mixer
    public bool currentlyCooking = false;   // tracks whether or not the appliance is busy cooking
    public AnimationClip animationClip;

    void Start()
    {
        animator = GetComponent<Animator>(); // Corrected to use GetComponent<Animator>()
    }

    void Update()
    {
        // Update the animation state based on currentlyCooking
        if (animator != null)
        {
            animator.SetBool("IsCooking", currentlyCooking);
        }

        if (isPlayerNear)
        {
            // Turn on appliance if it isn't already on and the player prompts you to do so.
            if (Input.GetKeyDown(KeyCode.F) && !this.currentlyCooking)
            {
                chef.maxShrimpCapacity = shrimpCount;
                chef.shrimpInsertPoint = this.transform;
                chef.DepositShrimp();
                StartCoroutine(Cooking());
            }
        }
    }

    private IEnumerator Cooking()
    {
        this.currentlyCooking = true; // set
        float cookTime = shrimpCount * chef.cookingTimePerShrimp;

        // Start the cooking animation
        if (animator != null)
        {
            animator.SetBool("IsCooking", true);
        }

        yield return new WaitForSeconds(cookTime);

        // Finish cooking
        this.currentlyCooking = false;

        // Stop the cooking animation
        if (animator != null)
        {
            animator.SetBool("IsCooking", false);
        }
    }

    private void StartMixer()
    {
        // when the appliance is running
        if (this.currentlyCooking)
        {
            Debug.Log("Shrimps cookin'");
            // Animation can now be controlled by the IsCooking bool in the Animator
        }
        else
        {
            Debug.Log("Shrimp ain't cookin'");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // when the player enters the interaction zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near an appliance.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // when the player leaves the interaction zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the appliance.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // when the player is trying to access a running appliance
        if (other.CompareTag("Player") && this.currentlyCooking)
        {
            Debug.Log("Wait for the appliance to finish cooking");
        }
    }
}
