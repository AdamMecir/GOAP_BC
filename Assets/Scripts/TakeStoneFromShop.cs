using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeStoneFromShop : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    //private string nextAction = "isSelling"; // New variable to store the name of the next action
    public Inventory inventory; // Reference to the Inventory script
    public StoneShopKeeperInventory Stonekeeperinventory;
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
        int Stones;
        Stonekeeperinventory = target.GetComponent<StoneShopKeeperInventory>();
        Stones = Stonekeeperinventory.AmountStone();
        Stonekeeperinventory.RemoveStone(Stones);
        inventory.AddStone(Stones);

        Money = Stonekeeperinventory.AmountMoney();
        Money = (int)Math.Round(Money / 2.0); 
        Stonekeeperinventory.RemoveMoney(Money);
        inventory.AddMoney(Money);




        return true;
    }
}
