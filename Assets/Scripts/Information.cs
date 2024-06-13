using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    public string Name = "";
    public string Profession = "";
    public GameManager gamemanager;

    string[] slovakConsonantSequences = { "ch", "h", "k", "l", "m", "n", "p", "r", "s", "t", "v", "z" };
    string[] slovakVowelSequences = { "a", "e", "i", "o", "u", "y" };

    private void Start()
    {
        // Find the GameManager GameObject
        gamemanager = GameObject.FindObjectOfType<GameManager>();

        GenerateRandomName();
    }

    public void GenerateRandomName()
    {
        // Generate a random number between 1 and 100
        int specialNameChance = UnityEngine.Random.Range(1, 101);

        if (specialNameChance == 1 && gamemanager.SpecialNameViktor == true)
        {
            Name = "Viktor Palkovic";
            gamemanager.SpecialNameViktor = false;
        }
        else if (specialNameChance == 2 && gamemanager.SpecialNameKubo == true)
        {
            Name = "Kubo Petrovic";
            gamemanager.SpecialNameViktor = false;
        }
        else if (specialNameChance == 3 && gamemanager.SpecialNameBrano == true)
        {
            Name = "Branislav Lucansky";
            gamemanager.SpecialNameViktor = false;
        }
        else if (specialNameChance == 4 && gamemanager.SpecialNameStano == true)
        {
            Name = "Stano Smal";
            gamemanager.SpecialNameViktor = false;
        }
        else if (specialNameChance == 5 && gamemanager.SpecialNameTomas == true)
        {
            Name = "Tomas Tytykalo";
            gamemanager.SpecialNameViktor = false;
        }
        else if (specialNameChance == 6 && gamemanager.SpecialNameLukas == true)
        {
            Name = "Lukas Petrufka";
            gamemanager.SpecialNameViktor = false;
        }
        else
        {
            // Generate a random Slovak-like name
            Name = GenerateRandomSlovakName(4, 7) + " " + GenerateRandomSlovakName(5, 7);
        }
    }

    private string GenerateRandomSlovakName(int minLength, int maxLength)
    {
        int length = UnityEngine.Random.Range(minLength, maxLength + 1);
        string randomString = "";

        for (int i = 0; i < length; i++)
        {
            string[] sequenceSet = (i % 2 == 0) ? slovakConsonantSequences : slovakVowelSequences;
            string randomSequence = sequenceSet[UnityEngine.Random.Range(0, sequenceSet.Length)];
            randomString += randomSequence;
        }

        return char.ToUpper(randomString[0]) + randomString.Substring(1);
    }

    public string GetInformationName()
    {
        return Name;
    }

    public string GetInformationProfession()
    {
        return Profession;
    }
}
