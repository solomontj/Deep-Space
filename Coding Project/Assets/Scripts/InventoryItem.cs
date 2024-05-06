using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;
    public Text countText;
    public Item item;
    [HideInInspector] public Transform parentAfterDrag;
    public Transform originalParent;
    public bool isFromHotbar;
    public int count = 1;

    [Header("Decryption")]
    [SerializeField] public Item decryptedFile1;
    [SerializeField] public Item decryptedFile2;
    [SerializeField] public Item decryptedFile3;

    [SerializeField] private Transform toolbarTransform;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        this.image.sprite = newItem.image;
        RefreshCount();
    }

    public void DecryptItem(Item decryptItem)
    {
        if (decryptItem == null)
        {
            Debug.LogError("DecryptItem was called but decryptItem is null.");
            return;
        }

        Debug.Log(decryptItem.itemName);

        if (InventoryManager.instance == null)
        {
            Debug.LogError("InventoryManager.instance is null.");
            return;
        }

        if (decryptItem.itemName == "Encrypted File 1")
        {
            InventoryManager.instance.RemoveItem(this);
            InventoryManager.instance.AddItem(decryptedFile1);
            Debug.Log("GIVING PLAYER DECRYPT 1");
        }
        else if (decryptItem.itemName == "Encrypted File 2")
        {
            InventoryManager.instance.RemoveItem(this);
            InventoryManager.instance.AddItem(decryptedFile2);
            Debug.Log("GIVING PLAYER DECRYPT 2");
        }
        else if (decryptItem.itemName == "Encrypted File 3")
        {
            InventoryManager.instance.RemoveItem(this);
            InventoryManager.instance.AddItem(decryptedFile3);
            Debug.Log("GIVING PLAYER DECRYPT 3");
        }
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        isFromHotbar = originalParent == toolbarTransform;
        image.color = new Color32(255, 255, 255, 120);
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255);
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if (parentAfterDrag != originalParent)
        {
            PerformAction();
        }
    }

    void PerformAction()
    {
        if (isFromHotbar)
        {

        }
        isFromHotbar = false;
    }
}