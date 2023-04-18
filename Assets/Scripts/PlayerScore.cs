using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public static int EnemyReachedDesti;
    public TextMeshProUGUI gameDurationText;
    public TextMeshProUGUI PlayerHealthText;
    public int gameDuration;
    public static int playerHealth = 10;

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
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
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
