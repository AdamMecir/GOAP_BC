using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Node class represents a node in the graph for planning
public class Node
{
    public Node parent;                     // Parent node in the graph
    public float cost;                      // Cost to reach this node
    public Dictionary<string, int> state;   // Dictionary representing the current state of the world
    public GAction action;                  // Action associated with this node

    // Constructor to initialize a Node
    public Node(Node parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
    }
}

public class GPlanner 
{
    public List<GAction> availableActions = new List<GAction>();

    // Method to initialize the available actions
    public void Initialize(List<GAction> actions)
    {
        availableActions = new List<GAction>(actions);
    }

    // Method to generate a plan using the STRIPS planning algorithm
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states, Vector3 agentPosition, ref Queue<GAction> actionQueue, ref GAction currentAction, ref SubGoal currentGoal, ref Dictionary<SubGoal, int> goals)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (a.IsAchievable())
                usableActions.Add(a);
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null); // Pass the current state of the world

        bool success = BuildGraph(start, leaves, usableActions, goal, ref actionQueue, ref currentAction, ref currentGoal, ref goals);


        if (actionQueue == null || actionQueue.Count == 0)
        {
            if (currentGoal != null) // Check if currentGoal is not null before removing it
                goals.Remove(currentGoal);
        }
        // If no valid plan is found, return null
        if (!success)
        {
            Debug.Log("No plan");
            return null;
        }

        Node cheapest = null;
        // Find the leaf node with the lowest cost (cheapest plan)
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }

        // Reconstruct the plan from the leaf node to the root
        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        // Convert the plan into a queue for execution
        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction a in result)
        {
            queue.Enqueue(a);
        }

        // Print the plan
        Debug.Log("The plan is: ");
        foreach (GAction a in result)
        {
            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    // BuildGraph method recursively builds the planning graph by exploring possible paths to achieve the goal state
    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal, ref Queue<GAction> actionQueue, ref GAction currentAction, ref SubGoal currentGoal, ref Dictionary<SubGoal, int> goals)
    {
        bool foundPath = false;
       
        // Iterate over usable actions and check if they are achievable from the parent node's state
        foreach (GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                // Create a new state by applying the action's effects to the parent's state
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> eff in action.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key, eff.Value);
                }

                // Create a new node for the state and action, with the cumulative cost
                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                // If the goal is achieved, add the node to the leaves and mark the path as found
                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                // Otherwise, continue building the graph with a subset of actions
                else
                {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal, ref actionQueue, ref currentAction, ref currentGoal, ref goals); // Pass actionQueue here
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    // GoalAchieved method checks if the goal is achieved given the current state
    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        // Check if all key-value pairs in the goal are present in the state
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }
        return true;
    }

    // ActionSubset method returns a new list with the specified action removed
    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }
}
