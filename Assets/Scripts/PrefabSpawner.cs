using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab you want to spawn
    public Transform spawnPosition; // The position where the prefab will be spawned
    public float spawnInterval = 3f; // The time interval in seconds between spawns
    public int maxSpawnCount; // The maximum number of spawns

    private int spawnCount = 0; // Keep track of the spawned count
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to spawn the prefab repeatedly
        StartCoroutine(SpawnPrefabRepeatedly());
    }

    // Coroutine to spawn the prefab repeatedly
    private IEnumerator SpawnPrefabRepeatedly()
    {
        while (spawnCount < maxSpawnCount)
        {
            SpawnPrefab(); // Call the method to spawn the prefab
            spawnCount++; // Increment the spawned count

            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before the next spawn
        }
    }

    // Method to spawn the prefab at the specified position
    public void SpawnPrefab()
    {
        if (prefabToSpawn == null)
        {
            //Debug.LogError("PrefabToSpawn is not set in the PrefabSpawner script.");
            return;
        }

        if (spawnPosition == null)
        {
           // Debug.LogError("SpawnPosition is not set in the PrefabSpawner script.");
            return;
        }

        Instantiate(prefabToSpawn, spawnPosition.position, spawnPosition.rotation);
    }
}
