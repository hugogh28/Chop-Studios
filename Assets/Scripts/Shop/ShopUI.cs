using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI coinsText;

    public int healthPrice = 10;
    public int armorPrice = 15;
    public int healthAmount = 30;
    public int armorAmount = 20;

    private void Update()
    {
        coinsText.text = "Coins: " + playerStats.coins.ToString();
    }
    public void BuyHealth()
    {
        if(playerStats.SpendCoins(healthPrice) && playerStats.currentHealth < 100)
            playerStats.AddHealth(healthAmount);
    }

    public void BuyArmor()
    {
        if (playerStats.SpendCoins(armorPrice) && playerStats.currentArmor < 50)
            playerStats.AddArmor(armorAmount);
    }
}
