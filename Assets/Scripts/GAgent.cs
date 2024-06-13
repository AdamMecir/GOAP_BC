using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Diagnostics;
using System;
// SubGoal class represents a sub-goal for the agent to achieve
public class SubGoal
{
    public Dictionary<string, int> sgoals; // Dictionary to store the sub-goal's conditions
    public bool remove; // Flag to indicate whether the sub-goal should be removed after achieving

    // Constructor to create a new sub-goal
    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i); // Add the condition to the sub-goal's dictionary
        remove = r; // Set the remove flag
    }
}

// GAgent class represents the agent with planning capabilities
public class GAgent : MonoBehaviour
{
    public WorldStates agentInventory;

    public UnityEngine.AI.NavMeshAgent agent;

    public List<GAction> actions = new List<GAction>();

    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    public GPlanner planner;

    public Animator animator;

    Queue<GAction> actionQueue;

    public GAction currentAction;

    SubGoal currentGoal;

    public TreeFinder treefinder;
    public WheatFinder wheatfinder;
    public StoneFinder stonefinder;
    public IronFinder ironfinder;

    public Inventory inventory;

    private Vector3 previousPosition;

    public bool TreeCollectorController = true;


    public float minSpeed = 2f;
    public float maxSpeed = 4f;

    float randomDuration = 0;

    public GameObject barrelObject;
    public MeshRenderer barrelRenderer;

    protected virtual void Start()
    {


        // Find Root object
        Transform rootTransform = transform.Find("Root");

        if (rootTransform != null)
        {
            // Find Hips object as a child of Root
            Transform hipsTransform = rootTransform.Find("Hips");

            if (hipsTransform != null)
            {
                // Find Barrel object as a child of Hips
                Transform barrelTransform = hipsTransform.Find("Barrel");

                if (barrelTransform != null)
                {
                    // Barrel object found
                    barrelObject = barrelTransform.gameObject;

                    // Accessing MeshRenderer component and setting it inactive
                    barrelRenderer = barrelObject.GetComponent<MeshRenderer>();
                    if (barrelRenderer != null)
                    {
                        barrelRenderer.enabled = false;
                        // or you can use barrelRenderer.gameObject.SetActive(false);
                        // if you want to deactivate the entire game object
                    }
                    else
                    {
                        // If MeshRenderer component not found
                        UnityEngine.Debug.LogWarning("MeshRenderer component not found on Barrel object.");
                    }
                }
                else
                {
                    // Barrel object not found
                    UnityEngine.Debug.LogWarning("Barrel object not found as a child of Hips.");
                }
            }
            else
            {
                // Hips object not found
                UnityEngine.Debug.LogWarning("Hips object not found as a child of Root.");
            }
        }
        else
        {
            // Root object not found
            UnityEngine.Debug.LogWarning("Root object not found.");
        }




        float randomSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.speed = randomSpeed;
        }
        animator = GetComponent<Animator>();
        treefinder = GameObject.Find("TreeFinder").GetComponent<TreeFinder>();
        wheatfinder = GetComponent<WheatFinder>();
        stonefinder = GameObject.Find("StoneFinder").GetComponent<StoneFinder>();
        ironfinder = GameObject.Find("IronFinder").GetComponent<IronFinder>();

        inventory = GetComponent<Inventory>();
        previousPosition = transform.position;



        GAction[] acts = GetComponents<GAction>();
        foreach (GAction a in acts)
            actions.Add(a);
    }




    bool invoked = false;
    void ContinueAction()
    {
        this.currentAction.PostPerform();
        invoked = false;
        SelectingTarget();
        // UnityEngine.Debug.Log("ContinueAction invoked");
    }

    void CompleteAction()
    {
        this.currentAction.running = false;
        this.currentAction.PostPerform();
        invoked = false;
        //UnityEngine.Debug.Log("CompleteAction invoked");

    }

    public void SetNewGoalShopOwner()
    {
        // Clear existing goals
        goals.Clear();
        SubGoal newGoal = new SubGoal("StockUp", 1, true);
        // Add the new goal with the specified priority
        goals.Add(newGoal, 1);

        // Re-plan to consider the new goal
        RePlan();

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------|

    public void SetNewGoalPlant()
    {
        // Clear existing goals
        goals.Clear();
        SubGoal newGoal = new SubGoal("isPlanting", 1, true);
        // Add the new goal with the specified priority
        goals.Add(newGoal, 1);

        // Re-plan to consider the new goal
        RePlan();
    }
    public void SetNewGoalWood()
    {
        // Clear existing goals
        goals.Clear();
        SubGoal newGoal = new SubGoal("isSelling", 1, true);
        // Add the new goal with the specified priority
        goals.Add(newGoal, 1);

        // Re-plan to consider the new goal
        RePlan();
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------|



    public void SetNewGoalPlantWheat()
    {
        // Clear existing goals
        goals.Clear();
        SubGoal newGoal = new SubGoal("isPlanting", 1, true);
        // Add the new goal with the specified priority
        goals.Add(newGoal, 1);

        // Re-plan to consider the new goal
        RePlan();
    }
    public void SetNewGoalFood()
    {
        // Clear existing goals
        goals.Clear();
        SubGoal newGoal = new SubGoal("isSelling", 1, true);
        // Add the new goal with the specified priority
        goals.Add(newGoal, 1);

        // Re-plan to consider the new goal
        RePlan();
    }





    //--------------------------------------------------------------------------------------------------------------------------------------------------|
    public void SetNewGoalStone()
    {
        // Clear existing goals
        goals.Clear();
        SubGoal newGoal = new SubGoal("isSelling", 1, true);
        // Add the new goal with the specified priority
        goals.Add(newGoal, 1);

        // Re-plan to consider the new goal
        RePlan();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------|
    public void SetNewGoalIron()
    {
        // Clear existing goals
        goals.Clear();
        SubGoal newGoal = new SubGoal("isSelling", 1, true);
        // Add the new goal with the specified priority
        goals.Add(newGoal, 1);

        // Re-plan to consider the new goal
        RePlan();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------|

    private void Update()
    {
        UpdateAI();
    }

    private void LateUpdate()
    {
        UpdateAnimations();
    }

    private void UpdateAI()
    {
        if (planner == null || actionQueue == null)
        {
            RePlan();
        }

        if (this.currentAction != null && this.currentAction.target != null && this.currentAction.running)
        {
            float distanceToTarget = Vector3.Distance(this.currentAction.target.transform.position, this.transform.position);

            if (this.agent.hasPath && distanceToTarget < 5f)
            {

                switch (this.currentAction)
                {
                    //----------------------------ShopOwnerFood------------------------------------------------------------|
                    case GoStockUpFoodMarket goStockUpFoodMarket when distanceToTarget < 4f:
                        HandleGoStockUpFoodMarketAction();
                        break;


             
                    case TakeFoodFromShop takeFoodFromShop when distanceToTarget < 5f:
                        HandleTakeFoodFromShopAction();
                        break;
                    //----------------------------ShopOwnerIron------------------------------------------------------------|
                    case GoStockUpIronMarket goStockUpIronMarket when distanceToTarget < 4f:
                        HandleGoStockUpIronMarketAction();
                        break;

          
        
                    case TakeIronFromShop takeIronFromShop when distanceToTarget < 5f:
                        HandleTakeIronFromShopAction();
                        break;
                    //----------------------------ShopOwnerWood------------------------------------------------------------|
                    case GoStockUpWoodMarket goStockUpWoodMarket when distanceToTarget < 4f:
                        HandleGoStockUpWoodMarketAction();
                        break;

      

   
                    case TakeWoodFromShop takeWoodFromShop when distanceToTarget < 5f:
                        HandleTakeWoodFromShopAction();
                        break;

                    //----------------------------ShopOwnerStone------------------------------------------------------------|
                    case GoStockUpStoneMarket goStockUpStoneMarket when distanceToTarget < 4f:
                        HandleGoStockUpStoneMarketAction();
                        break;

                        //This is used by all of owners
                    case GoToMarketCity goToMarketCity when distanceToTarget < 5f:
                        HandleGoToMarketCityAction();
                        break;

                   
                    case TakeStoneFromShop takeStoneFromShop when distanceToTarget < 5f:
                        HandleTakeStoneFromShopAction();
                        break;


                    //----------------------------IronMiner------------------------------------------------------------|
                    case GoToIronMine goToIronMineAction when distanceToTarget < 4f:
                        HandleGoToIronMineAction();
                        break;

                    case GoToMarketIron goToMarketIronAction when distanceToTarget < 5f:
                        HandleGoToMarketIronAction();
                        break;

                    case GoMakeDealIron goMakeDealIronAction when distanceToTarget < 5f:
                        HandleGoMakeDealIronAction();
                        break;
                    //----------------------------StoneMiner------------------------------------------------------------|
                    case GoToStonePit goToStonePitAction when distanceToTarget < 4f:
                        HandleGoToStonePitAction();
                        break;

                    case GoToMarketStone goToMarketStoneAction when distanceToTarget < 5f:
                        HandleGoToMarketStoneAction();
                        break;

                    case GoMakeDealStone goMakeDealStoneAction when distanceToTarget < 5f:
                        HandleGoMakeDealStoneAction();
                        break;
                    //----------------------------Lumberjack------------------------------------------------------------|
                    case GoToForest goToForestAction when distanceToTarget < 4f:
                        HandleGoToForestAction();
                        break;

                    case GoPlantTree goPlantTreeAction when distanceToTarget < 3f:
                        HandleGoPlantTreeAction();
                        break;

                    case GoToMarketWood goToMarketActionWood when distanceToTarget < 5f:
                        HandleGoToMarketWoodAction();
                        break;

                    case GoMakeDealWood goMakeDealWoodAction when distanceToTarget < 5f:
                        HandleGoMakeDealWoodAction();
                        break;

                    //----------------------------Farmer------------------------------------------------------------|
                    case GoToFarm goToFarmAction when distanceToTarget < 4f:
                        HandleGoToFarmAction();
                        break;

                    case GoPlantWheat goPlantWheatAction when distanceToTarget < 3f:
                        HandleGoPlantWheatAction();
                        break;

                    case GoToMarketFood goToMarketActionFood when distanceToTarget < 5f:
                        HandleGoToMarketFoodAction();
                        break;

                    case GoMakeDealFood goMakeDealFoodAction when distanceToTarget < 5f:
                        HandleGoMakeDealFoodAction();
                        break;

                    case GoToWindmill goToWindmillAction when distanceToTarget < 4f:
                        HandleGoToWindmillAction();
                        break;

                    // Add more cases for other action types if needed

                    default:
                        // Handle other action types if needed
                        break;
                }




                // ... handle other action types if needed ...
            }

            return;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            this.currentAction = actionQueue.Dequeue();

            SelectingTarget();


        }
        /*  if (actionQueue == null || actionQueue.Count == 0)
          {
              if (currentGoal != null)
              {
                  if (currentGoal.sgoals.ContainsKey("isSelling"))
                  {
                      // Reset the action queue and goal when "isSelling" goal is achieved
                      actionQueue = null;
                      goals.Remove(currentGoal);
                      currentGoal = null;
                  }
              }
          }*/
    }


    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoStockUpWoodMarketAction()
    {
        if (!invoked)
        {
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));

            SetNewGoalShopOwner();

        }
    }

    private void HandleTakeWoodFromShopAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoStockUpFoodMarketAction()
    {
        if (!invoked)
        {
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));

            SetNewGoalShopOwner();

        }
    }

    private void HandleTakeFoodFromShopAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoStockUpIronMarketAction()
    {
        if (!invoked)
        {
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));

            SetNewGoalShopOwner();

        }
    }

    private void HandleTakeIronFromShopAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoStockUpStoneMarketAction()
    {
        if (!invoked)
        {
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));

            SetNewGoalShopOwner();

        }
    }



    private void HandleGoToMarketCityAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }


    private void HandleTakeStoneFromShopAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoToIronMineAction()
    {
        if (this.inventory.AmountIron() >= this.inventory.desiredIronAmount - 1 && !invoked)
        {

            HandleInvoke("CompleteAction", UnityEngine.Random.Range(5f, 10f));
        }
        else if (!invoked)
        {

            // Handle other conditions for GoToForest
            HandleInvoke("ContinueAction", UnityEngine.Random.Range(5f, 10f));
        }
    }


    private void HandleGoToMarketIronAction()
    {
        if (!invoked)
        {

            // Handle conditions for GoToMarketWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));

            SetNewGoalIron();
            // Handle adding a new sub-goal if needed
        }
    }


    private void HandleGoMakeDealIronAction()
    {
        if (!invoked)
        {

            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));

        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoToStonePitAction()
    {
        if (this.inventory.AmountStone() >= this.inventory.desiredStoneAmount - 1 && !invoked)
        {
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(5f, 10f));
        }
        else if (!invoked)
        {
            // Handle other conditions for GoToForest
            HandleInvoke("ContinueAction", UnityEngine.Random.Range(5f, 10f));
        }
    }


    private void HandleGoToMarketStoneAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoToMarketWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            SetNewGoalStone();
            // Handle adding a new sub-goal if needed
        }
    }


    private void HandleGoMakeDealStoneAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoToForestAction()
    {
        if (this.inventory.AmountWood() >= this.inventory.desiredWoodAmount - 1 && !invoked)
        {
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(5f, 10f));
        }
        else if (!invoked)
        {
            // Handle other conditions for GoToForest
            HandleInvoke("ContinueAction", UnityEngine.Random.Range(5f, 10f));
        }
    }

    private void HandleGoPlantTreeAction()
    {

        if (!invoked)
        {
            // Adjusted conditions for planting wheat
            if (this.inventory.desiredWoodAmount == 1)
            {
                this.inventory.desiredWoodAmount -= 1;
                HandleInvoke("CompleteAction", UnityEngine.Random.Range(2f, 5f));
                SetNewGoalWood();
                SubGoal newGoal = new SubGoal("isSelling", 1, true);
                // Handle adding the sub-goal to the agent's goals
                UnityEngine.Debug.Log("Added new goal: " + newGoal);



            }
            else if (!invoked)
            {
                this.inventory.desiredWoodAmount -= 1;
                HandleInvoke("ContinueAction", UnityEngine.Random.Range(2f, 5f));

            }
        }

    }

    private void HandleGoToMarketWoodAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoToMarketWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            SetNewGoalPlant();
            // Handle adding a new sub-goal if needed
        }
    }


    private void HandleGoMakeDealWoodAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }


    //-------------------------------------------------------------------------------------------------------------------------------
    private void HandleGoToFarmAction()
    {
        if (this.inventory.AmountWheat() >= ((this.inventory.desiredBreadAmount) * 5) - 1 && !invoked)
        {

            HandleInvoke("CompleteAction", UnityEngine.Random.Range(2f, 3f));

        }
        else if (!invoked)
        {
            // Handle other conditions for GoToForest

            HandleInvoke("ContinueAction", UnityEngine.Random.Range(2f, 3f));

        }
    }

    private void HandleGoPlantWheatAction()
    {
        if (!invoked)
        {
            // Adjusted conditions for planting wheat
            if (wheatfinder.seedplaces.Count == 1)
            {
                HandleInvoke("CompleteAction", UnityEngine.Random.Range(2f, 3f));
                SetNewGoalFood();
                SubGoal newGoal = new SubGoal("isSelling", 1, true);
                // Handle adding the sub-goal to the agent's goals
                UnityEngine.Debug.Log("Added new goal: " + newGoal);
            }
            else if (!invoked)
            {
                HandleInvoke("ContinueAction", UnityEngine.Random.Range(2f, 3f));

            }
        }

    }

    private void HandleGoToMarketFoodAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoToMarketWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            SetNewGoalPlant();
            // Handle adding a new sub-goal if needed
        }
    }

    private void HandleGoMakeDealFoodAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoMakeDealWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }
    private void HandleGoToWindmillAction()
    {
        if (!invoked)
        {
            // Handle conditions for GoToMarketWood
            HandleInvoke("CompleteAction", UnityEngine.Random.Range(4f, 7f));
            // Handle adding a new sub-goal if needed
        }
    }

    private void HandleInvoke(string methodName, float duration)
    {
        randomDuration = duration;
        Invoke(methodName, randomDuration);
        invoked = true;
    }





    //-------------------------------------------------------------------------------------------------------------------------------
    private void SelectingTarget()
    {
        if (this.currentAction.PrePerform())
        {
            if (this.currentAction.target == null && currentAction.targetTag != "")
            {
                switch (this.currentAction)
                {
                    case GoToMarketCity goToMarketCity:
                        GoBuy();
                        break;


                    case GoStockUpFoodMarket goStockUpFoodMarket:
                        GoStockUpResources();
                        break;

                    case TakeFoodFromShop takeFoodFromShop:
                        GoTakeResources();
                        break;
                    //-------------------------------------------------------------------------------------------------------------------------------
                    case GoStockUpIronMarket goStockUpIronMarket:
                        GoStockUpResources();
                        break;

                    case TakeIronFromShop takeIronFromShop:
                        GoTakeResources();
                        break;
                    //-------------------------------------------------------------------------------------------------------------------------------
                    case GoStockUpWoodMarket goStockUpWoodMarket:
                        GoStockUpResources();
                        break;
                    case TakeWoodFromShop takeWoodFromShop:
                        GoTakeResources();
                        break;
                    //-------------------------------------------------------------------------------------------------------------------------------
                    case GoStockUpStoneMarket goStockUpStoneMarket:
                        GoStockUpResources();
                        break;
                    case TakeStoneFromShop takeStoneFromShop:
                        GoTakeResources();
                        break;
                    //-------------------------------------------------------------------------------------------------------------------------------
                    case GoToIronMine goToMineAction:
                        CollectIron();
                        break;

                    case GoToMarketIron goToMarketIronAction:
                        GoSellIron();
                        break;
                    //-------------------------------------------------------------------------------------------------------------------------------

                    case GoToStonePit goToStonePitAction:
                        CollectStone();
                        break;

                    case GoToMarketStone goToMarketStoneAction:
                        GoSellStone();
                        break;
                    //-------------------------------------------------------------------------------------------------------------------------------
                    case GoToForest goToForestAction:
                        CollectWood();
                        break;

                    case GoPlantTree goPlantTreeAction:
                        PlantTree();
                        break;

                    case GoToMarketWood goToMarketWoodAction:
                        GoSell();
                        break;
                    //-------------------------------------------------------------------------------------------------------------------------------
                    case GoToFarm goToFarmAction:
                        CollectWheat();
                        break;

                    case GoPlantWheat goPlantWheatAction:
                        PlantWheat();
                        break;

                    case GoToMarketFood goToMarketFoodAction:
                        GoSellWheat();
                        break;
                    case GoToWindmill goToWindmillAction:
                        GoBake();
                        break;


                    default:
                        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
                        break;
                }
            }

            if (this.currentAction.target != null)
            {
                this.currentAction.running = true;
                this.currentAction.agent.SetDestination(this.currentAction.target.transform.position);

            }
        }
        else
        {
            actionQueue = null;
        }
    }
    private void RePlan()
    {
        planner = new GPlanner(); // Initialize a new planner

        // Sort the goals based on priority (if applicable)
        var sortedGoals = from entry in goals orderby entry.Value descending select entry;

        foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
        {
            // Re-plan using the new goals and updated state
            actionQueue = planner.Plan(actions, sg.Key.sgoals, agentInventory, agent.transform.position, ref actionQueue, ref currentAction, ref currentGoal, ref goals);

            // If a valid plan is found, update the current goal and break the loop
            if (actionQueue != null)
            {
                currentGoal = sg.Key;
                break;
            }
        }

        // Handle cases where no valid plan is found
        if (actionQueue == null || actionQueue.Count == 0)
        {
            if (currentGoal != null)
                goals.Remove(currentGoal);
        }


    }

    private void UpdateAnimations()
    {
        float distanceTraveled = (transform.position - previousPosition).magnitude;

        if (currentAction != null && currentAction.running)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentAction.target.transform.position);

            if (currentAction is GoPlantWheat)
            {
                if (distanceTraveled != 0)
                {
                    animator.SetBool("IsPlanting", false);
                    animator.SetBool("IsHarvesting", false);
                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
                else if (distanceToTarget < 2f)
                {
                    animator.SetBool("IsPlanting", true);
                    animator.SetBool("IsHarvesting", false);
                    animator.SetBool("IsMoving", false);
                    animator.SetBool("IsSelling", false);
                }
                else
                {
                    animator.SetBool("IsPlanting", false);
                    animator.SetBool("IsHarvesting", false);
                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
            }
            else if (currentAction is GoPlantTree)
            {
                if (distanceTraveled != 0)
                {
                    animator.SetBool("IsPlanting", false);
                    animator.SetBool("IsChopping", false);
                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
                else if (distanceToTarget < 2f)
                {
                    animator.SetBool("IsPlanting", true);
                    animator.SetBool("IsChopping", false);
                    animator.SetBool("IsMoving", false);
                    animator.SetBool("IsSelling", false);
                }
                else
                {
                    animator.SetBool("IsPlanting", false);
                    animator.SetBool("IsChopping", false);
                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
            }
            else if (currentAction is GoToForest ||
                     currentAction is GoToFarm ||
                     currentAction is GoToIronMine ||
                     currentAction is GoToStonePit)
            {
                if (distanceTraveled != 0)
                {
                    if (currentAction is GoToForest ||
                     currentAction is GoToFarm)
                        animator.SetBool("IsPlanting", false);
                    if (currentAction is GoToForest)
                        animator.SetBool("IsChopping", false);
                    if (currentAction is GoToFarm)
                        animator.SetBool("IsHarvesting", false);
                    if (currentAction is GoToStonePit || currentAction is GoToIronMine)
                        animator.SetBool("IsMining", false);
                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
                else if (distanceToTarget < 2f)
                {
                    if (currentAction is GoToForest ||
                     currentAction is GoToFarm)
                        animator.SetBool("IsPlanting", false);
                    if (currentAction is GoToForest)
                        animator.SetBool("IsChopping", true);
                    if (currentAction is GoToFarm)
                    {


                        animator.SetBool("IsHarvesting", true);

                    }
                    if (currentAction is GoToStonePit || currentAction is GoToIronMine)
                    {


                        animator.SetBool("IsMining", true);

                    }


                    animator.SetBool("IsMoving", false);
                    animator.SetBool("IsSelling", false);
                }
                else
                {
                    if (currentAction is GoToForest ||
                     currentAction is GoToFarm)
                        animator.SetBool("IsPlanting", false);
                    if (currentAction is GoToForest)
                        animator.SetBool("IsChopping", false);
                    if (currentAction is GoToFarm)
                    {
                        animator.SetBool("IsHarvesting", false);
                    }
                    if (currentAction is GoToStonePit || currentAction is GoToIronMine)
                    {
                        animator.SetBool("IsMining", false);
                    }

                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
            }
            else if (currentAction is GoToMarketCity ||
                     currentAction is GoToMarketWood ||
                     currentAction is GoToMarketFood ||
                     currentAction is GoToMarketStone ||
                     currentAction is GoToMarketIron ||
                     currentAction is GoToWindmill ||
                     currentAction is GoMakeDealStone ||
                     currentAction is GoMakeDealIron ||
                     currentAction is GoMakeDealFood ||
                     currentAction is TakeStoneFromShop ||
                     currentAction is GoStockUpStoneMarket ||
                     currentAction is TakeIronFromShop ||
                     currentAction is GoStockUpIronMarket ||
                     currentAction is TakeFoodFromShop ||
                     currentAction is GoStockUpFoodMarket ||
                     currentAction is TakeWoodFromShop ||
                     currentAction is GoStockUpWoodMarket ||
                     currentAction is GoMakeDealWood)
            {
                if (distanceTraveled != 0)
                {
                    if (currentAction is GoToForest ||
                     currentAction is GoToFarm)
                        animator.SetBool("IsPlanting", false);
                    if (currentAction is GoMakeDealWood || currentAction is GoToMarketWood)
                        animator.SetBool("IsChopping", false);
                    if (currentAction is GoMakeDealFood || currentAction is GoToMarketFood || currentAction is GoToWindmill)
                        animator.SetBool("IsHarvesting", false);
                    if (currentAction is GoToMarketStone || currentAction is GoMakeDealStone || currentAction is GoToMarketIron || currentAction is GoMakeDealIron )
                        animator.SetBool("IsMining", false);

                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
                else if (distanceToTarget < 2f)
                {
                    if (currentAction is GoToForest ||
                     currentAction is GoToFarm)
                        animator.SetBool("IsPlanting", false);
                    if (currentAction is GoMakeDealWood || currentAction is GoToMarketWood)
                        animator.SetBool("IsChopping", false);
                    if (currentAction is GoMakeDealFood || currentAction is GoToMarketFood || currentAction is GoToWindmill)
                        animator.SetBool("IsHarvesting", false);
                    if (currentAction is GoToMarketStone || currentAction is GoMakeDealStone || currentAction is GoToMarketIron || currentAction is GoMakeDealIron)
                        animator.SetBool("IsMining", false);
                    animator.SetBool("IsMoving", false);
                    animator.SetBool("IsSelling", true);
                }
                else
                {
                    if (currentAction is GoToForest ||
                     currentAction is GoToFarm)
                        animator.SetBool("IsPlanting", false);
                    if (currentAction is GoMakeDealWood || currentAction is GoToMarketWood)
                        animator.SetBool("IsChopping", false);
                    if (currentAction is GoMakeDealFood || currentAction is GoToMarketFood || currentAction is GoToWindmill)
                        animator.SetBool("IsHarvesting", false);
                    if (currentAction is GoToMarketStone || currentAction is GoMakeDealStone || currentAction is GoToMarketIron || currentAction is GoMakeDealIron)
                        animator.SetBool("IsMining", false);
                    animator.SetBool("IsMoving", true);
                    animator.SetBool("IsSelling", false);
                }
            }
            else
            {
                if (currentAction is GoToForest ||
                     currentAction is GoToFarm)
                    animator.SetBool("IsPlanting", false);
                if (currentAction is GoMakeDealWood || currentAction is GoToMarketWood)
                    animator.SetBool("IsChopping", false);
                if (currentAction is GoMakeDealFood || currentAction is GoToMarketFood || currentAction is GoToWindmill)
                    animator.SetBool("IsHarvesting", false);
                if (currentAction is GoToMarketStone || currentAction is GoMakeDealStone || currentAction is GoToMarketIron || currentAction is GoMakeDealIron)
                    animator.SetBool("IsMining", false);
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsSelling", false);
            }
        }

        previousPosition = transform.position;
    }

    private void GoBuy()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }
    private void GoSell()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }
    private void GoBake()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }

    private void PlantTree()
    {

        if (treefinder.cuttedtrees.Count > 0)
        {
            GameObject nearestTree = FindNearestObject(treefinder.cuttedtrees);
            this.currentAction.target = nearestTree;
            this.currentAction.target.tag = "Owned";
        }
        else
        {
            // Handle no trees found
        }
    }

    private void CollectWood()
    {
        if (treefinder.trees.Count > 0)
        {
            GameObject nearestTree = FindNearestObject(treefinder.trees);
            this.currentAction.target = nearestTree;
            this.currentAction.target.tag = "Owned";
        }
        else
        {

        }
    }
    private void CollectIron()
    {
        if (ironfinder.irons.Count > 0)
        {
            GameObject nearestIron = FindNearestObject(ironfinder.irons);
            this.currentAction.target = nearestIron;
            this.currentAction.target.tag = "Owned";

        }
        else
        {

        }
    }
    private void CollectStone()
    {
        if (stonefinder.stones.Count > 0)
        {
            GameObject nearestStone = FindNearestObject(stonefinder.stones);
            this.currentAction.target = nearestStone;
            this.currentAction.target.tag = "Owned";
        }
        else
        {

        }
    }
    private void GoSellIron()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }
    //----------------------------------------------------------------------------------------------------|
    private void GoSellStone()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }
    private void GoTakeResources()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }
    private void GoStockUpResources()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }
    //----------------------------------------------------------------------------------------------------|
    private void GoSellWheat()
    {
        this.currentAction.target = GameObject.FindWithTag(this.currentAction.targetTag);
    }

    private void PlantWheat()
    {

        if (wheatfinder.seedplaces.Count > 0)
        {
            GameObject nearestWheat = FindNearestObject(wheatfinder.seedplaces);
            this.currentAction.target = nearestWheat;
            this.currentAction.target.tag = "Owned";
        }
        else
        {
            // Handle no trees found
        }
    }

    private void CollectWheat()
    {
        if (wheatfinder.wheats.Count > 0)
        {
            GameObject nearestWheat = FindNearestObject(wheatfinder.wheats);
            this.currentAction.target = nearestWheat;
            this.currentAction.target.tag = "Owned";
        }
        else
        {
            // Handle no trees found
        }
    }
    //-----------------------------------------------------------------------------------------------|
    private GameObject FindNearestObject(List<GameObject> objects)
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 agentPosition = transform.position;

        foreach (GameObject obj in objects)
        {
            // Skip objects with the "Owned" tag
            if (obj.CompareTag("Owned"))
            {
                continue;
            }

            float distance = Vector3.Distance(agentPosition, obj.transform.position);
            if (distance < closestDistance)
            {
                closestObject = obj;
                closestDistance = distance;
            }
        }

        return closestObject;
    }




}