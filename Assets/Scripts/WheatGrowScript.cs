using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatGrowScript : MonoBehaviour
{
    private float minGrowthDuration = 100f; // Minimum growth duration (seconds) for wheat
    private float maxGrowthDuration = 200f; // Maximum growth duration (seconds) for wheat
    private Vector3 minTargetScale = new Vector3(4f, 2f, 4f); // Minimum target scale for wheat
    private Vector3 maxTargetScale = new Vector3(4f, 6f, 4f); // Maximum target scale for wheat

    private Vector3 initialScale;
    private float timer;
    private float growthDuration; // Random growth duration for wheat
    private Vector3 targetScale; // Random target scale for wheat
    private bool isGrowing = true; // A flag to check if the wheat is still growing.

    private void Start()
    {
        initialScale = transform.localScale;

        // Generate random growth duration and target scale within the specified ranges for wheat
        growthDuration = Random.Range(minGrowthDuration, maxGrowthDuration);

        // Generate random target scales within the normal range for wheat
        targetScale = new Vector3(
            Random.Range(minTargetScale.x, maxTargetScale.x),
            Random.Range(minTargetScale.y, maxTargetScale.y),
            Random.Range(minTargetScale.z, maxTargetScale.z)
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
                isGrowing = false; // Wheat has finished growing.
                OnWheatFullyGrown(); // Call the method to handle actions once the wheat is fully grown.
            }
        }
    }

    private void OnWheatFullyGrown()
    {
        // Additional actions to be performed once the wheat is fully grown.
        // For example, change the tag to "GrownWheat" and perform other related actions.
        gameObject.tag = "GrownWheat";

        // Add any other actions you want to perform after the wheat has fully grown.
    }
    public Vector3 GetTargetScale()
    {
        return targetScale;
    }
}
