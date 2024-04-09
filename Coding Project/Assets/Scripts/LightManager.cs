using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 // This is required for 2D lights.

public class LightManager : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D globalLight; // Assign your global Light 2D component here in the inspector.
    public Button lightToggleButton;

    // Set these to the desired values for "lights off" and "lights on".
    public float darkIntensity = 0.5f;
    public float brightIntensity = 1f;

    void Start()
    {
        //lightToggleButton.onClick.AddListener(ToggleLights);
    }

    // void ToggleLights()
    // {
    //     // Toggle between the two intensity levels.
    //     if (globalLight.intensity == brightIntensity)
    //     {
    //         globalLight.intensity = darkIntensity;
    //     }
    //     else
    //     {
    //         globalLight.intensity = brightIntensity;
    //     }
    // }
}