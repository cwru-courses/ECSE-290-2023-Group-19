using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5.5f;
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

        waveCountDownText.text = "Next Wave Coming in: " + Mathf.Round(countDown).ToString() + "s";
    }

    IEnumerator SpawnWave ()
    {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y - 0.5f, spawnPoint.position.z), spawnPoint.rotation);
    }
}
