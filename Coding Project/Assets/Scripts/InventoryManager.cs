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

    private bool isFlashlightInInventory = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
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
        if (Input.inputString != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Item receivedItem = GetSelectedItem(false);
                if (receivedItem != null)
                {
                    Debug.Log("Using " + receivedItem.name);
                    if (receivedItem.name == "Map")
                    {
                        if (mapImage.activeInHierarchy == false)
                        {
                            mapImage.SetActive(true);
                            playerIconImage.SetActive(true);
                            exitButtonImage.SetActive(true);
                        }
                        else
                        {
                            mapImage.SetActive(false);
                            playerIconImage.SetActive(false);
                            exitButtonImage.SetActive(false);
                        }
                    }
                    else if (receivedItem.name == "Battery")
                    {
                        if (flashCheck == true)
                        {
                            Item removedItem = GetSelectedItem(true);
                        }
                        else
                        {
                            Debug.Log("Can't Use Battery");
                        }

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
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 10)
            {
                ChangeSelectedSlot(number - 1);
                // Update flashlight status whenever the selected slot changes.
                UpdateFlashlightLightStatus();
            }
        }
        ToggleFlashlightLight();
    }

    private void ToggleFlashlightLight()
    {
        // Assuming the flashCheck is true when the flashlight is being held.
        if (IsFlashlightHeld())
        {
            flashCheck = true;
            FlashLightLightSetActive(true);  // Method to enable the flashlight
        }
        else
        {
            flashCheck = false;
            FlashLightLightSetActive(false); // Method to disable the flashlight
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

    void UpdateFlashlightLightStatus()
    {
        bool isHoldingFlashlight = IsFlashlightHeld();
        flashlightLight.enabled = isHoldingFlashlight && isFlashlightInInventory;
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        if (item.name == "Flashlight" && !isFlashlightInInventory)
        {
            Debug.Log("Flashlight added to inventory.");
            isFlashlightInInventory = true;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
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
            if (use == true)
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

    public bool IsFlashlightHeld()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null && itemInSlot.item.name == "Flashlight")
        {
            return true;
        }
        return false;
    }
}