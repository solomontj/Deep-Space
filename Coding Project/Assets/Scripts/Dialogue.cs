using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMonologue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private string[] lines =
    {
        "Ah, my memory...",
        "This looks like the old cryochamber room.",
        "It's hard to see.",
        "If only I had some source of light..."
    };
    public float textSpeed = 0.05f; // Adjust as needed for pacing
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // Here you could also trigger events that happen after the dialogue ends, such as enabling a light source or changing the scene.
            gameObject.SetActive(false); // Hide or disable the dialogue UI after the last line
        }
    }
}