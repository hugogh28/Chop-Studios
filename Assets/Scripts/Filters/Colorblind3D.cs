using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorblind3D : MonoBehaviour
{
    public Renderer renderer;
    public Material protanopiaMaterial;
    public Material deuteranopiaMaterial;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ToggleBool2") == 1)
        {
            renderer.material = protanopiaMaterial;
        }

        if (PlayerPrefs.GetInt("ToggleBool3") == 1)
        {
            renderer.material = deuteranopiaMaterial;
        }
    }
}
