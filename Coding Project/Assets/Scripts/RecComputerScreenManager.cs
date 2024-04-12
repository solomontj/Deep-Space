using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecComputerScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject recComputerScreen; // Assign this in the inspector

    // Method to open the recComputerScreen
    public void OpenScreen()
    {
        Debug.Log("Rec Computer Screen Opened");
        recComputerScreen.SetActive(true);
        Time.timeScale = 0;  // Pause the game when the screen is open
    }

    // Method to close the recComputerScreen
    public void CloseScreen()
    {
        recComputerScreen.SetActive(false);
        Time.timeScale = 1;  // Resume the game when the screen is closed
    }
}
