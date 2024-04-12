using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject captainComputerScreen; // Assign this in the inspector

    // Method to open the recComputerScreen
    public void OpenScreen()
    {
        Debug.Log("Rec Computer Screen Opened");
        captainComputerScreen.SetActive(true);
        Time.timeScale = 0;  // Pause the game when the screen is open
    }

    // Method to close the recComputerScreen
    public void CloseScreen()
    {
        captainComputerScreen.SetActive(false);
        Time.timeScale = 1;  // Resume the game when the screen is closed
    }
}
