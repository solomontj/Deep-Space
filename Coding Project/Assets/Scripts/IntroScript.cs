using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroScript : MonoBehaviour
{
    public Image[] images; // Assign all your images in the inspector
    public Image[] subtitles;
    public Button nextSceneButton; // Assign this in the inspector
    public AudioSource[] audioSources;
    public AudioSource nextButton;
    public float delayBetweenImages = 2f; // Delay in seconds between images

    //private int currentImageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initially disable all images
        foreach (var image in images)
        {
            if (image != null)
                image.gameObject.SetActive(false);
        }
        nextButton.playOnAwake = false;
        nextButton.Stop();
        foreach (var audio in audioSources)
        {
            if (audio != null)
            {
                audio.playOnAwake = false;
                audio.Stop(); // Ensure that the audio is stopped
            }
        }

        // Initially disable all captions
        foreach (var subtitle in subtitles)
        {
            if (subtitle != null)
                subtitle.gameObject.SetActive(false);
        }
        nextSceneButton.gameObject.SetActive(false); // Make sure the button is also hidden

        // Start the sequence
        StartCoroutine(ShowImagesInSequence());


    }

    IEnumerator ShowImagesInSequence()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null && subtitles[i] != null)
            {
                // Enable the current image and subtitle
                images[i].gameObject.SetActive(true);
                subtitles[i].gameObject.SetActive(true);
                audioSources[i].Play();

                // Wait for the specified delay
                yield return new WaitForSeconds(delayBetweenImages);
                audioSources[i].Stop();
                // After the delay, disable them before the next iteration
                //images[i].gameObject.SetActive(false);
                subtitles[i].gameObject.SetActive(false);
            }
        }

        nextSceneButton.gameObject.SetActive(true); 

        nextSceneButton.onClick.AddListener(GoToNextScene); // Add the listener for button click


        // Optionally, do something after all images have been shown
        // e.g., load a new scene
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(2);
        nextButton.Play();
    }
}