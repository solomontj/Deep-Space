using UnityEngine;
using TMPro;
using System.Collections;

public class CharacterMonologue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Ensure this is assigned.
    public string[] gameStartText =
    {
        "Ah, my memory...",
        "This looks like the old cryochamber room.",
        "It's hard to see.",
        "If only I had some source of light..."
    };
    public string[] robotInteraction =
    {
        "Hmm. Looks like the droid still works.",
        "Maybe if I can find the ship's info, I can give it to the robot to decrypt.",
        "Let's gather some."
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
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogue();  // Call CloseDialogue when all lines are shown
        }
    }

    public void CloseDialogue()
    {
        gameObject.SetActive(false);  // Hide/disable the dialogue UI after the last line
        textComponent.text = string.Empty; // Clear text after dialogue is closed
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
            currentDialogue = robotInteraction;
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
