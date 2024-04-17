using UnityEngine;
using TMPro;
using System.Collections;

public class RobotDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Ensure this is assigned.
    private string[] gameStartText =
    {
        "HELLO! I AM SPARKY THE ROBOT.",
        "I LOVE TO READ FILES.",
        "DRAG AND DROP ENCRYPTED FILES ON ME...",
        "AND I'LL CRACK THE CODE LIKE AN EGG!!!!"
    };
    public float textSpeed = 0.05f;
    private int index;
    private string[] currentDialogue;
    private bool robotDialogueTriggered = false;  // To ensure robot dialogue happens only once

    void Start()
    {
        textComponent.text = string.Empty;
        currentDialogue = gameStartText;
        gameObject.SetActive(true);  // Ensure the dialogue box is active
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);  // Activate the dialogue box if it's not active
        }
        index = 0;
        textComponent.text = string.Empty; // Clear the text component before starting new dialogue
        StopAllCoroutines();  // Ensure to stop any ongoing typing coroutine
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentDialogue[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < currentDialogue.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StopAllCoroutines();  // Stop current coroutine to ensure no overlap occurs
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogue();  // Call CloseDialogue when all lines are shown
        }
    }

    public void CloseDialogue()
    {
        StopAllCoroutines();  // Ensure to stop any ongoing typing coroutine
        textComponent.text = string.Empty; // Clear text after dialogue is closed
        gameObject.SetActive(false);  // Hide/disable the dialogue UI after the last line
    }

    public void HideDialogue()
    {
        StopAllCoroutines();
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    public void TriggerRobotDialogue()
    {
        if (!robotDialogueTriggered)
        { 
            robotDialogueTriggered = true;  // Ensure this dialogue doesn't trigger more than once
            gameObject.SetActive(true);  // Activate the dialogue box
            StartDialogue();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && textComponent.text.Length == currentDialogue[index].Length) // Check if the current line is fully displayed
        {
            NextLine();
        }
    }
}
