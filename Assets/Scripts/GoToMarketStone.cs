using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMarketStone : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public DealSystemStone dealSystem; // Reference to the DealSystem script
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
        dealSystem = target.GetComponent<DealSystemStone>();
        if (dealSystem != null && inventory != null)
        {

            var result = dealSystem.CompleteDealStone(this.inventory.stoneDeal, this.inventory.desiredStoneAmount);
            int stoneDeal = result.Money;
            int desiredStoneAmount = result.DealedAmount;
            // You can access the updated woodDeal value from the inventory if needed
            inventory.CompleteDealStone(stoneDeal, desiredStoneAmount);
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
