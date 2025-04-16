using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Image overlay;

    private void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        if (overlay != null)
        {
            Color color = overlay.color;
            color.a = 1f - savedBrightness;

            overlay.color = color;
        }
        else
        {
            Debug.LogWarning("No se asignó la imagen de brillo al script BrightnessLoader.");
        }
    }
}
