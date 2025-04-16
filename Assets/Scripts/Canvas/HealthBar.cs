using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetSlider(float amount)
    {
        slider.value = amount;
    }
    public void SetSliderMax(float amount)
    {
        slider.maxValue = amount;
        SetSlider(amount);
    }
}
