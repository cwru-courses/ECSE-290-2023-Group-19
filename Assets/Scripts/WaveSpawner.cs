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

    private int waveNumber = 0;

    private void Start()
    {
    }

    void Update ()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
        waveCountDownText.text = "Next Wave In: " + Mathf.Round(countDown).ToString() + "s";
    }

    void spawnNormalEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }

    void spawnDragon()
    {
        Instantiate(dragonPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }

    void spawnBombEnemy()
    {
        Instantiate(bombEnemyPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        if (waveNumber < 3)
        {
            for (int i = 0; i < waveNumber + 1; i++)
            {
                spawnNormalEnemy();
                yield return new WaitForSeconds(0.3f);
            }
        }
        else if (waveNumber % 5 == 0)
        {
            for (int i = 0; i < Random.Range(waveNumber/5 , waveNumber/5 + 2); i++)
            {
                spawnDragon();
                yield return new WaitForSeconds(0.5f);
            }
        }
        else if (waveNumber % 3 == 0)
        {
            for (int i = 0; i < Random.Range(waveNumber / 2, waveNumber / 2 + 3); i++)
            {
                spawnBombEnemy();
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            for (int i = 0; i < Random.Range(waveNumber, waveNumber + 5); i++)
            {
                spawnNormalEnemy();
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

}
