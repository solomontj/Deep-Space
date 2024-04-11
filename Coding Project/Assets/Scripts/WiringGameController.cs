using System.Collections;
using UnityEngine;

public class WiringGameController : MonoBehaviour
{
    public GameObject wireGame;
    public GameObject playerlight;
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public AudioSource victorySound;  // AudioSource for the victory sound
    public AudioSource buttonSound;
    public float brightIntensity = 1f;

    private void Start()
    {
        CenterGame();
    }

    // This method centers the wireGame on the screen
    private void CenterGame()
    {
        Camera mainCamera = Camera.main;  // Get the main camera
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, mainCamera.nearClipPlane);
        Vector3 worldCenter = mainCamera.ScreenToWorldPoint(screenCenter);
        worldCenter.z = 0;  // Ensure the game object is placed at z=0 (adjust this depending on your game's camera setup)
        wireGame.transform.position = worldCenter;
        buttonSound.Play();
    }

    public void CloseGame()
    {
        if (Wire.connectionsMade >= Wire.totalConnections)
        {
            victorySound.Play();  // Play the victory sound when the game is won
        }
        StartCoroutine(CloseGameAfterDelay(2)); // Start the coroutine with a 2-second delay
    }

    private IEnumerator CloseGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        wireGame.SetActive(false); // Then deactivate the wireGame GameObject
        playerlight.SetActive(false);
        globalLight.intensity = brightIntensity;
    }
}
