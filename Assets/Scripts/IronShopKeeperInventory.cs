using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class IronShopKeeperInventory : MonoBehaviour
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
    public int ironCapacity = 200;

    //
    void Start()
    {
        // Initialize Money with a random value between 100 and 500 (adjust the range as needed)
        Money = Random.Range(500, 1000);
        ironCollected = Random.Range(200, 400);
        ironCapacity = 200;

        // Initialize woodCapacity with a random value between 100 and 1000 (adjust the range as needed)
    }
    public int GetMaxIronCapacity()
    {
        return ironCapacity;
    }



    //
    public void MakeDeal(int amount, int iron)
    {
        ironDeal += iron;
        Money -= amount;
        DealMoney += amount;
    }
    public void CompleteDeal(int amount, int stone)
    {
        DealMoney -= amount;
        ironDeal -= stone;
        ironCollected += stone;
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
