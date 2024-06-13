using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GoToMarket class inherits from GAction class

public class GoToMarketWood : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public DealSystem dealSystem; // Reference to the DealSystem script
    public Inventory inventory; // Reference to the Inventory script
    void Start()
    {
        // Get the DealSystem script from the same GameObject


        // Get the Inventory script from the same GameObject
        inventory = GetComponent<Inventory>();
    }

    public override bool PrePerform()
    {
       

        return true; // Proceed with the action

    }

    public override bool PostPerform()
    {
        dealSystem = target.GetComponent<DealSystem>();
        if (dealSystem != null && inventory != null)
        {
            
            var result =  dealSystem.CompleteDealWood(this.inventory.woodDeal,this.inventory.desiredWoodAmount);
            int woodDeal = result.Money;
            int desiredWoodAmount = result.DealedAmount;
            // You can access the updated woodDeal value from the inventory if needed
            inventory.CompleteDeal(woodDeal, desiredWoodAmount);
            if (Gagent.barrelRenderer != null)
            {
                Gagent.barrelRenderer.enabled = false;

            }
        }
        else
        {
            UnityEngine.Debug.LogError("DealSystem or Inventory script not found on the same object.");
        }
        
        return true;
    }
}