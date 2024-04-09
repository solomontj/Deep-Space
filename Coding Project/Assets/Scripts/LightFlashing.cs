using UnityEngine;
using System.Collections;
 // Required for Light2D

public class FlashingLight2D : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D lightToFlash;

    private void Start()
    {
        lightToFlash = GetComponent<UnityEngine.Rendering.Universal.Light2D>(); // Get the Light2D component on the same GameObject
        if (lightToFlash == null)
        {
            Debug.LogError("FlashingLight2D script is attached to a GameObject without a Light2D component.", this);
        }
        else
        {
            StartCoroutine(FlashLight());
        }
    }

    private IEnumerator FlashLight()
    {
        while (true) // Creates an infinite loop
        {
            lightToFlash.enabled = !lightToFlash.enabled; // Toggle the light on and off
            yield return new WaitForSeconds(1); // Wait for 1 second
        }
    }
}
