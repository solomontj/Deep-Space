using UnityEngine;
using System.Collections;

public class FlashingLight2D : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D lightToFlash;
    public AudioSource audioSource;

    private void Start()
    {
        lightToFlash = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        if (lightToFlash == null)
        {
            Debug.LogError("FlashingLight2D script is attached to a GameObject without a Light2D component.", this);
        }
        else
        {
            audioSource.spatialBlend = 1.0f;
            audioSource.minDistance = 1.0f;
            audioSource.maxDistance = 3.5f;
            StartCoroutine(FlashLight());
        }
    }

    private IEnumerator FlashLight()
    {
        while (true)
        {
            lightToFlash.enabled = !lightToFlash.enabled;

            if (lightToFlash.enabled)
            {
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
}