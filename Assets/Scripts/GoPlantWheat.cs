using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoPlantWheat : GAction
{
    public GameObject destinationObjectPrefab; // The new object to be replaced
    public GameObject currentDestinationObject; // Reference to the current destination object
    public float customStoppingDistance = 2.0f;
    public List<GameObject> replacementObjects;
    public GPlanner planner;
    public SubGoal currentGoal;
    public GAgent Gagent;

    // Reference to the WheatFinder script
    public WheatFinder wheatFinder;

    public override bool PrePerform()
    {
        // Perform any other necessary pre-action logic
        return true;
    }

    public override bool PostPerform()
    {
        wheatFinder = GetComponent<WheatFinder>();
        if (this.target != null)
        {
            Vector3 spawnPosition = this.target.transform.position;

            // Choose and instantiate the replacement object
            if (replacementObjects != null && replacementObjects.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, replacementObjects.Count);
                GameObject chosenReplacement = replacementObjects[randomIndex];

                // Instantiate the chosen replacement at the same position with a random rotation
                Quaternion randomRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
                UnityEngine.Debug.Log("----------------------------------------------------" + randomRotation);
                GameObject newWheat = Instantiate(chosenReplacement, spawnPosition, randomRotation);

                // Get FarmParent from WheatFinder script
                GameObject parentObject = wheatFinder.FarmParent;

                if (parentObject != null)
                {
                    newWheat.transform.SetParent(parentObject.transform);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("FarmParent not found in WheatFinder script.");
                }

                // Perform any necessary setup for the new wheat (e.g., setting up components, tags, etc.)

                // Destroy the old wheat
                DestroyImmediate(this.target);

                // You can also update the current action's target to be the new wheat
                this.target = newWheat;
            }
            else
            {
                // If no replacement object is specified, you can reset the target and tag to empty values
                target = null;
            }
        }

        // Perform any other necessary post-action logic

        this.target = null;
        return true;
    }
}
