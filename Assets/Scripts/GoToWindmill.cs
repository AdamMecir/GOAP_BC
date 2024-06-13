using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GoToWindmill : GAction
{
    public float customStoppingDistance = 2.0f;
    public GPlanner planner;
    public SubGoal currentGoal;
    public GAgent Gagent;

    public DealSystemWindmill dealSystem; // Reference to the DealSystem script
    public Inventory inventory; // Reference to the Inventory script

    // Start is called before the first frame update
    void Start()
    {
        // Get the DealSystem script from the same GameObject


        // Get the Inventory script from the same GameObject
        inventory = GetComponent<Inventory>();
    }

    public override bool PrePerform()
    {
        // Perform any necessary pre-action logic

        return true;
    }

    public override bool PostPerform()
    {
        dealSystem = target.GetComponent<DealSystemWindmill>();
        if (dealSystem != null && inventory != null)
        {
            int wheat;
            int money;
            if (inventory.wheatCollected >= inventory.desiredBreadAmount * dealSystem.Material && inventory.Money >= inventory.desiredBreadAmount*dealSystem.Material)
            {

                wheat = inventory.desiredBreadAmount*dealSystem.breadPricePerUnit;
                
                int Bread = dealSystem.MakeDealBread(wheat);

                money = Bread * dealSystem.breadPricePerUnit;

                inventory.RemoveMoney(money);
                inventory.RemoveWheat(wheat);

                inventory.MakeDealBread(Bread);
               
            }
            
                
            //UnityEngine.Debug.LogError("deal");
        }
        else
        {
            UnityEngine.Debug.LogError("DealSystem or Inventory script not found on the same object.");
        }

        return true;
    }
}
