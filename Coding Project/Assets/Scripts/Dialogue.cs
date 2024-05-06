using UnityEngine;
using TMPro;
using System.Collections;

public class CharacterMonologue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] gameStartText =
    {
        "Ah, my memory...",
        "This looks like the old cryochamber room.",
        "It's hard to see.",
        "If only I had some source of light..."
    };
    private string[] robotInteraction =
    {
        "Wow! It's Sparky the Robot...",
        "but he's not moving...",
        "Let's check out his code on the computer.",
        "Maybe he can show us what's really in these files."
    };
    private string[] lightsOnText =
    {
        "Finally, some lights...",
        "No longer will be needing that flashlight then.",
        "Lets do some searching around this ship."
    };
    public float textSpeed = 0.05f;
    private int index;
    private string[] currentDialogue;
    private bool robotDialogueTriggered = false;

    void Start()
    {
        textComponent.text = string.Empty;
        currentDialogue = gameStartText;
        gameObject.SetActive(true);
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        index = 0;
        textComponent.text = string.Empty;
        StopAllCoroutines();
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
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        StopAllCoroutines();
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
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
            robotDialogueTriggered = true;
            gameObject.SetActive(true);
            StartDialogue();
        }
    }

    public void TriggerLightsOnDialogue()
    {
        currentDialogue = lightsOnText;
        gameObject.SetActive(true);
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && textComponent.text.Length == currentDialogue[index].Length)
        {
            NextLine();
        }
    }
}