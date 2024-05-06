using TMPro;
using UnityEngine;

public class CodingChallenge6 : MonoBehaviour
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

        input = input.ToUpper();

        if (input == "A>B" || input == "B<A" || input == "A > B" || input == "B > A")
        {

            editableText.color = Color.green;
            codeText.color = Color.green;

            if (successIndicator != null)
                successIndicator.SetActive(true);

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