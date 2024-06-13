using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DealSystemWindmill : MonoBehaviour
{
    public FoodShopKeeperInventory inventory; // Reference to the player's inventory

    public int breadPricePerUnit; // Store the calculated price per one wood
    public int Material;

    private void Start()
    {
        // Ensure you have a reference to the Inventory component.
        inventory = GetComponent<FoodShopKeeperInventory>();

        // Calculate the price per one wood and store it.
        var result = CalculateFoodPricePerUnit();
        breadPricePerUnit = result.priceFee; // Use fullPrice, not fullprice
        Material = result.Material; // Use desiredamount, not desiredAmount
    }

    private void Update()
    {
        // Any additional update logic if needed
    }

    // Calculate the price per unit of wood based on the wood storage capacity.
    private (int priceFee, int Material) CalculateFoodPricePerUnit()
    {
        int priceFee = 1;
        int Material = 5;

        return (priceFee, Material);
    }

    public int MakeDealBread(int wheat)
    {
        CalculateFoodPricePerUnit();
        int Bread = wheat / Material;
        inventory.AddMoney(Bread * breadPricePerUnit);
        return Bread;
    }
}

