using System.Collections; // Include this to use IEnumerator
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject humanPrefab; // The human prefab to spawn
    public GameObject infectedPrefab; // The infected prefab to spawn after a delay
    public int numberOfHumans = 50; // The number of humans to spawn

    private bool hasInstantiatedInfected = false; // Flag to ensure the infected prefab is instantiated only once

    void Start()
    {
        // Ensure the number of humans does not exceed 50
        numberOfHumans = Mathf.Min(numberOfHumans, 50);

        // Spawn the initial humans
        for (int i = 0; i < numberOfHumans; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-8f, 8f),
                Random.Range(-4f, 4f)
            );
            Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
        }

        // Start the coroutine to instantiate the infected prefab after a delay
        StartCoroutine(InstantiateInfectedAfterDelay(5f));
    }

    // Coroutine to instantiate the infected prefab after a delay
    private IEnumerator InstantiateInfectedAfterDelay(float delay)
    {
        if (hasInstantiatedInfected) yield break; // Ensure it only runs once

        yield return new WaitForSeconds(delay); // Wait for the specified delay

        Vector2 spawnPosition = new Vector2(
            Random.Range(-8f, 8f),
            Random.Range(-4f, 4f)
        );

        Instantiate(infectedPrefab, spawnPosition, Quaternion.identity); // Instantiate the infected prefab
        hasInstantiatedInfected = true; // Mark as instantiated to prevent further calls
    }
}