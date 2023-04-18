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

    public float timeBetweenWaves = 20f;
    private float countDown = 1.5f;

    public TextMeshProUGUI waveCountDownText;

    private int waveNumber = 1;

    private void Start()
    {
        InvokeRepeating("randomSpawnBetweenWaves", 0f, 2f);
    }

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

    void randomSpawnBetweenWaves()
    {
        randomSpawnNormalEnemy();
        randomSpawnDragon();
    }

    IEnumerator SpawnWave ()
    {
        waveNumber++;
        if (waveNumber >= 1)
        {
            for (int i = 0; i < Random.Range(waveNumber, 2 * waveNumber); i++)
            {
                spawnNormalEnemy();
                yield return new WaitForSeconds(0.3f);
            }
        }
        if (waveNumber >= 3) {
            for (int i = 0; i < Random.Range(0.3f * waveNumber, 0.75f * waveNumber); i++)
            {
                spawnDragon();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void randomSpawnNormalEnemy()
    {
        int number = Random.Range(0, 10);
        if (number >= 6)
        {
            spawnNormalEnemy();
        }
    }

    void randomSpawnDragon()
    {
        int number = Random.Range(0, 10);
        if (number >= 8)
        {
            spawnNormalEnemy();
        }
    }

    void spawnNormalEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }

    void spawnDragon()
    {
        Instantiate(dragonPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }
}
