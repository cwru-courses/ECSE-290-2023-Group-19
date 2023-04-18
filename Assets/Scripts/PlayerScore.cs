using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static int EnemyReachedDesti;
    public TextMeshProUGUI enemyCountText;
    public int gameDuration;

    // Start is called before the first frame update
    void Start()
    {
        EnemyReachedDesti = 0;
        InvokeRepeating("detectTurret", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCountText.text = "Enemy got in territory: " + EnemyReachedDesti;
        Debug.Log(gameDuration);
    }

    void updateDuration()
    {
        gameDuration++;
    }
}
