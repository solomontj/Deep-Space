using TMPro;  // Import the TextMesh Pro namespace
using UnityEngine;

public class ClearText : MonoBehaviour
{
    public TMP_Text textComponent;  // Reference to the TextMesh Pro component

    public void Clear()
    {
        Debug.Log("Clear method called");  // Log to confirm method is called
        if (textComponent == null)
        {
            Debug.LogError("Text component is not assigned!");
            return;
        }
        textComponent.text = "";  // Clear the text
    }

}
