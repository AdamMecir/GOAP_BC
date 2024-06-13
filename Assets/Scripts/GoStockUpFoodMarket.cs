using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GoStockUpFoodMarket : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public Inventory inventory; // Reference to the Inventory script
    public FoodShopKeeperInventory Foodkeeperinventory;

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

        Foodkeeperinventory = target.GetComponent<FoodShopKeeperInventory>();

        Foodkeeperinventory.AddStone(inventory.AmountStone());
        inventory.RemoveStone(inventory.AmountStone());

        Foodkeeperinventory.AddIron(inventory.AmountIron());
        inventory.RemoveIron(inventory.AmountIron());

        Foodkeeperinventory.AddBread(inventory.AmountBread());
        inventory.RemoveBread(inventory.AmountBread());

        Foodkeeperinventory.AddWood(inventory.AmountWood());
        inventory.RemoveWood(inventory.AmountWood());

        Foodkeeperinventory.AddMoney(inventory.AmountMoney());
        inventory.RemoveMoney(inventory.AmountMoney());




        return true;
    }
}
