using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OutroScript : MonoBehaviour
{
    public Image[] images;
    public Image[] subtitles;
    public Button nextSceneButton;
    public AudioSource backgroundMusic;
    public AudioSource nextButton;
    public float delayBetweenImages = 2f;

    void Start()
    {
        backgroundMusic.Play();

        foreach (var image in images)
        {
            if (image != null)
                image.gameObject.SetActive(false);
        }
        nextButton.playOnAwake = false;
        nextButton.Stop();

        foreach (var subtitle in subtitles)
        {
            if (subtitle != null)
                subtitle.gameObject.SetActive(false);
        }
        nextSceneButton.gameObject.SetActive(false);

        StartCoroutine(ShowImagesInSequence());

    }

    IEnumerator ShowImagesInSequence()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null && subtitles[i] != null)
            {

                images[i].gameObject.SetActive(true);
                subtitles[i].gameObject.SetActive(true);

                yield return new WaitForSeconds(delayBetweenImages);

                subtitles[i].gameObject.SetActive(false);
            }
        }

        nextSceneButton.gameObject.SetActive(true);

        nextSceneButton.onClick.AddListener(GoToNextScene);

    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(0);
        nextButton.Play();
    }
}