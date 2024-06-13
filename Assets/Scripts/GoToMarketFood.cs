using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMarketFood : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public DealSystemFood dealSystem; // Reference to the DealSystem script
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
        dealSystem = target.GetComponent<DealSystemFood>();
        if (dealSystem != null && inventory != null)
        {

            var result = dealSystem.CompleteDealBread(this.inventory.breadDeal, this.inventory.desiredBreadAmount);
            int wheatDeal = result.Money;
            int desiredWheatAmount = result.DealedAmount;
            // You can access the updated woodDeal value from the inventory if needed
            inventory.CompleteDealWheat(wheatDeal, desiredWheatAmount);
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
