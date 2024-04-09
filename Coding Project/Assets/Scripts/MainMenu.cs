using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the inspector

    void Start()
    {
        // Disable play on awake to prevent the audio from playing immediately
        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    public void PlayGame()
    {
        // Play the audio clip for starting the game
        audioSource.Play();

        // Load the scene after the audio clip has finished playing
        StartCoroutine(LoadSceneAfterAudio(audioSource.clip.length));
    }

    private IEnumerator LoadSceneAfterAudio(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        // Play the audio clip for quitting the game
        audioSource.Play();

        // Quit the application after the audio clip has finished playing
        StartCoroutine(QuitAfterAudio(audioSource.clip.length));
    }

    private IEnumerator QuitAfterAudio(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Only for use in the editor
#else
        Application.Quit();
#endif
    }
}