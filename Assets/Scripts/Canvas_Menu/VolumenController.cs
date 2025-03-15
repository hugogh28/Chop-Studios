using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;  // Referencia al Slider de volumen
    private AudioSource musicSource;  // Referencia al AudioSource

    void Start()
    {
        // Encontramos el AudioSource que reproduce la música (asegúrate de asignarlo en la escena)
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();

        // Comprobamos si tenemos el slider y asignamos el valor actual de volumen
        if (volumeSlider != null)
        {
            volumeSlider.value = musicSource.volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    // Método que se llama cuando el volumen cambia
    void OnVolumeChanged(float value)
    {
        musicSource.volume = value;
    }
}
