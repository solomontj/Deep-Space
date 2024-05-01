using System.Collections;
using UnityEngine;

public class WiringGameController : MonoBehaviour
{
    public GameObject wireGame;
    public GameObject playerlight;
    public GameObject lightDialogueObject;
    public CharacterMonologue lightMonologue;
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public AudioSource victorySound;
    public AudioSource buttonSound;
    public float brightIntensity = 1f;

    private void Start()
    {
        CenterGame();
    }

    private void CenterGame()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, mainCamera.nearClipPlane);
        Vector3 worldCenter = mainCamera.ScreenToWorldPoint(screenCenter);
        worldCenter.z = 0;
        wireGame.transform.position = worldCenter;
        buttonSound.Play();
    }

    public void CloseGame()
    {
        if (Wire.connectionsMade >= Wire.totalConnections)
        {
            victorySound.Play();
        }
        lightDialogueObject.SetActive(true);
        lightMonologue.TriggerLightsOnDialogue();
        StartCoroutine(CloseGameAfterDelay(2));
    }

    private IEnumerator CloseGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        wireGame.SetActive(false);
        playerlight.SetActive(false);
        globalLight.intensity = brightIntensity;
    }
}