using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The GAction class represents an abstract action that the agent can perform
public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";          // Name of the action
    public float cost = 1.0f;                     // Cost of performing the action
    public GameObject target;                     // Target object of the action
    public string targetTag;                      // Tag of the target object
    public float duration;                    // Duration of the action (if any)
    public WorldState[] preConditions;            // Pre-conditions required to perform the action
    public WorldState[] afterEffects;             // Effects of the action after completion
    public NavMeshAgent agent;                    // Reference to the NavMeshAgent component
    public Dictionary<string, int> preconditions;  // Dictionary to store pre-conditions
    public Dictionary<string, int> effects;         // Dictionary to store effects

    public WorldStates agentBeliefs;              // Reference to the agent's world states (beliefs)

    public bool running = false;                  // Flag to indicate if the action is currently running
  


   

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        // Get the NavMeshAgent component attached to this GameObject
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        

        // Copy the preConditions and afterEffects arrays into the preconditions and effects dictionaries
        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
        }
    }

    // Check if the action is achievable based on its preconditions
    public bool IsAchievable()
    {
        // For this implementation, all actions are achievable
        return true;
    }

    // Check if the action is achievable given a set of conditions
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        // Check if all preconditions of the action are present in the given conditions
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
                return false;
        }
        return true;
    }

    // Virtual methods that can be overridden by the derived classes
    public abstract bool PrePerform();
    public abstract bool PostPerform();


}
