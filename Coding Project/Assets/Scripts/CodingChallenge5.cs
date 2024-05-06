using TMPro;
using UnityEngine;

public class CodingChallenge5 : MonoBehaviour
{
    [SerializeField] private TMP_Text editableText;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private GameObject successIndicator;

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

        if (input.Length >= 3 && char.IsDigit(input[0]) && char.IsDigit(input[1]) && char.IsDigit(input[2]) &&
            input[0] != input[1] && input[1] != input[2] && input[0] != input[2])
        {

            editableText.color = Color.green;
            codeText.color = Color.green;

            if (successIndicator != null)
                successIndicator.SetActive(true);
        }
        else
        {

            editableText.color = Color.white;
            codeText.color = Color.red;

            if (successIndicator != null)
                successIndicator.SetActive(false);
        }
    }

}