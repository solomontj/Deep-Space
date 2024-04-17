using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageMenu : MonoBehaviour
{
    [SerializeField] private GameObject contextMenuPrefab; // Assign the prefab in the Inspector
    [SerializeField] private GameObject decryptedFile1Display; // GameObject for Decrypted File 1
    [SerializeField] private GameObject decryptedFile2Display; // GameObject for Decrypted File 2
    [SerializeField] private GameObject decryptedFile3Display; // GameObject for Decrypted File 3

    private GameObject contextMenuInstance;
    private Item currItem;

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentItem();
    }

    private void UpdateCurrentItem()
    {
        if (InventoryManager.instance != null && InventoryManager.instance.item != currItem)
        {
            currItem = InventoryManager.instance.item; // Access the public 'item' from InventoryManager
            Debug.Log("Item Updated: " + currItem.itemName);
            UpdateDisplayBasedOnItem();
        }
    }

    private void UpdateDisplayBasedOnItem()
    {
        if (currItem != null)
        {
            decryptedFile1Display.SetActive(currItem.itemName == "Decrypted File 1");
            decryptedFile2Display.SetActive(currItem.itemName == "Decrypted File 2");
            decryptedFile3Display.SetActive(currItem.itemName == "Decrypted File 3");
        }
        else
        {
            decryptedFile1Display.SetActive(false);
            decryptedFile2Display.SetActive(false);
            decryptedFile3Display.SetActive(false);
        }
    }

    public void ShowContextMenu()
    {
        if (contextMenuPrefab != null && currItem != null)
        {
            contextMenuPrefab.SetActive(true);
            Debug.Log("Context Menu Shown for: " + currItem.itemName);
        }
        else
        {
            Debug.LogError("Context Menu Prefab or Current Item is not assigned.");
        }
    }

    public void HideContextMenu()
    {
        if (contextMenuPrefab != null)
        {
            contextMenuPrefab.SetActive(false);
            Debug.Log("Context Menu Hidden");
        }
        else
        {
            Debug.LogError("Context Menu Prefab is not assigned.");
        }
    }
}
