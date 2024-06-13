using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GoToStonePit : GAction
{
    public GameObject destinationObjectPrefab; // The new object to be replaced

    public GameObject currentDestinationObject; // Reference to the current destination object

    public float customStoppingDistance = 2.0f;

    public List<GameObject> replacementObjects;

    // public GAction currentAction;
    // Reference to the TreeFinder script

    public GPlanner planner;

    public SubGoal currentGoal;

    public GAgent Gagent;

    public Inventory inventory;

    public string parentTag = "StoneParent"; // Tag of the parent GameObject

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
            Vector3 spawnPosition = this.target.transform.position;

            if (replacementObjects != null && replacementObjects.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, replacementObjects.Count);
                GameObject chosenReplacement = replacementObjects[randomIndex];

                Quaternion randomRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
                UnityEngine.Debug.Log("----------------------------------------------------" + randomRotation);
                GameObject newStone = Instantiate(chosenReplacement, spawnPosition, randomRotation);

                // Find the parent GameObject by tag
                GameObject parentObject = GameObject.FindWithTag(parentTag);
                if (parentObject != null)
                {
                    // Set the parent of the newTree to the found parentObject
                    newStone.transform.SetParent(parentObject.transform);

                    // Offset the newTree position within its parent's coordinate system
                    //Vector3 lowerPosition = newTree.transform.localPosition;
                    //lowerPosition.y -= lowerOffset;
                    //newTree.transform.localPosition = lowerPosition;

                    // Offset the newTree position to be higher relative to its parent
                    //newTree.transform.localPosition += Vector3.up * higherOffset;

                    DestroyImmediate(this.target);
                    this.inventory.AddStone(1);
                    this.target = newStone;
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Parent object not found with the tag '" + parentTag + "'.");
                }
            }
            else
            {
                target = null;
            }

            // Perform any other necessary post-action logic
        }

        this.target = null;

        return true;




    }
}
