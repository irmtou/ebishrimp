using System.Collections;
using UnityEngine;

public class FridgeSpawner : MonoBehaviour
{
    [Header("Fridge Settings")]
    public Transform spawnPoint; // The point where shrimp spawn
    public GameObject shrimpPrefab; // The shrimp prefab to spawn
    public Animator fridgeAnimator; // Animator to control the fridge door animation
    public AudioManager audioManager;
    public AudioClip sound;
    public string fridgeOpenAnimationName = "FridgeOpen"; // The name of the opening animation
    public float animationDuration = 2.5f; // Total duration of the animation
    public float spawnIntervalMin = 3f; // Minimum time between spawns
    public float spawnIntervalMax = 6f; // Maximum time between spawns
    public int shrimpBatchSize = 3; // Number of shrimp to spawn per batch
    public int shrimpBatchSizeMin = 3; 
    public int shrimpBatchSizeMax = 5; 
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
            // float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax); // old implementation
            float waitTime = randomizerTime(GameManager.Instance.difficulty); // new implementation (w/difficulty)
            yield return new WaitForSeconds(waitTime);

            // Open the fridge door
            if (fridgeAnimator != null)
            {
                fridgeAnimator.SetTrigger(fridgeOpenAnimationName);
            }

            // Wait for the animation to complete (assuming animationDuration matches its length)
            yield return new WaitForSeconds(animationDuration);

            shrimpBatchSize = randomizerBatchSize(GameManager.Instance.difficulty); // new implementation (w/difficulty)

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

            audioManager.PlaySound(sound);

            // Wait for the remaining part of the animation (door closing)
            yield return new WaitForSeconds(animationDuration);
        }
    }

    float randomizerTime(int difficulty)
    {
        float waitTime;
        if (difficulty == 1) 
        {
            // easy
            waitTime = Random.Range(spawnIntervalMin+3.0f, spawnIntervalMax);
        } 
        else if (difficulty == 2)
        {
            // medium
            waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
        else if (difficulty == 3)
        {
            // hard
            waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax/2);
        }
        else if (difficulty == 4)
        {
            // x-mode (extreme)
            waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax/3);
        } 
        else
        {
            waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
        return waitTime;
    }

    int randomizerBatchSize(int difficulty)
    {
        int shrimpBatchSize;
        if (difficulty == 1) 
        {
            // easy
            shrimpBatchSize = (int) Random.Range(shrimpBatchSizeMin, shrimpBatchSizeMin);
        } 
        else if (difficulty == 2)
        {
            // medium
            shrimpBatchSize = (int) Random.Range(shrimpBatchSizeMin+1, shrimpBatchSizeMax+1);
        }
        else if (difficulty == 3)
        {
            // hard
            shrimpBatchSize = (int) Random.Range(shrimpBatchSizeMin+2, shrimpBatchSizeMax+3);
        }
        else if (difficulty == 4)
        {
            // x-mode (extreme)
            shrimpBatchSize = (int) Random.Range(shrimpBatchSizeMin+3, shrimpBatchSizeMax+3);
        } 
        else
        {
            shrimpBatchSize = (int) Random.Range(shrimpBatchSizeMin, shrimpBatchSizeMax);
        }
        return shrimpBatchSize;
    }


}