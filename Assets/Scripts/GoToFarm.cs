using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GoToFarm : GAction
{
    public GameObject destinationObjectPrefab; // The new object to be replaced
    public GameObject currentDestinationObject; // Reference to the current destination object
    public float customStoppingDistance = 2.0f;
    public List<GameObject> replacementObjects;

    // Reference to the WheatFinder script
    public WheatFinder wheatFinder;

    public GPlanner planner;
    public SubGoal currentGoal;
    public GAgent Gagent;
    public Inventory inventory;
    public float higherOffset = 0.5f; // Offset to make the new object lower
    public float lowerOffset = 0.5f;

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        if (this.target != null)
        {
            inventory = GetComponent<Inventory>();
            wheatFinder = GetComponent<WheatFinder>();
            Vector3 spawnPosition = this.target.transform.position;

            if (replacementObjects != null && replacementObjects.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, replacementObjects.Count);
                GameObject chosenReplacement = replacementObjects[randomIndex];

                Quaternion randomRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
                GameObject newWheat = Instantiate(chosenReplacement, spawnPosition, randomRotation);

                // Get FarmParent from WheatFinder script
                GameObject parentObject = wheatFinder.FarmParent;

                if (parentObject != null)
                {
                    

                    // Access the target scale from WheatGrowScript using the new GetTargetScale() method
                    Transform targetScale = target.transform;

                    // Calculate a size value based on the target scale (for simplicity, using the average of x, y, z)
                    float targetSize = (targetScale.transform.localScale.y) / 1f;

                    // Add food based on the size of the target
                    int wheatToAdd = Mathf.RoundToInt(targetSize);
                    this.inventory.AddWheat(wheatToAdd);


                    newWheat.transform.SetParent(parentObject.transform);
                    DestroyImmediate(this.target);
                    this.target = newWheat;
                }
                else
                {
                    UnityEngine.Debug.LogWarning("FarmParent not found in WheatFinder script.");
                }
            }
            else
            {
                target = null;
            }
        }

        this.target = null;

        return true;
    }

}
