using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordChallenge4 : MonoBehaviour
{
    [SerializeField] private TMP_Text editableText;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private GameObject successIndicator;
    [SerializeField] private GameObject successIndicator2;
    [SerializeField] private GameObject successIndicator3;
    [SerializeField] private GameObject successIndicator4;
    [SerializeField] private GameObject successIndicator5;

    private string currentText = "";

    void Awake()
    {

        if (successIndicator != null)
            successIndicator.SetActive(false);
    }

    void Update()
    {

        if (editableText.text != currentText)
        {
            currentText = editableText.text;
            HandleTextUpdated(currentText);
        }
    }

    private void HandleTextUpdated(string input)
    {

        if (input.EndsWith("_"))
        {
            input = input.Remove(input.Length - 1);
            editableText.text = input;
        }

        input = input.ToUpper();

        if (input == "STARS")
        {

            editableText.color = Color.green;
            codeText.color = Color.green;

            if (successIndicator != null)
                successIndicator.SetActive(true);
            successIndicator2.SetActive(false);
            successIndicator3.SetActive(true);
            successIndicator4.SetActive(false);
            successIndicator5.SetActive(true);

            Debug.Log("Correct input received: " + input);
        }
        else
        {

            editableText.color = Color.white;
            codeText.color = Color.red;

            if (successIndicator != null)
                successIndicator.SetActive(false);

            Debug.Log("Incorrect input. Current input: " + input);
        }
    }
}