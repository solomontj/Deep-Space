using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageMenu : MonoBehaviour
{
    [SerializeField] private GameObject contextMenuPrefab; // Assign the prefab in the Inspector
    private GameObject contextMenuInstance;

    public void ShowContextMenu()
    {
        contextMenuPrefab.SetActive(true);
    }

    public void HideContextMenu()
    {
        Debug.Log("CLOSE");
        contextMenuPrefab.SetActive(false);
    }
}
