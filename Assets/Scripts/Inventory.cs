using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    public int Money = 0;

    public int woodCollected = 0;
    public int stoneCollected = 0;
    public int ironCollected = 0;
    public int wheatCollected = 0;
    public int breadCollected = 0;



    public int woodPricePerUnit = 0;
    public int wheatPricePerUnit = 0;
    public int breadPricePerUnit = 0;
    public int stonePricePerUnit = 0;
    public int ironPricePerUnit = 0;

    public int woodCapacity = 0;
    public int wheatCapacity = 0;
    public int breadCapacity = 0;
    public int stoneCapacity = 0;
    public int ironCapacity = 0;


    public int woodDeal = 0;
    public int wheatDeal = 0;
    public int breadDeal = 0;
    public int stoneDeal = 0;
    public int ironDeal = 0;

    public int desiredWoodAmount = 0;
    public int desiredWheatAmount = 0;
    public int desiredBreadAmount = 0;
    public int desiredStoneAmount = 0;
    public int desiredIronAmount = 0;

    //
    void Start()
    {
        // Initialize Money with a random value between 100 and 500 (adjust the range as needed)
        Money = Random.Range(200, 500);

        // Initialize woodCapacity with a random value between 100 and 1000 (adjust the range as needed)
        woodCapacity = Random.Range(3, 6);
        wheatCapacity = Random.Range(100, 150);
        breadCapacity = Random.Range(10, 20);
        stoneCapacity = Random.Range(3, 6);
        ironCapacity = Random.Range(3, 6);
    }
    



    public bool CheckBread()
    {
        if (breadCollected == desiredBreadAmount)
            return true;
        else
            return false;
    }
    public bool CheckFood()
    {
        if (wheatCollected == desiredWheatAmount)
            return true;
        else
            return false;
    }
    public bool CheckWood()
    {
     if(woodCollected == desiredWoodAmount) 
            return true;
     else
            return false;  
    }
    public bool CheckStone()
    {
        if (stoneCollected == desiredStoneAmount)
            return true;
        else
            return false;
    }
    public bool CheckIron()
    {
        if (ironCollected == desiredIronAmount)
            return true;
        else
            return false;
    }



    public int GetMaxWoodCapacity()
    {
        return woodCapacity;
    }
    public int GetMaxFoodCapacity()
    {
        return wheatCapacity;
    }
    public int GetMaxBreadCapacity()
    {
        return breadCapacity;
    }
    public int GetMaxStoneCapacity()
    {
        return stoneCapacity;
    }
    public int GetMaxIronCapacity()
    {
        return ironCapacity;
    }



    public void CompleteOrderFromStoneVillage(int Money, int Bread , int Iron , int Wood)
    {
        RemoveMoney(Money);
        AddBread(Bread);
        AddIron(Iron);
        AddWood(Wood);
    }

    public void CompleteOrderFromIronVillage(int Money, int Bread, int Stone, int Wood)
    {
        RemoveMoney(Money);
        AddBread(Bread);
        AddStone(Stone);
        AddWood(Wood);
    }

    public void CompleteOrderFromFoodVillage(int Money, int Stone, int Iron, int Wood)
    {
        RemoveMoney(Money);
        AddIron(Iron);
        AddStone(Stone);
        AddWood(Wood);
    }

    public void CompleteOrderFromWoodVillage(int Money, int Bread, int Iron, int Stone)
    {
        RemoveMoney(Money);
        AddBread(Bread);
        AddIron(Iron);
        AddStone(Stone);
    }


    //------------------------------------------------------------------------------
    public void MakeDealStone(int amount, int stoneamountdealed)
    {
        stoneDeal = amount;
        desiredStoneAmount = stoneamountdealed;
    }
    //------------------------------------------------------------------------------
    public void MakeDealIron(int amount, int ironamountdealed)
    {
        ironDeal = amount;
        desiredIronAmount = ironamountdealed;
    }

    //------------------------------------------------------------------------------
    public void MakeDeal(int amount , int woodamountdealed)
    {
        woodDeal = amount;
        desiredWoodAmount = woodamountdealed;
    }
    //------------------------------------------------------------------------------
    public void MakeDealWheat(int amount, int wheatamountdealed)
    {
        breadDeal = amount;
        desiredBreadAmount = wheatamountdealed;
    }
    public void MakeDealBread(int amount)
    {
        breadCollected += amount;
    }
    public void MakeDealBreadCity(int amount, int breadamountdealed)
    {
        breadDeal = amount;
        desiredBreadAmount = breadamountdealed;
    }
    //------------------------------------------------------------------------------



    //------------------------------------------------------------------------------
    public void CompleteDeal(int amount , int woodamountdealed)
    {
        woodDeal -= amount;
        Money += amount;
        //desiredWoodAmount -= woodamountdealed;
        woodCollected -= woodamountdealed;
    }
    //------------------------------------------------------------------------------
    public void CompleteDealWheat(int amount, int breadamountdealed)
    {
        breadDeal -= amount;
        Money += amount;
        desiredBreadAmount -= breadamountdealed;
        breadCollected -= breadamountdealed;
    }
    //------------------------------------------------------------------------------
    public void CompleteDealStone(int amount, int stoneamountdealed)
    {
        stoneDeal -= amount;
        Money += amount;
        desiredStoneAmount -= stoneamountdealed;
        stoneCollected -= stoneamountdealed;
    }
    //------------------------------------------------------------------------------
    public void CompleteDealIron(int amount, int ironamountdealed)
    {
        ironDeal -= amount;
        Money += amount;
        desiredIronAmount -= ironamountdealed;
        ironCollected -= ironamountdealed;
    }





    public void AddMoney(int amount)
    {
        Money += amount;
    }
    public void AddWood(int amount)
    {
        woodCollected += amount;
    }

    public void AddStone(int amount)
    {
        stoneCollected += amount;
    }

    public void AddIron(int amount)
    {
        ironCollected += amount;
    }

    public void AddWheat(int amount)
    {
        wheatCollected += amount;
    }
    public void AddBread(int amount)
    {
        breadCollected += amount;
    }


    public void RemoveMoney(int amount)
    {
        Money = Mathf.Max(Money - amount, 0);
    }
    public void RemoveWood(int amount)
    {
        woodCollected = Mathf.Max(woodCollected - amount, 0);
    }

    public void RemoveStone(int amount)
    {
        stoneCollected = Mathf.Max(stoneCollected - amount, 0);
    }

    public void RemoveIron(int amount)
    {
        ironCollected = Mathf.Max(ironCollected - amount, 0);
    }

    public void RemoveWheat(int amount)
    {
        wheatCollected = Mathf.Max(wheatCollected - amount, 0);
    }
    public void RemoveBread(int amount)
    {
        breadCollected = Mathf.Max(breadCollected - amount, 0);
    }






    public void SetMoney(int amount)
    {
        Money = amount;
    }
    public void SetWheat(int amount)
    {
        wheatCollected = amount;
    }
    public void SetWood(int amount)
    {
        woodCollected = amount;
    }
    public void SetStone(int amount)
    {
        stoneCollected = amount;
    }
    public void SetIron(int amount)
    {
        ironCollected = amount;
    }
    public void SetBread(int amount)
    {
        breadCollected = amount;
    }


    public int AmountMoney()
    {
        return Money;
    }
    public int AmountWheat()
    {
        return wheatCollected;
    }
    public int AmountWood()
    {
        return woodCollected;
    }
    public int AmountStone()
    {
        return stoneCollected;
    }
    public int AmountIron()
    {
        return ironCollected;
    }
    public int AmountBread()
    {
        return breadCollected;
    }
}
