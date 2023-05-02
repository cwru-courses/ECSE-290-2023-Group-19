using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float initialSpeed = 6f;
    public float speed;
    public float health = 100;
    private float initialHealth;
    public int killMoney = 50;
    private Transform target;
    private int wavepointIndex = 0;
    public GameObject dieEffect;
    public Image healthBar;
    public bool isSlowDown = false;
    public int damageToPlayerHealth = 1;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health + health * 0.05f * (WaveSpawner.waveNumber - 1f);
        health = initialHealth;
        speed = initialSpeed;
        target = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        var rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 20);
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            // update player score
            PlayerScore.EnemyReachedDesti++;
            PlayerScore.takeDamage(damageToPlayerHealth);
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / initialHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    public void slowDown(float percent)
    {
        speed = initialSpeed * percent;
    }

    public void stopSlowDown(float percent)
    {
        if (isSlowDown == false)
        {
            speed = initialSpeed;
        }
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(dieEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(this.gameObject);
        PlayerStats.totalMoney += killMoney;

        Debug.Log("Total money " + PlayerStats.totalMoney);
    }
}
