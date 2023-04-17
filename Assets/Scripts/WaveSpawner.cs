using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform dragonPrefab;
    public Transform bombEnemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 10f;
    private float countDown = 1.5f;

    public TextMeshProUGUI waveCountDownText;

    private int waveNumber = 1;

    void Update ()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
        waveCountDownText.text = "Next Wave Coming: " + Mathf.Round(countDown).ToString() + "s";
    }

    IEnumerator SpawnWave ()
    {
        waveNumber++;
        if (waveNumber != 1 && waveNumber % 5 == 0)
        {
            spawnDragon();
        }
        else
        {
            for (int i = 0; i < waveNumber; i++)
            {
                spawnEnemy();
                yield return new WaitForSeconds(0.3f);
            }
        }

    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }

    void spawnDragon()
    {
        Instantiate(dragonPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }
}
