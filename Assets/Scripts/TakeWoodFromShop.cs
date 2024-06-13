using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWoodFromShop : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public Inventory inventory; // Reference to the Inventory script
    public WoodShopKeeperInventory Woodkeeperinventory;
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
        int Money;
        int Woods;
        Woodkeeperinventory = target.GetComponent<WoodShopKeeperInventory>();
        Woods = Woodkeeperinventory.AmountWood();
        Woodkeeperinventory.RemoveWood(Woods);
        inventory.AddWood(Woods);

        Money = Woodkeeperinventory.AmountMoney();
        Money = (int)Math.Round(Money / 2.0);
        Woodkeeperinventory.RemoveMoney(Money);
        inventory.AddMoney(Money);




        return true;
    }
}
