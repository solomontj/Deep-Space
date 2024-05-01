using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject captainComputerScreen;

    public void OpenScreen()
    {
        Debug.Log("Rec Computer Screen Opened");
        captainComputerScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseScreen()
    {
        captainComputerScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
