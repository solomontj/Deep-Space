using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for IEnumerator

public class OutroSceneLoader : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Settings()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        StartCoroutine(LoadHomeSceneAfterDelay(4)); // Call Coroutine to delay scene loading
    }

    IEnumerator LoadHomeSceneAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Wait for 4 seconds in real-time
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
