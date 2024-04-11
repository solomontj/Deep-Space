using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Item[] startItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject mapImage, playerIconImage, exitButtonImage;
    public Light2D flashlightLight;
    public bool flashCheck = false;
    int selectedSlot = -1;

    public AudioSource inventoryPickupAudioSource;
    public AudioSource flashlightToggleAudioSource;

    private bool isFlashlightInInventory = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (inventorySlots.Length > 0)
        {
            ChangeSelectedSlot(0);  // Ensure there's at least one slot to select
        }
        foreach (var item in startItems)
        {
            if (item.name == "Battery")
            {
                for (int i = 0; i < 3; i++)
                {
                    AddItem(item);
                }
            }
            else
            {
                AddItem(item);
            }
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(Input.inputString))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Item receivedItem = GetSelectedItem(false);
                if (receivedItem != null)
                {
                    Debug.Log("Using " + receivedItem.name);
                    if (receivedItem.name == "Map")
                    {
                        ToggleMapVisibility();
                    }
                    else if (receivedItem.name == "Battery")
                    {
                        TryUseBattery();
                    }
                    if (receivedItem.name == "Flashlight")
                    {
                        UpdateFlashlightLightStatus();
                    }
                }
                else
                {
                    Debug.Log("No Item Selected");
                }
            }

            if (int.TryParse(Input.inputString, out int number) && number > 0 && number <= inventorySlots.Length)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
        ToggleFlashlightLight();
    }

    private void ChangeSelectedSlot(int newSlot)
    {
        if (newSlot >= 0 && newSlot < inventorySlots.Length)
        {
            if (selectedSlot >= 0)
            {
                inventorySlots[selectedSlot].Deselect();
            }
            inventorySlots[newSlot].Select();
            selectedSlot = newSlot;
        }
    }

    private void UpdateFlashlightLightStatus()
    {
        if (IsFlashlightHeld() && isFlashlightInInventory)
        {
            flashlightLight.enabled = true;
        }
        else
        {
            flashlightLight.enabled = false;
        }
    }

    private void ToggleMapVisibility()
    {
        bool isActive = !mapImage.activeInHierarchy;
        mapImage.SetActive(isActive);
        playerIconImage.SetActive(isActive);
        exitButtonImage.SetActive(isActive);
    }

    private void TryUseBattery()
    {
        if (flashCheck)
        {
            GetSelectedItem(true);  // This assumes that using a battery consumes it
        }
        else
        {
            Debug.Log("Can't Use Battery");
        }
    }

    private void ToggleFlashlightLight()
    {
        bool wasFlashlightOn = flashCheck;  // Store the old state
        flashCheck = IsFlashlightHeld();    // Update the current state based on whether the flashlight is held

        // Check if the state changed
        if (wasFlashlightOn != flashCheck)
        {
            FlashLightLightSetActive(flashCheck);  // Update the light state

            // Play the toggle sound only if the state changes
            if (flashlightToggleAudioSource && flashlightToggleAudioSource.clip)
            {
                flashlightToggleAudioSource.Play();
            }
            else
            {
                Debug.LogError("Flashlight toggle AudioSource not properly configured or missing AudioClip.");
            }
        }
    }


    public void FlashLightLightSetActive(bool isActive)
    {
        if (flashlightLight != null)
        {
            flashlightLight.enabled = isActive;
        }
        else
        {
            Debug.LogError("FlashlightLight is not assigned in the inspector.");
        }
    }

    public bool IsFlashlightHeld()
    {
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            InventorySlot slot = inventorySlots[selectedSlot];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            return itemInSlot != null && itemInSlot.item.name == "Flashlight";
        }
        return false;
    }


    public bool AddItem(Item item)
    {
        bool itemAdded = false;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.item.stackable)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                itemAdded = true;
                Debug.Log($"Added another {item.name} to existing slot.");
                break;
            }
        }

        if (!itemAdded)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                if (slot.GetComponentInChildren<InventoryItem>() == null)
                {
                    SpawnNewItem(item, slot);
                    Debug.Log($"Spawned new {item.name} in empty slot.");
                    itemAdded = true;
                    break;
                }
            }
        }

        if (itemAdded)
        {
            inventoryPickupAudioSource.Play();  // Play inventory pickup sound
            Debug.Log($"{item.name} picked up and audio played.");
        }
        else
        {
            Debug.LogError($"Failed to add {item.name} to the inventory.");
        }

        return itemAdded;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
}
