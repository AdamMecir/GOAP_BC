using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GoStockUpIronMarket : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public Inventory inventory; // Reference to the Inventory script
    public IronShopKeeperInventory Ironkeeperinventory;

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

        Ironkeeperinventory = target.GetComponent<IronShopKeeperInventory>();

        Ironkeeperinventory.AddStone(inventory.AmountStone());
        inventory.RemoveStone(inventory.AmountStone());

        Ironkeeperinventory.AddIron(inventory.AmountIron());
        inventory.RemoveIron(inventory.AmountIron());

        Ironkeeperinventory.AddBread(inventory.AmountBread());
        inventory.RemoveBread(inventory.AmountBread());

        Ironkeeperinventory.AddWood(inventory.AmountWood());
        inventory.RemoveWood(inventory.AmountWood());

        Ironkeeperinventory.AddMoney(inventory.AmountMoney());
        inventory.RemoveMoney(inventory.AmountMoney());




        return true;
    }
}
