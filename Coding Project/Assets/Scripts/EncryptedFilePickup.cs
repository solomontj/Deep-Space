using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncryptedFilePickup : MonoBehaviour
{
    public Item[] pickupItems;         // Array of items that can be picked up, presumably encrypted files
    public InventoryManager inventoryManager; // Reference to the InventoryManager to call AddItem
    public SpriteRenderer spriteRenderer; // SpriteRenderer to control the visibility of the pickup
    private bool isInRange;            // Flag to check if the player is within the pickup range
    private bool isEmpty;              // Flag to check if the pickup has been collected
    private int counter;               // Counter to track how many files have been picked up at once

    private void Start()
    {
        counter = 0;                   // Initialize counter to zero
        isEmpty = false;               // Set isEmpty to false indicating the pickup is available
        spriteRenderer.enabled = true; // Ensure the sprite is visible at the start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isInRange)
        {
            if (!isEmpty)
            {
                Debug.Log($"Trying to pick up: {pickupItems[0].name}");
                bool result = inventoryManager.AddItem(pickupItems[0]);
                if (result)
                {
                    Debug.Log($"{pickupItems[0].name} added");
                }
                else
                {
                    Debug.Log($"Failed to add {pickupItems[0].name}");
                }
                isEmpty = true;
                spriteRenderer.enabled = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure that the trigger is exited by the player
        {
            isInRange = false; // Set isInRange to false when player exits the trigger
        }
    }
}
