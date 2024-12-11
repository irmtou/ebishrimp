using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using UnityEngine;

public class CookingAppliance : MonoBehaviour {
    [Header("Appliance Settings")]
    public int maxShrimpCapacity = 5; // Maximum number of shrimp the appliance can hold
    public float cookingTimePerShrimp = 3f; // Time it takes to cook each shrimp
    public Transform shrimpInsertPoint; // Where shrimp are visually placed in the appliance
    public ShrimpManager shrimpManager; // Reference to the ShrimpManager
    public float interactionRadius = 3f; // Radius for detecting shrimp
    public LayerMask shrimpLayer;       // Layer mask for filtering shrimp
    public ParticleSystem fireEffect;   // Reference to the fire particle system

    // Queue of shrimp currently in the appliance
    private Queue<GameObject> shrimpInAppliance = new Queue<GameObject>(); 
    public bool isCooking = false; // Whether the appliance is currently cooking shrimp

    void Update() {
        // Replacing this with a call from appliance interaction code - Gio
    }


    public void DepositShrimp() {
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
                StartCoroutine(ShrimpIntoAppliance(shrimp, shrimp.transform.position, shrimpInsertPoint.position + new Vector3(0, 0.5f * shrimpInAppliance.Count, 0)));
                // transform.localPosition =  shrimpInsertPoint.position + new Vector3(0, 0.5f * shrimpInAppliance.Count, 0);
                // shrimp.transform.rotation = Quaternion.identity;
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

    private IEnumerator ShrimpIntoAppliance(ShrimpFollower shrimp, Vector3 startPos, Vector3 finalPos) {
        float elapsed = 0f;
        float duration = 1f;
        float arcHeight = 2f;

        Vector3 originalScale = shrimp.transform.localScale;

        while (elapsed<duration)
        {
            float t = elapsed/duration;
            Vector3 position = Vector3.Lerp(startPos, finalPos, t);
            position.y += Mathf.Sin(t * Mathf.PI) * arcHeight;

            shrimp.transform.position = position;

            // Lerp = Linearly interpolate
            shrimp.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }


    IEnumerator CookShrimp() {
        isCooking = true;

        while (shrimpInAppliance.Count > 0) {
            // Get the next shrimp to cook
            GameObject shrimp = shrimpInAppliance.Dequeue();

            // Trigger the fire effect
            if (fireEffect != null) {
                fireEffect.Play(); // Start the fire effect
            }

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

