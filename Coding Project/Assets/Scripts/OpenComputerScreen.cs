using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecComputerScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject recComputerScreen;

    public void OpenScreen()
    {
        Debug.Log("Rec Computer Screen Opened");
        recComputerScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseScreen()
    {
        recComputerScreen.SetActive(false);
        Time.timeScale = 1;
    }
}