using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        StartCoroutine(LoadHomeSceneAfterDelay(4));
    }

    IEnumerator LoadHomeSceneAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}