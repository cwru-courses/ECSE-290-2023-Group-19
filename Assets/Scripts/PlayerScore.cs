using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static int EnemyReachedDesti;
    public TextMeshProUGUI gameDurationText;
    public TextMeshProUGUI PlayerHealthText;
    public int gameDuration;
    public static int playerHealth = 10;

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI woodText;

    // Start is called before the first frame update
    void Start()
    {
        EnemyReachedDesti = 0;
        InvokeRepeating("updateDuration", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        gameDurationText.text = "Game Duration: " + gameDuration.ToString() + "s";
        PlayerHealthText.text = "Your Health: " + playerHealth + "hp";

        coinsText.text = "Coins: " + PlayerStats.totalMoney;
        woodText.text = "Wood: " + PlayerStats.totalWood;
    }

    void updateDuration()
    {
        gameDuration++;
    }

    public static void takeDamage(int damage)
    {
        playerHealth -= damage;
    }
}
