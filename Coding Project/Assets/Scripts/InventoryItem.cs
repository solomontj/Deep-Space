using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    [Header("UI")]
    public Image image;
    public Text countText;
    public Item item;
    [HideInInspector] public Transform parentAfterDrag;
    public int count = 1;

    public void InitialiseItem(Item newItem) {
        item = newItem;
        this.image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount() {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag(PointerEventData eventData){
        this.image.color = new Color32(255, 255, 255, 120);
        this.image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData){
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        this.image.color = new Color32(255, 255, 255, 255);
        this.image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    
    void Awake(){
        image = GetComponent<Image>();
    }
}
