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
    private float cookTime;
    
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
            currentlyCooking = chef.isCooking;
            if (currentlyCooking)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    private IEnumerator Cooking()
    {
        cookTime = shrimpCount * chef.cookingTimePerShrimp * Time.deltaTime; //the time it takes for the shrimp to cook + the amt of shrimp
        yield return new WaitForSeconds(cookTime);
    }
    public void CookDaShrimp()
    {
                chef.maxShrimpCapacity = shrimpCount;
                chef.shrimpInsertPoint = transform;
                chef.DepositShrimp();
    }

    private void StartMixer()
    {
        // when the appliance is running
        if (currentlyCooking)
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
        if (other.CompareTag("Invisible"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near an appliance.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // when the player leaves the interaction zone
        if (other.CompareTag("Invisible"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the appliance.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // when the player is trying to access a running appliance
        if (other.CompareTag("Invisible") && currentlyCooking) // Example key for entering the air fryer
        {
            Debug.Log("Wait for the appliance to finish cooking");
        }
        if (other.CompareTag("Invisible") && Input.GetKeyDown(KeyCode.F) && !this.currentlyCooking)
        {
            CookDaShrimp();
        }
    }
}
