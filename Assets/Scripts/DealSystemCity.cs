using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealSystemCity : MonoBehaviour
{
    public CityShopkeeperInventory inventory;

    public int stonePricePerUnit;
    public int ironPricePerUnit;
    public int breadPricePerUnit;
    public int woodPricePerUnit;

    private void Start()
    {
        // Ensure you have a reference to the Inventory component.
        inventory = GetComponent<CityShopkeeperInventory>();
    }

    private void Update()
    {
        // Calculate the price per one wood and store it.
        stonePricePerUnit = CalculateStonePricePerUnit();
        breadPricePerUnit = CalculateBreadPricePerUnit();
        ironPricePerUnit = CalculateIronPricePerUnit();
        woodPricePerUnit = CalculateWoodPricePerUnit();
    }

    // Calculate the price per unit of wood based on the wood storage capacity.
    private int CalculateStonePricePerUnit()
    {
        int price;
        price = 2;

        return price;

    }
    private int CalculateBreadPricePerUnit()
    {
        int price;
        price = 6;

        return price;

    }
    private int CalculateIronPricePerUnit()
    {
        int price;
        price = 2;

        return price;

    }
    private int CalculateWoodPricePerUnit()
    {
        int price;
        price = 2;

        return price;

    }





    public (int Wood, int Bread, int Iron , int Money) OrderFromStoneVillage(int Money)
    {
        int Wood = 51;
        int Bread = 51;
        int Iron = 51;
        do
        {
            if(inventory.AmountWood() >= Wood)
            {
                --Wood;
            }
            else
            {
                Wood = 0;
            }
            if (inventory.AmountBread() >= Bread)
            {
                --Bread;
            }
            else
            {
                Bread = 0;
            }
            if (inventory.AmountIron() >= Iron)
            {
                --Iron;
            }
            else
            {
                Iron = 0;
            }
           

        } while (((Wood * (woodPricePerUnit - 1)) + (Bread * (breadPricePerUnit - 6)) + (Iron * (ironPricePerUnit - 1))) >= Money);
        int MoneyForCity = ((Wood * (woodPricePerUnit - 1)) + (Bread * (breadPricePerUnit - 6)) + (Iron * (ironPricePerUnit - 1)));
        Money -= ((Wood * (woodPricePerUnit - 1)) + (Bread * (breadPricePerUnit - 6)) + (Iron * (ironPricePerUnit - 1)));
        inventory.OrderFromStoneVillage(Wood , Bread , Iron , MoneyForCity);
        return (Wood, Bread, Iron, Money);
        
    }

    public (int Bread, int Stone, int Wood, int Money) OrderFromIronVillage(int Money)
    {
        int Wood = 51;
        int Bread = 51;
        int Stone = 51;
        do
        {
            if (inventory.AmountWood() >= Wood)
            {
                --Wood;
            }
            else
            {
                Wood = 0;
            }
            if (inventory.AmountBread() >= Bread)
            {
                --Bread;
            }
            else
            {
                Bread = 0;
            }
            if (inventory.AmountStone() >= Stone)
            {
                --Stone;
            }
            else
            {
                Stone = 0;
            }


        } while (((Wood * (woodPricePerUnit - 1)) + (Bread * (breadPricePerUnit - 6)) + (Stone * (stonePricePerUnit - 1))) >= Money);
        int MoneyForCity = ((Wood * (woodPricePerUnit - 1)) + (Bread * (breadPricePerUnit - 6)) + (Stone * (stonePricePerUnit - 1)));
        Money -= ((Wood * (woodPricePerUnit - 1)) + (Bread * (breadPricePerUnit - 6)) + (Stone * (stonePricePerUnit - 1)));
        inventory.OrderFromIronVillage(Wood, Bread, Stone, MoneyForCity);
        return (Wood, Bread, Stone, Money);

    }
        
    public (int Iron, int Stone, int Wood, int Money) OrderFromFoodVillage(int Money)
    {

        int Wood = 51;
        int Iron = 51;
        int Stone = 51;
        do
        {
            if (inventory.AmountWood() >= Wood)
            {
                --Wood;
            }
            else
            {
                Wood = 0;
            }
            if (inventory.AmountIron() >= Iron)
            {
                --Iron;
            }
            else
            {
                Iron = 0;
            }
            if (inventory.AmountStone() >= Stone)
            {
                --Stone;
            }
            else
            {
                Stone = 0;
            }


        } while (((Wood * (woodPricePerUnit - 1)) + (Iron * (ironPricePerUnit - 1)) + (Stone * (stonePricePerUnit - 1))) >= Money);
        int MoneyForCity = ((Wood * (woodPricePerUnit - 1)) + (Iron * (ironPricePerUnit - 1)) + (Stone * (stonePricePerUnit - 1)));
        Money -= ((Wood * (woodPricePerUnit - 1)) + (Iron * (ironPricePerUnit - 1)) + (Stone * (stonePricePerUnit - 1)));
        inventory.OrderFromFoodVillage(Wood, Iron, Stone, MoneyForCity);
        return (Wood, Iron, Stone, Money);

    }

    public (int Iron, int Stone, int Bread, int Money) OrderFromWoodVillage(int Money)
    {
        int Bread = 51;
        int Iron = 51;
        int Stone = 51;
        do
        {
            if (inventory.AmountBread() >= Bread)
            {
                --Bread;
            }
            else
            {
                Bread = 0;
            }
            if (inventory.AmountIron() >= Iron)
            {
                --Iron;
            }
            else
            {
                Iron = 0;
            }
            if (inventory.AmountStone() >= Stone)
            {
                --Stone;
            }
            else
            {
                Stone = 0;
            }


        } while (((Bread * (breadPricePerUnit - 6)) + (Iron * (ironPricePerUnit - 1)) + (Stone * (stonePricePerUnit - 1))) >= Money);
        int MoneyForCity = ((Bread * (breadPricePerUnit - 6)) + (Iron * (ironPricePerUnit - 1)) + (Stone * (stonePricePerUnit - 1)));
        Money -= ((Bread * (breadPricePerUnit - 6)) + (Iron * (ironPricePerUnit - 1)) + (Stone * (stonePricePerUnit - 1)));
        inventory.OrderFromWoodVillage(Bread, Iron, Stone, MoneyForCity);
        return (Bread, Iron, Stone, Money);

    }


    public (int fullPrice, int desiredamount) MakeDealStone(int maxAmount)
    {

        int desiredamount = maxAmount;
        int fullprice = stonePricePerUnit * desiredamount;
        inventory.MakeDealStone(fullprice, desiredamount);
        return (fullprice, desiredamount);

    }


    public (int Money, int DealedAmount) CompleteDealStone(int Money, int DealedAmount)
    {
        inventory.CompleteDealStone(Money, DealedAmount);
        return (Money, DealedAmount);
    }


    public (int fullPrice, int desiredamount) MakeDealIron(int maxAmount)
    {

        int desiredamount = maxAmount;
        int fullprice = ironPricePerUnit * desiredamount;
        inventory.MakeDealIron(fullprice, desiredamount);
        return (fullprice, desiredamount);

    }
    public (int Money, int DealedAmount) CompleteDealIron(int Money, int DealedAmount)
    {
        inventory.CompleteDealIron(Money, DealedAmount);
        return (Money, DealedAmount);
    }





    public (int fullPrice, int desiredamount) MakeDealBread(int maxAmount)
    {

        int desiredamount = maxAmount;
        int fullprice = breadPricePerUnit * desiredamount;
        inventory.MakeDealBread(fullprice, desiredamount);
        return (fullprice, desiredamount);

    }
    public (int Money, int DealedAmount) CompleteDealBread(int Money, int DealedAmount)
    {
        inventory.CompleteDealBread(Money, DealedAmount);
        return (Money, DealedAmount);
    }




    public (int fullPrice, int desiredamount) MakeDealWood(int maxAmount)
    {

        int desiredamount = maxAmount;
        int fullprice = woodPricePerUnit * desiredamount;
        inventory.MakeDealWood(fullprice, desiredamount);
        return (fullprice, desiredamount);

    }
    public (int Money, int DealedAmount) CompleteDealWood(int Money, int DealedAmount)
    {
        inventory.CompleteDealWood(Money, DealedAmount);
        return (Money, DealedAmount);
    }
}
