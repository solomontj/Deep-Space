using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WiringGameController : MonoBehaviour
{
    public GameObject wireGame;
    public GameObject playerlight;
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public float brightIntensity = 1f;

    public void CloseGame()
{
    StartCoroutine(CloseGameAfterDelay(2)); // Start the coroutine with a 3-second delay

IEnumerator CloseGameAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay); // Wait for the specified delay
    wireGame.SetActive(false); // Then deactivate the wireGame GameObject
    playerlight.SetActive(false);
    globalLight.intensity = brightIntensity;
}
}

}
