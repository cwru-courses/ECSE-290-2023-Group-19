using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countDown = 2f;

    private int waveNumber = 1;

    void update ()
    {
        Debug.Log("123");
        if (countDown <= 0f)
        {
            spawnWave();
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
    }

    void spawnWave ()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            spawnEnemy();
        }

        waveNumber++;
    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
