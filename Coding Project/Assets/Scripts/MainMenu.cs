using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource; 

    void Start()
    {

        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    public void PlayGame()
    {

        audioSource.Play();

        StartCoroutine(LoadSceneAfterAudio(audioSource.clip.length));
    }

    private IEnumerator LoadSceneAfterAudio(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {

        audioSource.Play();

        StartCoroutine(QuitAfterAudio(audioSource.clip.length));
    }

    private IEnumerator QuitAfterAudio(float delay)
    {
        yield return new WaitForSeconds(delay);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit();
#endif
    }
}