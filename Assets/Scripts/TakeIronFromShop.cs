using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeIronFromShop : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public Inventory inventory; // Reference to the Inventory script
    public IronShopKeeperInventory Ironkeeperinventory;
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
        int Irons;
        Ironkeeperinventory = target.GetComponent<IronShopKeeperInventory>();
        Irons = Ironkeeperinventory.AmountIron();
        Ironkeeperinventory.RemoveIron(Irons);
        inventory.AddIron(Irons);

        Money = Ironkeeperinventory.AmountMoney();
        Money = (int)Math.Round(Money / 2.0);
        Ironkeeperinventory.RemoveMoney(Money);
        inventory.AddMoney(Money);




        return true;
    }
}
