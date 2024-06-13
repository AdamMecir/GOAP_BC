using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeFoodFromShop : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public Inventory inventory; // Reference to the Inventory script
    public FoodShopKeeperInventory Foodkeeperinventory;
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
        int Breads;
        Foodkeeperinventory = target.GetComponent<FoodShopKeeperInventory>();
        Breads = Foodkeeperinventory.AmountBread();
        Foodkeeperinventory.RemoveBread(Breads);
        inventory.AddBread(Breads);

        Money = Foodkeeperinventory.AmountMoney();
        Money = (int)Math.Round(Money / 2.0);
        Foodkeeperinventory.RemoveMoney(Money);
        inventory.AddMoney(Money);




        return true;
    }
}
