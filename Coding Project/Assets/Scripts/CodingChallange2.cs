using TMPro;
using UnityEngine;

public class CodingChallenge2 : MonoBehaviour
{
    [SerializeField] private TMP_Text editableText;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private GameObject successIndicator;

    private string currentText = "";

    void Awake()
    {
        if (successIndicator != null) successIndicator.SetActive(false);
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

        if (int.TryParse(input, out int number) && number > 100)
        {

            editableText.color = Color.green;
            codeText.color = Color.green;

            if (successIndicator != null) successIndicator.SetActive(true);
        }
        else
        {

            editableText.color = Color.white;
            codeText.color = Color.red;

            if (successIndicator != null) successIndicator.SetActive(false);
        }
    }
}