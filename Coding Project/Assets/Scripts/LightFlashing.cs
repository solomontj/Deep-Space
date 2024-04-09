using UnityEngine;
using System.Collections;
 // Required for Light2D

public class FlashingLight2D : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D lightToFlash;
    public AudioSource audioSource;

    private void Start()
    {
        lightToFlash = GetComponent<UnityEngine.Rendering.Universal.Light2D>(); // Get the Light2D component on the same GameObject
        if (lightToFlash == null)
        {
            Debug.LogError("FlashingLight2D script is attached to a GameObject without a Light2D component.", this);
        }
        else
        {
            audioSource.spatialBlend = 1.0f; // 1.0 means fully 3D
            audioSource.minDistance = 1.0f; // The distance within which the volume is at the loudest
            audioSource.maxDistance = 3.5f;
            StartCoroutine(FlashLight());
        }
    }

    private IEnumerator FlashLight()
    {
        while (true) // Creates an infinite loop
        {
            lightToFlash.enabled = !lightToFlash.enabled; // Toggle the light on and off

            if (lightToFlash.enabled)
            {
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play(); // Play the sound only when the light turns on
                }
            }
            else
            {
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop(); // Stop the sound when the light turns off
                }
            }

            yield return new WaitForSeconds(1); // Wait for 1 second
        }
    }
}
