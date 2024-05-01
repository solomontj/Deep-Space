using UnityEngine;
using TMPro;
using System.Collections;

public class RobotDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
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
            robotDialogueTriggered = true;
            gameObject.SetActive(true);
            StartDialogue();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && textComponent.text.Length == currentDialogue[index].Length)
        {
            NextLine();
        }
    }
}