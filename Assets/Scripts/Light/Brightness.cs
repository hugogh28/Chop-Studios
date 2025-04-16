using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    public Image overlay;
    public Slider brightSlider;

    private void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);

        brightSlider.minValue = 0.1f;
        brightSlider.maxValue = 1f;

        brightSlider.value = savedBrightness;
        Bright(savedBrightness);

        brightSlider.onValueChanged.AddListener(Bright);
    }

    void Bright(float value)
    {
        Color color = overlay.color;
        color.a = 1f - value;

        overlay.color = color;
        PlayerPrefs.SetFloat("Brightness", value);
    }
}
