using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManagement : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider gunshotsSlider;

    // Start is called before the first frame update
    void Start()
    {
        float savedMasterVol = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        float savedGunshotsVol = PlayerPrefs.GetFloat("GunshotsVolume", 0.75f);

        masterSlider.value = savedMasterVol;
        gunshotsSlider.value = savedGunshotsVol;

        SetMasterVolume(savedMasterVol);
        SetGunshotsVolume(savedGunshotsVol);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        gunshotsSlider.onValueChanged.AddListener(SetGunshotsVolume);
    }

    public void SetMasterVolume(float volume)
    {
        float newVolume = (volume <= 0.01f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("MasterVolume", newVolume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetGunshotsVolume(float volume)
    {
        float newVolume = (volume <= 0.01f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("GunshotsVolume", newVolume);
        PlayerPrefs.SetFloat("GunshotsVolume", volume);
    }
}
