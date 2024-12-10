using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingAppliance : MonoBehaviour {
    [Header("Appliance Settings")]
    public int maxShrimpCapacity = 5; // Maximum number of shrimp the appliance can hold
    public float cookingTimePerShrimp = 3f; // Time it takes to cook each shrimp
    public Transform shrimpInsertPoint; // Where shrimp are visually placed in the appliance
    public ShrimpManager shrimpManager; // Reference to the ShrimpManager
    public float interactionRadius = 3f; // Radius for detecting shrimp
    public LayerMask shrimpLayer;       // Layer mask for filtering shrimp

    // Queue of shrimp currently in the appliance
    private Queue<GameObject> shrimpInAppliance = new Queue<GameObject>(); 
    private bool isCooking = false; // Whether the appliance is currently cooking shrimp

    void Update() {
        // This can probably be replaced with Gio/Cameron's code
        if (Input.GetKeyDown(KeyCode.F)) {
            DepositShrimp();
        }
    }


    void DepositShrimp() {
        Collider[] nearbyShrimp = Physics.OverlapSphere(transform.position, interactionRadius, shrimpLayer);

        if (shrimpInAppliance.Count >= maxShrimpCapacity) {
            Debug.Log("Appliance is full!");
            // Optional: Play error sound or show UI feedback
            return;
        }

        if (nearbyShrimp.Length == 0) {
            Debug.Log("No shrimp available to cook.");
            return;
        }

        bool shrimpDeposited = false;

        foreach (Collider collider in nearbyShrimp) {
            if (shrimpInAppliance.Count >= maxShrimpCapacity) break;

            ShrimpFollower shrimp = collider.GetComponent<ShrimpFollower>();
            if (shrimp != null && shrimpManager.shrimpTroupe.Contains(shrimp.gameObject)) {
                shrimpManager.RemoveShrimpFromTroupe(shrimp.gameObject);
                shrimpInAppliance.Enqueue(shrimp.gameObject);

                // Visually move shrimp ... Not sure if we should do it like this
                shrimp.transform.position = shrimpInsertPoint.position + new Vector3(0, 0.5f * shrimpInAppliance.Count, 0); // Adjust as needed
                shrimp.transform.rotation = Quaternion.identity;

                // Disable shrimp's movement behavior
                shrimp.enabled = false;

                shrimpDeposited = true;
                Debug.Log("Shrimp deposited into appliance.");
            }
        }

        if (shrimpDeposited) {
            // Update shrimp count

            if (!isCooking && shrimpInAppliance.Count > 0) {
                StartCoroutine(CookShrimp());
            }
        }
        else {
            Debug.Log("No eligible shrimp found to cook.");
        }
    }


    IEnumerator CookShrimp() {
        isCooking = true;

        while (shrimpInAppliance.Count > 0) {
            // Get the next shrimp to cook
            GameObject shrimp = shrimpInAppliance.Dequeue();

            Debug.Log("Cooking shrimp...");
            yield return new WaitForSeconds(cookingTimePerShrimp); // Simulate cooking time

            // Shrimp is cooked
            Destroy(shrimp); // Remove shrimp from the game
            shrimpManager.IncrementCookedShrimpCount(1); // Update the global cooked shrimp count

            Debug.Log("Shrimp cooked!");
        }

        isCooking = false;
    }
}

