using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroScript : MonoBehaviour
{
    public Image[] images;
    public Image[] subtitles;
    public Button nextSceneButton;
    public AudioSource[] audioSources;
    public AudioSource nextButton;
    public float delayBetweenImages = 2f;

    void Start()
    {

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
                audio.Stop();
            }
        }

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
                audioSources[i].Play();

                yield return new WaitForSeconds(delayBetweenImages);
                audioSources[i].Stop();

                subtitles[i].gameObject.SetActive(false);
            }
        }

        nextSceneButton.gameObject.SetActive(true);

        nextSceneButton.onClick.AddListener(GoToNextScene);

    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(2);
        nextButton.Play();
    }
}