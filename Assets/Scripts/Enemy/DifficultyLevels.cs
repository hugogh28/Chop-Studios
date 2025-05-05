using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevels : MonoBehaviour
{
    public void Easy()
    {
        DifficultySettings.difficulty = 1f;
    }

    public void Normal()
    {
        DifficultySettings.difficulty = 1.5f;
    }

    public void Hard()
    {
        DifficultySettings.difficulty = 2f;
    }
}

public static class DifficultySettings
{
    public static float difficulty = 1f;
}
