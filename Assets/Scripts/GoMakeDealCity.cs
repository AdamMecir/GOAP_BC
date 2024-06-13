using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GoMakeDealCity : GAction
{
    public float customStoppingDistance = 2.0f;
    public GPlanner planner;
    public SubGoal currentGoal;
    public GAgent Gagent;

    public DealSystemCity dealSystem; // Reference to the DealSystem script
    public Inventory inventory; // Reference to the Inventory script

    // Start is called before the first frame update
    void Start()
    {

        inventory = GetComponent<Inventory>();
    }

    public override bool PrePerform()
    {
        // Perform any necessary pre-action logic

        return true;
    }

    public override bool PostPerform()
    {
        dealSystem = target.GetComponent<DealSystemCity>();
        if (dealSystem != null && inventory != null)
        {
            // Call the MakeDeal function with the Inventory as an argument
            if(inventory.stoneCollected >= 1)
            {
                var result = dealSystem.MakeDealStone(this.inventory.stoneCollected);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount;

                inventory.MakeDealStone(fullprice, desiredamount);
            }
            if (inventory.ironCollected >= 1)
            {
                var result = dealSystem.MakeDealIron(this.inventory.ironCollected);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount;

                inventory.MakeDealIron(fullprice, desiredamount);
            }
            if (inventory.breadCollected >= 1)
            {
                var result = dealSystem.MakeDealBread(this.inventory.breadCollected);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount; // Use desiredamount, not desiredAmount
                                                          // You can access the updated woodDeal value from the inventory if needed
                inventory.MakeDealWheat(fullprice, desiredamount);
            }
            if (inventory.woodCollected >= 1)
            {
                var result = dealSystem.MakeDealWood(this.inventory.woodCollected);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount; // Use desiredamount, not desiredAmount
                                                          // You can access the updated woodDeal value from the inventory if needed
                inventory.MakeDeal(fullprice, desiredamount);
            }
        }
        else
        {
            UnityEngine.Debug.LogError("DealSystem or Inventory script not found on the same object.");
        }

        return true;
    }
}
