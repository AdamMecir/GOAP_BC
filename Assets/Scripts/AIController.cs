using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GAction[] actions;
    public WorldStates agentBeliefs;

    private void Start()
    {
        // Initialize agent's beliefs
        agentBeliefs = new WorldStates();
        agentBeliefs.ModifyState("wood", 0);
        agentBeliefs.ModifyState("money", 0);

        // Initialize actions
        actions = GetComponents<GAction>();
        foreach(GAction a in actions)
        {
            a.agentBeliefs = agentBeliefs;
        }
    }

}
