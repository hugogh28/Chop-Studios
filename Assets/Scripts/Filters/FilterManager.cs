using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.SceneManagement;

public class FilterManager : MonoBehaviour
{
    public static FilterManager instance;

    public Material noFilter;
    public Material protanopia;
    public Material deuteranopia;
    public Material tritanopia;

    private Material activeFilter;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetFilter(string filter)
    {
        switch(filter)
        {
            case "Protanopia":
                activeFilter = protanopia;
                break;
            case "Deuteranopia":
                activeFilter = deuteranopia;
                break;
            case "Tritanopia":
                activeFilter = tritanopia;
                break;
            case "None":
                activeFilter = noFilter; 
                break;
            default:
                break;
        }
        ApplyFilter();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyFilter();
    }

    void ApplyFilter()
    {
        Camera cam = Camera.main;
        if (cam==null)
        {
            return;
        }
        var effect = cam.GetComponent<Colorblind>();
        if (effect == null)
        {
            effect = cam.gameObject.AddComponent<Colorblind>();
        }

        effect.effectMaterial = activeFilter;
    }
}
