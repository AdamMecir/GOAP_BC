using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GoMakeDealFood : GAction
{
    public float customStoppingDistance = 2.0f;
    public GPlanner planner;
    public SubGoal currentGoal;
    public GAgent Gagent;

    public DealSystemFood dealSystem; // Reference to the DealSystem script
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
        dealSystem = target.GetComponent<DealSystemFood>();
        if (dealSystem != null && inventory != null)
        {
            
            if (gameObject.tag == "OwnerFoodShop")
            {

                var result = dealSystem.MakeDealBread(this.inventory.breadCollected);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount;


                inventory.MakeDealBreadCity(fullprice, desiredamount);

            }
            else
            {
                // Call the MakeDeal function with the Inventory as an argument
                var result = dealSystem.MakeDealBread(this.inventory.breadCapacity);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount; // Use desiredamount, not desiredAmount
                                                          // You can access the updated woodDeal value from the inventory if needed
                inventory.MakeDealWheat(fullprice, desiredamount);

                if (Gagent.barrelRenderer != null)
                {
                    Gagent.barrelRenderer.enabled = true;

                }
            }
        }
        else
        {
            UnityEngine.Debug.LogError("DealSystem or Inventory script not found on the same object.");
        }

        return true;
    }
}
