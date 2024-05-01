using TMPro;
using UnityEngine;

public class ClearText : MonoBehaviour
{
    public TMP_Text textComponent;

    public void Clear()
    {
        Debug.Log("Clear method called");
        if (textComponent == null)
        {
            Debug.LogError("Text component is not assigned!");
            return;
        }
        textComponent.text = "";
    }

}
