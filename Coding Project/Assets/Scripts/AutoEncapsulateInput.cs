using UnityEngine;
using TMPro;

public class AutoEncapsulateInput : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textDisplay;

    private string currentText = "";

    private void Awake()
    {

        if (!string.IsNullOrEmpty(textDisplay.text))
        {
            currentText = StripFormatting(textDisplay.text);
            UpdateTextDisplay();
        }
    }

    private void Update()
    {

        string newText = StripFormatting(textDisplay.text);
        if (newText != currentText)
        {
            currentText = newText;
            UpdateTextDisplay();
        }
    }

    private void UpdateTextDisplay()
    {
        if (!string.IsNullOrEmpty(currentText))
        {
            textDisplay.text = $"({currentText});";
        }
        else
        {
            textDisplay.text = "();";
        }
    }

    private string StripFormatting(string formattedText)
    {
        if (formattedText.StartsWith("(") && formattedText.EndsWith(");"))
        {
            return formattedText.Substring(1, formattedText.Length - 3);
        }
        return formattedText;
    }
}