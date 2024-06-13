using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// WorldState represents a single key-value pair in the world's state
public class WorldState
{
    public string key; // Key of the state (e.g., "wood", "gold", etc.)
    public int value;  // Value associated with the key
}

public class WorldStates
{
    // Dictionary to store the world states (key-value pairs)
    public Dictionary<string, int> states;

    // Awake is called when the script instance is being loaded
        public WorldStates()
        {
            states = new Dictionary<string, int>();
        }


    // Check if a state with the given key exists
    public bool HasState(string key)
    {
        return states.ContainsKey(key);
    }

    // Add a state with the given key and value to the world
    public void AddState(string key, int value)
    {
        states.Add(key, value);
    }

    // Modify a state with the given key by adding the provided value
    public void ModifyState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] += value;
            // If the value becomes less than or equal to 0, remove the state
            if (states[key] <= 0)
                RemoveState(key);
        }
        else
            states.Add(key, value);
    }

    // Remove a state with the given key from the world
    public void RemoveState(string key)
    {
        if (states.ContainsKey(key))
            states.Remove(key);
    }

    // Set the value of a state with the given key to the provided value
    public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
            states[key] = value;
        else
            states.Add(key, value);
    }

    // Get a copy of the entire world state (dictionary)
    public Dictionary<string, int> GetStates()
    {
        return states;
    }

}