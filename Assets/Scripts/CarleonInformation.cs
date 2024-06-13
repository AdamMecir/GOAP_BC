using UnityEngine;

public class CarleonInformation : MonoBehaviour
{
    public string Name;
    public int Population;
    public int Money;


    public string GetInformationName()
    {
        Name = "Carleon";
        return Name;
    }
    public int GetInformationPopulation()
    {
        Population = 0;
        return Population;
    }
    public int GetInformationMoney()
    {
        Money = 0;
        return Money;
    }
}