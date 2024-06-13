using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationOfVillage : MonoBehaviour
{
    public string Name = "";
    public int Population = 0;
    public int Money = 0;


    public string GetInformationName()
    {
        return Name;
    }
    public int GetInformationPopulation()
    {
        return Population;
    }
    public int GetInformationMoney()
    {
        return Money;
    }
}
