using UnityEngine;
using TMPro;

public class AutoEncapsulateInput : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textDisplay;  // Reference to your TMP Text

    private string currentText = ""; // To keep track of the actual text without formatting

    private void Awake()
    {
        // Initialize the text with encapsulation if needed
        if (!string.IsNullOrEmpty(textDisplay.text))
        {
            currentText = StripFormatting(textDisplay.text);
            UpdateTextDisplay();
        }
    }

    private void Update()
    {
        // Check if the text has changed since the last frame by comparing unformatted versions
        string newText = StripFormatting(textDisplay.text);
        if (newText != currentText)
        {
            currentText = newText;
            UpdateTextDisplay();
        }
    }

    // Update the visible text display to include formatting
    private void UpdateTextDisplay()
    {
        if (!string.IsNullOrEmpty(currentText))
        {
            textDisplay.text = $"({currentText});"; // Encapsulate with parentheses and add semicolon
        }
        else
        {
            textDisplay.text = "();"; // Default text with empty parentheses and semicolon
        }
    }

    // Remove formatting from the displayed text to check against the internal representation
    private string StripFormatting(string formattedText)
    {
        if (formattedText.StartsWith("(") && formattedText.EndsWith(");"))
        {
            return formattedText.Substring(1, formattedText.Length - 3);
        }
        return formattedText;
    }
}
