using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 6f;
    private float countDown = 1.5f;

    public TextMeshProUGUI waveCountDownText;

    private int waveNumber = 0;

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
        for (int i = 0; i < waveNumber; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }

    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position + new Vector3(0.0f, -0.5f, 0.0f), spawnPoint.rotation);
    }
}
