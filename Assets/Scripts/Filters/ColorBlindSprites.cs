using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlindSprites : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite protanopiaSprite;
    public Sprite deuteranopiaSprite;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ToggleBool2") == 1)
        {
            spriteRenderer.sprite = protanopiaSprite;
        }

        if (PlayerPrefs.GetInt("ToggleBool3") == 1)
        {
            spriteRenderer.sprite = deuteranopiaSprite;
        }
    }
}
