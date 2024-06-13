using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMarketIron : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public DealSystemIron dealSystem; // Reference to the DealSystem script
    public Inventory inventory; // Reference to the Inventory script
    void Start()
    {

        inventory = GetComponent<Inventory>();
    }

    public override bool PrePerform()
    {


        return true; // Proceed with the action

    }

    public override bool PostPerform()
    {
        dealSystem = target.GetComponent<DealSystemIron>();
        if (dealSystem != null && inventory != null)
        {

            var result = dealSystem.CompleteDealIron(this.inventory.ironDeal, this.inventory.desiredIronAmount);
            int ironDeal = result.Money;
            int desiredIronAmount = result.DealedAmount;
            // You can access the updated woodDeal value from the inventory if needed
            inventory.CompleteDealIron(ironDeal, desiredIronAmount);
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
