using System.Collections;
using UnityEngine;

public class FridgeSpawner : MonoBehaviour
{
    [Header("Fridge Settings")]
    public Transform spawnPoint; // The point where shrimp spawn
    public GameObject shrimpPrefab; // The shrimp prefab to spawn
    public Animator fridgeAnimator; // Animator to control the fridge door animation
    public string fridgeOpenAnimationName = "FridgeOpen"; // The name of the opening animation
    public float animationDuration = 2.5f; // Total duration of the animation
    public float spawnIntervalMin = 4f; // Minimum time between spawns
    public float spawnIntervalMax = 9f; // Maximum time between spawns
    public int shrimpBatchSize = 3; // Number of shrimp to spawn per batch
    [Header("Shrimp Manager")]
    public ShrimpManager shrimpManager; // Reference to the ShrimpManager

    void Start()
    {
        StartCoroutine(SpawnShrimpRoutine());
    }

    IEnumerator SpawnShrimpRoutine()
    {
        while (true)
        {
            // Wait for a random interval
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            // Open the fridge door
            if (fridgeAnimator != null)
            {
                fridgeAnimator.SetTrigger(fridgeOpenAnimationName);
            }

            // Wait for the animation to complete (assuming animationDuration matches its length)
            yield return new WaitForSeconds(animationDuration);

            // Spawn shrimp during the animation
            for (int i = 0; i < shrimpBatchSize; i++)
            {
                GameObject shrimp = Instantiate(shrimpPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f); // Delay between spawning individual shrimp
                if (shrimpManager != null)
                {
                    shrimpManager.AddShrimpToTroupe(shrimp);
                }
            }


            // Wait for the remaining part of the animation (door closing)
            yield return new WaitForSeconds(animationDuration);
        }
    }
}