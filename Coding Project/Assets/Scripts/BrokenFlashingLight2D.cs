using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class BrokenFlashingLight2D : MonoBehaviour
{
    private Light2D lightComponent;
    public float minOnTime = 0.05f;
    public float maxOnTime = 0.15f;
    public float minOffTime = 0.1f;
    public float maxOffTime = 0.3f;
    public float extendedPauseTime = .4f;
    private int iterationCounter = 0;

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
            lightComponent.enabled = !lightComponent.enabled;
            iterationCounter++;
            if (iterationCounter % 5 == 0)
            {
                yield return new WaitForSeconds(extendedPauseTime);
            }
            else
            {
                float waitTime = lightComponent.enabled ? 
                                 Random.Range(minOnTime, maxOnTime) : 
                                 Random.Range(minOffTime, maxOffTime);
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
