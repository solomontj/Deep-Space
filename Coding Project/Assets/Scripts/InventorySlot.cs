using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem != null)
        {
            InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();

            // Check if the slot already contains an item
            if (itemInSlot != null)
            {
                // Swap items between slots
                Transform droppedItemOriginalParent = droppedItem.originalParent;
                droppedItem.originalParent = itemInSlot.originalParent;
                itemInSlot.originalParent = droppedItemOriginalParent;

                droppedItem.parentAfterDrag = droppedItem.originalParent;
                itemInSlot.parentAfterDrag = itemInSlot.originalParent;

                droppedItem.transform.SetParent(droppedItem.parentAfterDrag);
                droppedItem.transform.position = droppedItem.parentAfterDrag.position;

                itemInSlot.transform.SetParent(itemInSlot.parentAfterDrag);
                itemInSlot.transform.position = itemInSlot.parentAfterDrag.position;
            }
            else
            {
                // If the slot is empty, simply place the dragged item into it
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.position = transform.position;
                droppedItem.parentAfterDrag = transform;
            }
        }
    }
}

