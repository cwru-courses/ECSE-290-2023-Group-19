using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static int EnemyReachedDesti;
    public TextMeshProUGUI gameDurationText;
    public Image healthBar;
    public int gameDuration;
    public static float playerHealth = 10;

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI bombText;

    public TextMeshProUGUI endTimeText;
    public Image gameOver;
    private bool gameIsOver;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 10;
        gameIsOver = false;
        EnemyReachedDesti = 0;
        InvokeRepeating("updateDuration", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerHealth / 10;
        gameDurationText.text = "Game Duration: " + gameDuration.ToString() + "s";
        if (playerHealth <= 0)
        {
            //SceneManager.LoadScene("GameOver");
            GameOver();
            gameIsOver = true;
        }

        coinsText.text = "Coins: " + PlayerStats.totalMoney;
        woodText.text = "Wood: " + PlayerStats.totalWood;
        bombText.text = "Bombs: " + PlayerStats.totalBomb;
    }

    void updateDuration()
    {
        gameDuration++;
    }

    public static void takeDamage(int damage)
    {
        playerHealth -= damage;
    }

    void GameOver()
    {
        if (!gameIsOver)
            endTimeText.text = "Game Duration: " + gameDuration.ToString() + "s";
        gameOver.gameObject.SetActive(true);
    }
}
