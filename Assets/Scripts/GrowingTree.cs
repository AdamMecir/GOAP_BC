using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingTree : MonoBehaviour
{
    private float minGrowthDuration = 10.0f; // Minimum growth duration (seconds)
    private float maxGrowthDuration = 20.0f; // Maximum growth duration (seconds)
    private Vector3 minTargetScale = new Vector3(1.5f, 1.5f, 1.5f); // Minimum target scale
    private Vector3 maxTargetScale = new Vector3(3.0f, 3.0f, 3.0f); // Maximum target scale

    private Vector3 initialScale;
    private float timer;
    private float growthDuration; // Random growth duration for the tree
    private Vector3 targetScale; // Random target scale for the tree
    private bool isGrowing = true; // A flag to check if the tree is still growing.

    private void Start()
    {
        initialScale = transform.localScale;

        // Generate random growth duration and target scale within the specified ranges
        growthDuration = UnityEngine.Random.Range(minGrowthDuration, maxGrowthDuration);

        // Generate a random chance (1 to 100)
        int randomChance = UnityEngine.Random.Range(1, 5);

        // If the chance is 1, set huge target scales
        if (randomChance == 1)
        {
            minTargetScale = new Vector3(4.0f, 4.0f, 4.0f);
            maxTargetScale = new Vector3(6.0f, 6.0f, 6.0f);
        }
        else
        {
            // Otherwise, generate random target scales within the normal range
            minTargetScale = new Vector3(1.5f, 1.5f, 1.5f);
            maxTargetScale = new Vector3(3.0f, 3.0f, 3.0f);
        }

        targetScale = new Vector3(
            UnityEngine.Random.Range(minTargetScale.x, maxTargetScale.x),
            UnityEngine.Random.Range(minTargetScale.y, maxTargetScale.y),
            UnityEngine.Random.Range(minTargetScale.z, maxTargetScale.z)
        );
    }

    private void Update()
    {
        if (isGrowing)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / growthDuration);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            if (t >= 1.0f)
            {
                isGrowing = false; // Tree has finished growing.
                OnTreeFullyGrown(); // Call the method to handle actions once the tree is fully grown.
            }
        }
    }

    private void OnTreeFullyGrown()
    {
        // Additional actions to be performed once the tree is fully grown.
        // For example, change the tag to "GrownTree" and perform other related actions.
        gameObject.tag = "GrownTree";

        // Add any other actions you want to perform after the tree has fully grown.
    }
}