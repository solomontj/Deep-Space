using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodingChallenge5 : MonoBehaviour
{
    [SerializeField] private TMP_Text editableText; // Reference to your TMP_Text component that displays editable text
    [SerializeField] private TextMeshProUGUI codeText; // Reference to your TextMeshProUGUI component that displays code text
    [SerializeField] private GameObject successIndicator; // Reference to the GameObject that should appear on success

    private string currentText = ""; // To keep track of the text and update only when it changes

    void Awake()
    {
        // Initially hide the success indicator
        if (successIndicator != null)
            successIndicator.SetActive(false);
    }

    void Update()
    {
        // Check if the text has changed since the last frame
        if (editableText.text != currentText)
        {
            currentText = editableText.text;
            HandleTextUpdated(currentText);
        }
    }

    private void HandleTextUpdated(string input)
    {
        // Automatically remove an underscore from the end of the input text if it exists
        if (input.EndsWith("_"))
        {
            input = input.Remove(input.Length - 1);
            editableText.text = input; // Update the text field to reflect this change
        }

        // Check if the first three characters are all digits and are not equal to each other
        if (input.Length >= 3 && char.IsDigit(input[0]) && char.IsDigit(input[1]) && char.IsDigit(input[2]) &&
            input[0] != input[1] && input[1] != input[2] && input[0] != input[2])
        {
            // If the first three digits are not equal, set the color to green for both texts
            editableText.color = Color.green;
            codeText.color = Color.green;

            // Show the success indicator
            if (successIndicator != null)
                successIndicator.SetActive(true);
        }
        else
        {
            // Reset the text color to white if the condition is not met
            editableText.color = Color.white;
            codeText.color = Color.red;

            // Hide the success indicator
            if (successIndicator != null)
                successIndicator.SetActive(false);
        }
    }

}
