using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RobotOptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private Image selectedItem;
    [SerializeField] private TMP_Text selectedItemText;
    [SerializeField] private Image selectedItemImage;
    [SerializeField] private Sprite cancelSprite;
    [SerializeField] private GameObject decryptFileButtonGroup;
    [SerializeField] private Button decryptButton;

    private InventoryItem currentlySelectedInventoryItem;
    private bool isMenuEnabled = false;

    private void Start()
    {
        SetNoItemSelected();
    }

    public void OpenSettingsWithItem(string itemName, Sprite itemSprite, InventoryItem inventoryItem)
    {
        if (!isMenuEnabled)
        {
            Debug.LogWarning("Menu is disabled.");
            return;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            SetNoItemSelected();
        }
        else
        {
            selectedItemText.text = itemName;
            selectedItemImage.sprite = itemSprite;
            currentlySelectedInventoryItem = inventoryItem;
            Debug.Log($"Received {itemName}, opening settings menu.");
            decryptFileButtonGroup.SetActive(itemName.StartsWith("Encrypted File"));
        }

        optionMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Settings()
    {
        if (!isMenuEnabled)
        {
            Debug.LogWarning("Menu is disabled.");
            return;
        }

        SetNoItemSelected();
        Debug.Log("Settings clicked");
        optionMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        SetNoItemSelected();
        optionMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetNoItemSelected()
    {
        selectedItemText.text = "No Item Selected";
        selectedItemImage.sprite = cancelSprite;
        decryptFileButtonGroup.SetActive(false);
    }

    public void DecryptFile()
    {
        if (!isMenuEnabled)
        {
            Debug.LogWarning("Menu is disabled.");
            return;
        }

        if (currentlySelectedInventoryItem != null && selectedItemText.text.StartsWith("Encrypted File"))
        {
            currentlySelectedInventoryItem.DecryptItem(currentlySelectedInventoryItem.item);
            Debug.Log("File decrypted successfully.");
            SetNoItemSelected();
        }
    }

    public void EnableMenu()
    {
        isMenuEnabled = true;
        Debug.Log("Menu has been enabled.");
    }
}