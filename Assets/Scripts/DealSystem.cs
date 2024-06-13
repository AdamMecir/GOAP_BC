using System;
using UnityEngine;

public class DealSystem : MonoBehaviour
{
    public WoodShopKeeperInventory inventory; // Reference to the player's inventory
    public CityShopkeeperInventory inventoryCity;

    public int woodPricePerUnit; // Store the calculated price per one wood
    public int woodPricePerUnitCity;

    private void Start()
    {
        // Ensure you have a reference to the Inventory component.
        inventory = GetComponent<WoodShopKeeperInventory>();
        if (inventory == null)
        {
            inventoryCity = GetComponent<CityShopkeeperInventory>();
        }
    }

    private void Update()
    {
        // Calculate the price per one wood and store it.
        woodPricePerUnit = CalculateWoodPricePerUnit();
        woodPricePerUnitCity = CalculateWoodPricePerUnitCity();
    }

    // Calculate the price per unit of wood based on the wood storage capacity.
    private int CalculateWoodPricePerUnit()
    {
        int price;
        price = 1;

        return price;
    }

    private int CalculateWoodPricePerUnitCity()
    {
        int price;
        price = 2;

        return price;
    }

    public (int fullPrice, int desiredamount) MakeDeal(int maxAmount)
    {

        int desiredamount;
        int fullprice;
        if (inventory != null)
        {
            desiredamount = 6;
            fullprice = woodPricePerUnit * desiredamount;
            inventory.MakeDeal(fullprice, desiredamount);
        }
        else
        {
            desiredamount = maxAmount;
            fullprice = woodPricePerUnitCity * desiredamount;
            inventoryCity.MakeDealWood(fullprice, desiredamount);
        }

        return (fullprice, desiredamount);
    }

    public (int Money, int DealedAmount) CompleteDealWood(int Money, int DealedAmount)
    {

        if (inventory != null)
        {
            inventory.CompleteDeal(Money, DealedAmount);
        }
        else
        {
            inventoryCity.CompleteDealWood(Money, DealedAmount);
        }
        return (Money, DealedAmount);
    }
}
