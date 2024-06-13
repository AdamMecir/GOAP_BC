using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxSystemStone : MonoBehaviour
{
    public GameObject ownerStoneShop; // GameObject that represents the OwnerStoneShop
    private Inventory thisInventory; // Inventory of this GameObject
    private Inventory ownerInventory; // Inventory of the OwnerStoneShop
    private float paymentInterval = 10.0f; // Time in seconds between payments
    private float timer; // Timer to track the interval

    void Start()
    {
        // Find the OwnerStoneShop GameObject
        ownerStoneShop = GameObject.FindGameObjectWithTag("OwnerStoneShop");
        if (ownerStoneShop == null)
        {
            UnityEngine.Debug.LogError("OwnerStoneShop not found in the scene!");
            return;
        }

        // Get the Inventory component from the OwnerStoneShop
        ownerInventory = ownerStoneShop.GetComponent<Inventory>();
        if (ownerInventory == null)
        {
            UnityEngine.Debug.LogError("No Inventory found on OwnerStoneShop!");
        }

        // Get the Inventory component from this GameObject
        thisInventory = GetComponent<Inventory>();
        if (thisInventory == null)
        {
            UnityEngine.Debug.LogError("No Inventory found on this GameObject!");
        }

        // Initialize timer
        timer = paymentInterval;
    }

    void Update()
    {
        if (thisInventory != null && ownerInventory != null)
        {
            // Update the timer each frame
            timer -= Time.deltaTime;

            // Check if it's time to pay
            if (timer <= 0)
            {
                // Reset timer
                timer = paymentInterval;

                // Perform the payment if there's enough money
                if (thisInventory.Money >= 1)
                {
                    thisInventory.Money -= 1; // Subtract 1 Money from this GameObject's inventory
                    ownerInventory.Money += 1; // Add 1 Money to the OwnerStoneShop's inventory
                    UnityEngine.Debug.Log("Paid 1 Money to OwnerStoneShop.");
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Not enough Money to pay the tax.");
                }
            }
        }
    }
}
