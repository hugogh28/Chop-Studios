using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFilter : MonoBehaviour
{
    public Toggle toggleNoFilter;
    public Toggle toggleProtanopia;
    public Toggle toggleDeuteranopia;
    public Toggle toggleTritanopia;

    void Start()
    {
        toggleNoFilter.onValueChanged.AddListener((isOn) => { if (isOn) FilterManager.instance.SetFilter("None"); });
        toggleProtanopia.onValueChanged.AddListener((isOn) => { if (isOn) FilterManager.instance.SetFilter("Protanopia"); });
        toggleDeuteranopia.onValueChanged.AddListener((isOn) => { if (isOn) FilterManager.instance.SetFilter("Deuteranopia"); });
        toggleTritanopia.onValueChanged.AddListener((isOn) => { if (isOn) FilterManager.instance.SetFilter("Tritanopia"); });
    }
 }

