using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Colorblind : MonoBehaviour
{
    public Material effectMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(effectMaterial != null)
            Graphics.Blit(source, destination, effectMaterial);
        else
            Graphics.Blit(source, destination);
    }
}
