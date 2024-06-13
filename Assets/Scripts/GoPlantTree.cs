using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// GoPlantTree class inherits from GAction class
public class GoPlantTree : GAction
{
    public GameObject destinationObjectPrefab; // The new object to be replaced

    public GameObject currentDestinationObject; // Reference to the current destination object

    public float customStoppingDistance = 2.0f;

    public List<GameObject> replacementObjects;

    public GPlanner planner;

    public SubGoal currentGoal;

    public GAgent Gagent;

   /* public void Update()
    {
        currentDestinationObject = this.target;
        if (target == null)
        {
            //this.running = false;
            Gagent.change("isSelling");
        }
    }*/

    public override bool PrePerform()
    {

        // Perform any other necessary pre-action logic
        return true;
    }

    public override bool PostPerform()
    {
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
                UnityEngine.Debug.Log("----------------------------------------------------"+randomRotation);
                GameObject newTree = Instantiate(chosenReplacement, spawnPosition, randomRotation);

                // Perform any necessary setup for the new tree (e.g., setting up components, tags, etc.)

                // Destroy the old tree
                DestroyImmediate(this.target);

                // You can also update the current action's target to be the new tree
                this.target = newTree;
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
