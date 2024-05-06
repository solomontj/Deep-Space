using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Keys : MonoBehaviour
{
    [SerializeField] private string keyValue = "";

    private TMPro.TMP_Text text;

    private Image image;

    private AudioSource keyAudioSFX;

    private void Start()
    {
        image = GetComponent<Image>();
        text = TextBlock.instance.textBlock;

        keyAudioSFX = GetComponent<AudioSource>();

        Debug.Log("Key Initialized: " + keyValue);
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse down on key: " + keyValue);
        OnKeyPressed();
    }

    public void OnKeyPressed()
    {
        Debug.Log("Key pressed: " + keyValue);

        keyAudioSFX.Play();

        if (keyValue == "back")
        {
            if (text.text.Length > 0)
            {
                text.text = text.text.Remove(text.text.Length - 1);
                Debug.Log("Backspace pressed. New text: " + text.text);
            }
            else
            {
                Debug.LogWarning("Backspace pressed but text is already empty.");
            }
        }
        else if (keyValue == "enter") //change line + ()
        {
            text.text += "\n";
            Debug.Log("Enter pressed. New text: " + text.text);
        }
        else if (keyValue == "capslock")
        {
            TextBlock.instance.isCapslockOn = !TextBlock.instance.isCapslockOn;
            Debug.Log("Capslock toggled. Capslock is now " + (TextBlock.instance.isCapslockOn ? "ON" : "OFF"));
        }
        else
        {
            string newText = TextBlock.instance.isCapslockOn ? keyValue.ToUpper() : keyValue;
            text.text += newText;
            Debug.Log("Added character '" + newText + "'. New text: " + text.text);
        }

        StartCoroutine(on_key_pressed());
    }

    IEnumerator on_key_pressed()
    {
        Debug.Log("Key visual feedback started for key: " + keyValue);
        image.color = Color.grey;

        yield return new WaitForSeconds(.15f);

        image.color = Color.white;
        Debug.Log("Key visual feedback ended for key: " + keyValue);
    }
}
