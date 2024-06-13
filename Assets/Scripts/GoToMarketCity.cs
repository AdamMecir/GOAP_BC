using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMarketCity : GAction
{
    public GAgent Gagent;
    public float customStoppingDistance = 2.0f;
    // Reference to different DealSystem scripts for different shop types
    public DealSystemCity dealSystem;
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
        // Based on the target's tag, use the appropriate DealSystem
        dealSystem = target.GetComponent<DealSystemCity>();
        switch (gameObject.tag)
        {
            case "OwnerStoneShop":
           
                if (dealSystem != null && inventory != null)
                {
                   
                    CompleteStoneDeal();
                    OrderFromStoneVillage();

                }
                break;
            case "OwnerIronShop":
          
                if (dealSystem != null && inventory != null)
                {
                    CompleteIronDeal();
                    OrderFromIronVillage();
                }
                break;
            case "OwnerFoodShop":
    
                if (dealSystem != null && inventory != null)
                {
                    CompleteFoodDeal();
                    OrderFromFoodVillage();
                }
                break;
            case "OwnerWoodShop":
          
                if (dealSystem != null && inventory != null)
                {
                    CompleteWoodDeal();
                    OrderFromWoodVillage();
                }
                break;
            default:
                UnityEngine.Debug.LogError("Unrecognized shop owner tag.");
                break;
        }

        return true;
    }
    
    private void CompleteStoneDeal()
    {
        var result = dealSystem.CompleteDealStone(this.inventory.stoneDeal, this.inventory.desiredStoneAmount);
        int stoneDeal = result.Money;
        int desiredStoneAmount = result.DealedAmount;
        // You can access the updated woodDeal value from the inventory if needed
        inventory.CompleteDealStone(stoneDeal, desiredStoneAmount);
    }

    

    private void CompleteIronDeal()
    {
        // Assume you have a similar CompleteDealIron method and inventory fields for iron deals
        var result = dealSystem.CompleteDealIron(this.inventory.ironDeal, inventory.desiredIronAmount);
        int ironDeal = result.Money;
        int desiredIronAmount = result.DealedAmount;

        inventory.CompleteDealIron(ironDeal, desiredIronAmount);
    }

    private void CompleteFoodDeal()
    {
        var result = dealSystem.CompleteDealBread(this.inventory.breadDeal, inventory.desiredBreadAmount);
        int breadDeal = result.Money;
        int desiredBreadAmount = result.DealedAmount;

        inventory.CompleteDealWheat(breadDeal, desiredBreadAmount);
    }

    private void CompleteWoodDeal()
    {
        var result = dealSystem.CompleteDealWood(this.inventory.woodDeal, inventory.desiredWoodAmount);
        int woodDeal = result.Money;
        int desiredWoodAmount = result.DealedAmount;

        inventory.CompleteDeal(woodDeal, desiredWoodAmount);
    }
    
    private void OrderFromStoneVillage()
    {
        var result = dealSystem.OrderFromStoneVillage(this.inventory.AmountMoney());
        int Money = result.Money;
        int Bread = result.Bread;
        int Iron = result.Iron;
        int Wood = result.Wood;

        inventory.CompleteOrderFromStoneVillage(Money, Bread, Iron, Wood);
    }

    private void OrderFromIronVillage()
    {
        var result = dealSystem.OrderFromIronVillage(this.inventory.AmountMoney());
        int Money = result.Money;
        int Bread = result.Bread;
        int Stone = result.Stone;
        int Wood = result.Wood;

        inventory.CompleteOrderFromIronVillage(Money, Bread, Stone, Wood);
    }

    private void OrderFromFoodVillage()
    {
        var result = dealSystem.OrderFromFoodVillage(this.inventory.AmountMoney());
        int Money = result.Money;
        int Stone = result.Stone;
        int Iron = result.Iron;
        int Wood = result.Wood;

        inventory.CompleteOrderFromFoodVillage(Money, Stone, Iron, Wood);
    }

    private void OrderFromWoodVillage()
    {
        var result = dealSystem.OrderFromWoodVillage(this.inventory.AmountMoney());
        int Money = result.Money;
        int Bread = result.Bread;
        int Iron = result.Iron;
        int Stone = result.Stone;

        inventory.CompleteOrderFromWoodVillage(Money, Bread, Iron, Stone);
    }

}
