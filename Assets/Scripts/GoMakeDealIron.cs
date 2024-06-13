using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GoMakeDealIron : GAction
{
    public float customStoppingDistance = 2.0f;
    public GPlanner planner;
    public SubGoal currentGoal;
    public GAgent Gagent;

    public DealSystemIron dealSystem; // Reference to the DealSystem script
    public Inventory inventory; // Reference to the Inventory script

    // Start is called before the first frame update
    void Start()
    {

        inventory = GetComponent<Inventory>();

    }

    public override bool PrePerform()
    {
        

        return true;
    }

    public override bool PostPerform()
    {
        dealSystem = target.GetComponent<DealSystemIron>();
        if (dealSystem != null && inventory != null)
        {
            if (gameObject.tag == "OwnerIronShop")
            {

                var result = dealSystem.MakeDealIron(this.inventory.ironCollected);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount;
            

                inventory.MakeDealIron(fullprice, desiredamount);

            }
            else
            {
                var result = dealSystem.MakeDealIron(this.inventory.ironCapacity);
                int fullprice = result.fullPrice; // Use fullPrice, not fullprice
                int desiredamount = result.desiredamount;


                inventory.MakeDealIron(fullprice, desiredamount);

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
