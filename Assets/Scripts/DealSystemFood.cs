using System;
using UnityEngine;

public class DealSystemFood : MonoBehaviour
{
    public FoodShopKeeperInventory inventory;
    public CityShopkeeperInventory inventoryCity;

    public int breadPricePerUnit;
    public int breadPricePerUnitCity;

    private void Start()
    {
        // Ensure you have a reference to the Inventory component.
        inventory = GetComponent<FoodShopKeeperInventory>();
        if (inventory == null)
        {
            inventoryCity = GetComponent<CityShopkeeperInventory>();

        }
    }

    private void Update()
    {
        // Calculate the price per one wood and store it.
        breadPricePerUnit = CalculateFoodPricePerUnit();
        breadPricePerUnitCity = CalculateFoodPricePerUnitCity();
    }

    // Calculate the price per unit of wood based on the wood storage capacity.
    private int CalculateFoodPricePerUnit()
    {
        int price;
        price = 4;

        return price;

    }

    private int CalculateFoodPricePerUnitCity()
    {
        int price;
        price = 6;

        return price;

    }

    // Example function for buying wood.
    public (int fullPrice, int desiredamount) MakeDealBread(int maxAmount)
    {

        int desiredamount;
        int fullprice;

        if (inventory != null)
        {
            desiredamount = 6;
            fullprice = breadPricePerUnit * desiredamount;
            inventory.MakeDeal(fullprice, desiredamount);
        }
        else
        {
            desiredamount = maxAmount;
            fullprice = breadPricePerUnitCity * desiredamount;
            inventoryCity.MakeDealBread(fullprice, desiredamount);
        }

        return (fullprice, desiredamount);

    }
    public (int Money, int DealedAmount) CompleteDealBread(int Money, int DealedAmount)
    {
        inventory.CompleteDeal(Money, DealedAmount);
        if (inventory != null)
        {
            inventory.CompleteDeal(Money, DealedAmount);
        }
        else
        {
            inventoryCity.CompleteDealBread(Money, DealedAmount);
        }
        return (Money, DealedAmount);
    }
}
