using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Progress;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Item[] startItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject mapImage, playerIconImage, cardImage;
    public Light2D flashlightLight;
    public bool flashCheck = false;
    int selectedSlot = -1;

    public AudioSource inventoryPickupAudioSource;
    public AudioSource flashlightToggleAudioSource;
    public GameObject contextMenuPrefab;
    private GameObject contextMenuInstance;
    public Item item;

    public void readFile(Item currItem)
    {
        if (currItem != null && currItem.itemName.StartsWith("Decrypted File"))
        {
            Debug.Log("Right-clicked with Decrypted File: " + currItem.itemName);
            if (contextMenuPrefab != null)
                contextMenuPrefab.SetActive(true);
            else
                Debug.LogError("MessageMenu is not assigned.");
        }
    }

    public void HideContextMenu()
    {
        if (contextMenuInstance != null)
        {
            contextMenuInstance.SetActive(false);
        }
        else
        {
            Debug.LogError("No context menu instance foun!");
        }
    }

    private bool isFlashlightInInventory = false;

    private void Awake()
    {
        instance = this;
        if (contextMenuInstance == null)
        {
            Debug.LogError("MessageMenu component is not found in the scene. Please assign it in the Inspector.");
        }
    }

    private void Start()
    {
        if (inventorySlots.Length > 0)
        {
            ChangeSelectedSlot(0);
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
                item = receivedItem;
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
                    else if (receivedItem.name == "Flashlight")
                    {
                        UpdateFlashlightLightStatus();
                    }
                    else if (receivedItem.itemName.StartsWith("Decrypted File"))
                    {
                        readFile(receivedItem);
                    }
                    else if (receivedItem.name == "Captain Card")
                    {
                        ToggleCardVisibility();
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

            InventoryItem itemInSlot = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Debug.Log("Selected item: " + itemInSlot.item.itemName);
            }
            else
            {
                Debug.Log("No item selected in the new slot.");
            }
        }
    }

    public void RemoveItem(InventoryItem itemToRemove)
    {

        Destroy(itemToRemove.gameObject);
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
    }

    private void ToggleCardVisibility()
    {
        bool isActive = !cardImage.activeInHierarchy;
        cardImage.SetActive(isActive);
    }

    private void TryUseBattery()
    {
        if (flashCheck)
        {
            GetSelectedItem(true);
        }
        else
        {
            Debug.Log("Can't Use Battery");
        }
    }

    private void ToggleFlashlightLight()
    {
        bool wasFlashlightOn = flashCheck;
        flashCheck = IsFlashlightHeld();

        if (wasFlashlightOn != flashCheck)
        {
            FlashLightLightSetActive(flashCheck);

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
            inventoryPickupAudioSource.Play();
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

    public Item GetDecryptedItemVersion(Item item)
    {
        return item;
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