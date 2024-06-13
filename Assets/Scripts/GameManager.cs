using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using TMPro;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public int Count = 0;
    public bool SpecialNameViktor = true;
    public bool SpecialNameKubo = true;
    public bool SpecialNameBrano = true;
    public bool SpecialNameStano = true;
    public bool SpecialNameTomas = true;
    public bool SpecialNameLukas = true;
    public TextMeshProUGUI clockText; // Assuming you have a TextMeshProUGUI component for displaying the clock.

    private void Start()
    {
        // Find the child GameObject named "ClockTextMesh"
        GameObject clockTextTransform = GameObject.Find("ClockTextMesh");

        // Get the TextMeshProUGUI component from the child GameObject
        clockText = clockTextTransform.GetComponent<TextMeshProUGUI>();

        if (clockText == null)
        {
            Debug.LogError("ClockTextMesh not found or does not contain TextMeshProUGUI component.");
        }
    }

    private void Update()
    {
        DisplayNPC();
    }

    private void DisplayNPC()
    {
        // Find all game objects with a NavMeshAgent component
        NavMeshAgent[] allAgents = UnityEngine.Object.FindObjectsOfType<NavMeshAgent>();

        // Get the count of NavMeshAgent components
        int agentsCount = allAgents.Length;
        Count = agentsCount;

        // Update the text of the UI element to display the count of NPCs
        clockText.text = "NPC Count: " + agentsCount;
    }
}
