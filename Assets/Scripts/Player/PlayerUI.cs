using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        coinsText.text = "Coins: " + playerStats.coins.ToString();
        scoreText.text = "Score: " + playerStats.score.ToString();
    }
}
