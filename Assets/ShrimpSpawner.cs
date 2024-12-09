using System.Collections;
using UnityEngine;

public class FridgeSpawner : MonoBehaviour {
    [Header("Fridge Settings")]
    public Transform spawnPoint; // The point where shrimp spawn
    public GameObject shrimpPrefab; // The shrimp prefab to spawn
    public Animator fridgeAnimator; // Animator to control the fridge door animation
    public string fridgeAnimationName = "FridgeOpenClose"; // The name of the opening/closing animation
    public float animationDuration = 2.5f; // Total duration of the animation
    public float spawnIntervalMin = 2f; // Minimum time between spawns
    public float spawnIntervalMax = 5f; // Maximum time between spawns
    public int shrimpBatchSize = 4; // Number of shrimp to spawn per batch

    void Start() {
        StartCoroutine(SpawnShrimpRoutine());
    }

    IEnumerator SpawnShrimpRoutine() {
        while (true) {
            // Wait for a random interval
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            // Play the fridge animation
            //fridgeAnimator.Play(fridgeAnimationName);

            // Wait for the animation to complete (assuming animationDuration matches its length)
            yield return new WaitForSeconds(animationDuration); // Wait for the door to open halfway

            // Spawn shrimp during the animation
            for (int i = 0; i < shrimpBatchSize; i++) {
                GameObject shrimp = Instantiate(shrimpPrefab, spawnPoint.position, Quaternion.identity);
                

                yield return new WaitForSeconds(0.2f); // Delay between spawning individual shrimp
            }

            // Wait for the remaining part of the animation (door closing)
            yield return new WaitForSeconds(animationDuration);
        }
    }
}
