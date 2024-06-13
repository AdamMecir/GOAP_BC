using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRespawn : MonoBehaviour
{
    public GameObject stonePrefab; // Assign this in the Unity Editor with the prefab you want to spawn
    private float spawnInterval = 10f; // Interval in seconds after which a new stone is spawned
    private float timer; // Timer to track when to spawn the next stone

    void Start()
    {
        // Initialize the timer
        timer = spawnInterval;
    }

    void Update()
    {
        // Decrement the timer by the amount of time that has passed since the last frame
        timer -= Time.deltaTime;

        // Check if the timer has reached zero or less
        if (timer <= 0)
        {
            // Reset the timer
            timer = spawnInterval;

            // Spawn the stone prefab at the position of the GameObject this script is attached to
            Instantiate(stonePrefab, transform.position, Quaternion.identity);

            // Destroy the current game object
            Destroy(gameObject);
        }
    }
}
