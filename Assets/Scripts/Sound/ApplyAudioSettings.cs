using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ApplyAudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    void Start()
    {
        float masterVol = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        float gunshotsVol = PlayerPrefs.GetFloat("GunshotsVolume", 0.75f);

        float masterDB = (masterVol <= 0.01f) ? -80f : Mathf.Log10(masterVol) * 20;
        float gunshotsDB = (gunshotsVol <= 0.01f) ? -80f : Mathf.Log10(gunshotsVol) * 20;

        audioMixer.SetFloat("MasterVolume", masterDB);
        audioMixer.SetFloat("GunshotsVolume", gunshotsDB);
    }
}
