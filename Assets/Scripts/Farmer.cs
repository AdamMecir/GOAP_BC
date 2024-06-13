using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : GAgent
{
    protected override void Start()
    {
        base.Start(); // Call the Start method of the parent class (GAgent)

        // Create a sub-goal to go to the forest and chop wood
        SubGoal s1 = new SubGoal("isSelling", 1, true);

        // Add the sub-goal with a priority for the lumberjack character
        goals.Add(s1, 3);

    }
}
