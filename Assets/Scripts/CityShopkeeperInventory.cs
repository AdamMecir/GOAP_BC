using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CityShopkeeperInventory : MonoBehaviour
{
    public int Money = 0;
    public int DealMoney = 0;

    public int ironCollected = 0;
    public int breadCollected = 0;
    public int woodCollected = 0;
    public int stoneCollected = 0;

    public int ironDeal = 0;
    public int breadDeal = 0;
    public int woodDeal = 0;
    public int stoneDeal = 0;
    //
    void Start()
    {
        // Initialize Money with a random value between 100 and 500 (adjust the range as needed)
        Money = Random.Range(5000, 10000);
        ironCollected = Random.Range(200, 1000);
        breadCollected = Random.Range(200, 1000);
         woodCollected = Random.Range(200, 1000);
        stoneCollected = Random.Range(200, 1000);



    }




    public void MakeDealIron(int amount, int iron)
    {
        ironDeal += iron;
        Money -= amount;
        DealMoney += amount;
    }
    public void CompleteDealIron(int amount, int iron)
    {
        DealMoney -= amount;
        ironDeal -= iron;
        ironCollected += iron;
    }



    public void MakeDealBread(int amount, int bread)
    {
        breadDeal += bread;
        Money -= amount;
        DealMoney += amount;
    }
    public void CompleteDealBread(int amount, int bread)
    {
        DealMoney -= amount;
        breadDeal -= bread;
        breadCollected += bread;
    }




    public void MakeDealStone(int amount, int stone)
    {
        stoneDeal += stone;
        Money -= amount;
        DealMoney += amount;

    }
    public void CompleteDealStone(int amount, int stone)
    {
        DealMoney -= amount;
        stoneDeal -= stone;
        stoneCollected += stone;
    }



    public void OrderFromStoneVillage(int Wood , int Bread , int Iron , int Money)
    {
        RemoveBread(Bread);
        RemoveIron(Iron);
        RemoveWood(Wood);
        AddMoney(Money);
      
    }
    public void OrderFromIronVillage(int Wood, int Bread, int Stone, int Money)
    {
        RemoveBread(Bread);
        RemoveStone(Stone);
        RemoveWood(Wood);
        AddMoney(Money);

    }
    public void OrderFromFoodVillage(int Wood, int Iron, int Stone, int Money)
    {
        RemoveStone(Stone);
        RemoveIron(Iron);
        RemoveWood(Wood);
        AddMoney(Money);

    }
    public void OrderFromWoodVillage(int Bread, int Iron, int Stone, int Money)
    {
        RemoveBread(Bread);
        RemoveIron(Iron);
        RemoveStone(Stone);
        AddMoney(Money);

    }


    public void MakeDealWood(int amount, int wood)
    {
        woodDeal += wood;
        Money -= amount;
        DealMoney += amount;
    }
    public void CompleteDealWood(int amount, int wood)
    {
        DealMoney -= amount;
        woodDeal -= wood;
        woodCollected += wood;
    }







    public void AddMoney(int amount)
    {
        Money += amount;
    }
    public void AddStone(int amount)
    {
        stoneCollected += amount;
    }
    public void AddIron(int amount)
    {
        ironCollected += amount;
    }
    public void AddBread(int amount)
    {
        breadCollected += amount;
    }
    public void AddWood(int amount)
    {
        woodCollected += amount;
    }


    public void RemoveMoney(int amount)
    {
        Money = Mathf.Max(Money - amount, 0);
    }
    public void RemoveIron(int amount)
    {
        ironCollected = Mathf.Max(ironCollected - amount, 0);
    }
    public void RemoveStone(int amount)
    {
        stoneCollected = Mathf.Max(stoneCollected - amount, 0);
    }
    public void RemoveBread(int amount)
    {
        breadCollected = Mathf.Max(breadCollected - amount, 0);
    }
    public void RemoveWood(int amount)
    {
        woodCollected = Mathf.Max(woodCollected - amount, 0);
    }


    public void SetMoney(int amount)
    {
        Money = amount;
    }

    public void SetIron(int amount)
    {
        ironCollected = amount;
    }
    public void SetWood(int amount)
    {
        woodCollected = amount;
    }
    public void SetBread(int amount)
    {
        breadCollected = amount;
    }
    public void SetStone(int amount)
    {
        stoneCollected = amount;
    }



    public int AmountMoney()
    {
        return Money;
    }

    public int AmountIron()
    {
        return ironCollected;
    }
    public int AmountStone()
    {
        return stoneCollected;
    }
    public int AmountBread()
    {
        return breadCollected;
    }
    public int AmountWood()
    {
        return woodCollected;
    }
}
