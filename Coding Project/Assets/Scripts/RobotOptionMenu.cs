using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RobetOptionMenu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    public void Settings()
    {
        optionMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        optionMenu.SetActive(false);
        Time.timeScale = 1;

    }
}
