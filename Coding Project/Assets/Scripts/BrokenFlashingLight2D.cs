using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class BrokenFlashingLight2D : MonoBehaviour
{
    private Light2D lightComponent;
    public float minOnTime = 0.05f; // Minimum time the light stays on
    public float maxOnTime = 0.15f; // Maximum time the light stays on
    public float minOffTime = 0.1f; // Minimum time the light stays off
    public float maxOffTime = 0.3f; // Maximum time the light stays off
    public float extendedPauseTime = .4f; // Time for the extended pause
    private int iterationCounter = 0; // To count the number of iterations

    void Awake()
    {
        lightComponent = GetComponent<Light2D>();
    }

    void Start()
    {
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            // Toggle the light on or off
            lightComponent.enabled = !lightComponent.enabled;

            // Increment the iteration counter
            iterationCounter++;

            // Determine if it's time for an extended pause
            if (iterationCounter % 5 == 0) // After every 2 iterations
            {
                // Apply an extended pause after toggling light
                yield return new WaitForSeconds(extendedPauseTime);
            }
            else // Regular flashing behavior
            {
                // Wait for a random time that the light is on or off
                float waitTime = lightComponent.enabled ? 
                                 Random.Range(minOnTime, maxOnTime) : 
                                 Random.Range(minOffTime, maxOffTime);
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
