using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public Item[] pickupItems;
    public InventoryManager inventoryManager;
    public SpriteRenderer spriteRenderer;
    private bool isInRange;
    private bool isEmpty;
    private int counter;
    private float randomNumber;

    private void Start()
    {
        foreach (var item in pickupItems)
        {
            Debug.Log("Loaded pickup item: " + item.name);
        }
        counter = 0;
        randomNumber = Random.Range(1, 4);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isInRange) {
            if (!isEmpty) {
                for(int i=0; i<randomNumber; i++){
                    inventoryManager.AddItem(pickupItems[0]);
                    counter++;
                }
                if (counter > 1){
                    Debug.Log(counter + " Batteries Added");
                }
                else {
                    Debug.Log("1 Battery Added");
                }
                isEmpty = true;
                this.spriteRenderer.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isInRange = false;
    }
}
