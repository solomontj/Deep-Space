using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodingChallenge1 : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField; // Assign in inspector
    [SerializeField] private TextMeshProUGUI codeText; // Assign in inspector

    private void Awake()
    {
        // Add a listener to catch when the input field's content changes
        inputField.onValueChanged.AddListener(HandleInputChanged);
    }

    private void HandleInputChanged(string input)
    {
        if (int.TryParse(input, out int number) && number > 10)
        {
            // If input is a number greater than 10, set the color to green
            inputField.textComponent.color = Color.green;
            codeText.color = Color.green;
        }
        else
        {
            // If input is not a number greater than 10, reset color to default (black)
            inputField.textComponent.color = Color.black;
            codeText.color = Color.black;
        }
    }

    // Ensure to remove the listener when the GameObject is destroyed
    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(HandleInputChanged);
    }
}
