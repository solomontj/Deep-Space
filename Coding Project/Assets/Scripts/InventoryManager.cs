using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Item[] startItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject mapImage, playerIconImage, exitButtonImage;
    public bool flashCheck = false;
    int selectedSlot = -1;

    private void Awake(){
        instance = this;
    }

    private void Start() {
        ChangeSelectedSlot(0);
        foreach(var item in startItems) {
            if (item.name == "Battery") {
                for(int i=0; i<3; i++){
                    AddItem(item);
                }
            }
            else {
                AddItem(item);
            }
            
        }
    }

    private void Update() {
        if (Input.inputString != null) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                Item receivedItem = GetSelectedItem(false);
                if(receivedItem != null) {
                    Debug.Log("Using " + receivedItem.name);
                    if (receivedItem.name == "Map") {
                        if(mapImage.activeInHierarchy == false) {
                            mapImage.SetActive(true);
                            playerIconImage.SetActive(true);
                            exitButtonImage.SetActive(true);
                        }
                        else {
                            mapImage.SetActive(false);
                            playerIconImage.SetActive(false);
                            exitButtonImage.SetActive(false);
                        }
                    }
                    else if (receivedItem.name == "Battery") {
                        if (flashCheck == true)
                        {
                            Item removedItem = GetSelectedItem(true);
                        }
                        else {
                            Debug.Log("Can't Use Battery");
                        }

                    }
                }
                else {
                    Debug.Log("No Item Selected");
                }
            }
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 10) {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue) {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item) {
        for (int i = 0; i<inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.item.stackable == true) {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        
        for (int i = 0; i<inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot) {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use) {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null) {
            Item item = itemInSlot.item;
            if(use == true) {
                itemInSlot.count--;
                if(itemInSlot.count<=0) {
                    Destroy(itemInSlot.gameObject);
                }
                else{
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
}
