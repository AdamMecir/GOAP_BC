using System;
using UnityEngine;


public class DealSystemIron : MonoBehaviour
{
    public IronShopKeeperInventory inventory;
    public CityShopkeeperInventory inventoryCity;
    public int ironPricePerUnit;
    public int ironPricePerUnitCity;

    private void Start()
    {
        // Ensure you have a reference to the Inventory component.
        inventory = GetComponent<IronShopKeeperInventory>();
        if (inventory == null)
        {
            inventoryCity = GetComponent<CityShopkeeperInventory>();

        }
    }

    private void Update()
    {
        // Calculate the price per one wood and store it.
        ironPricePerUnit = CalculateStonePricePerUnit();
        ironPricePerUnitCity = CalculateStonePricePerUnitCity();
    }

    // Calculate the price per unit of wood based on the wood storage capacity.
    private int CalculateStonePricePerUnit()
    {
        int price;
        price = 1;

        return price;

    }

    private int CalculateStonePricePerUnitCity()
    {
        int price;
        price = 2;

        return price;

    }

    // Example function for buying wood.
    public (int fullPrice, int desiredamount) MakeDealIron(int maxAmount)
    {
        int desiredamount;
        int fullprice;
        if (inventory != null)
        {
            desiredamount = 6;
            fullprice = ironPricePerUnit * desiredamount;
            inventory.MakeDeal(fullprice, desiredamount);
        }
        else
        {
            desiredamount = maxAmount;
            fullprice = ironPricePerUnitCity * desiredamount;
            inventoryCity.MakeDealIron(fullprice, desiredamount);
        }

        return (fullprice, desiredamount);

    }
    public (int Money, int DealedAmount) CompleteDealIron(int Money, int DealedAmount)
    {
        if (inventory != null)
        {
            inventory.CompleteDeal(Money, DealedAmount);
        }
        else
        {
            inventoryCity.CompleteDealIron(Money, DealedAmount);
        }
        return (Money, DealedAmount);
    }
}
