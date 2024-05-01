using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightManager : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public Button lightToggleButton;

    public float darkIntensity = 0.5f;
    public float brightIntensity = 1f;

    void Start()
    {

    }

}