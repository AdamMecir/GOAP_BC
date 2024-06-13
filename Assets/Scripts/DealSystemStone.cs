using System;
using UnityEngine;

public class DealSystemStone : MonoBehaviour
{
    public StoneShopKeeperInventory inventory;
    public CityShopkeeperInventory inventoryCity;

    public int stonePricePerUnit;
    public int stonePricePerUnitCity;

    private void Start()
    {
        // Ensure you have a reference to the Inventory component.
        inventory = GetComponent<StoneShopKeeperInventory>();
        if(inventory == null)
        {
            inventoryCity = GetComponent<CityShopkeeperInventory>();

        }
    }

    private void Update()
    {
        // Calculate the price per one wood and store it.
        stonePricePerUnit = CalculateStonePricePerUnit();
        stonePricePerUnitCity = CalculateStonePricePerUnitCity();
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
    public (int fullPrice, int desiredamount) MakeDealStone(int maxAmount)
    {

        int desiredamount;
        int fullprice;
        if (inventory != null)
        {
            desiredamount = 6;
            fullprice = stonePricePerUnit * desiredamount;
            inventory.MakeDeal(fullprice, desiredamount);
        }
        else
        {
            desiredamount = maxAmount;
            fullprice = stonePricePerUnitCity * desiredamount;
            inventoryCity.MakeDealStone(fullprice, desiredamount);
        }
        
        return (fullprice, desiredamount);

    }
    public (int Money, int DealedAmount) CompleteDealStone(int Money, int DealedAmount)
    {

        if (inventory != null)
        {
            inventory.CompleteDeal(Money, DealedAmount);
        }
        else
        {
            inventoryCity.CompleteDealStone(Money, DealedAmount);
        }
        return (Money, DealedAmount);
    }
}
