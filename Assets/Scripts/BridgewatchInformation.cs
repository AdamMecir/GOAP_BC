using UnityEngine;

public class BridgewatchInformation : MonoBehaviour
{
    public string Name;
    public int Population;
    public int Money;


    public string GetInformationName()
    {
        Name = "Bridgewatch";
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