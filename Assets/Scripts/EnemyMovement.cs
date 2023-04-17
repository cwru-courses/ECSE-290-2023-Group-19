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
    private Transform target;
    private int wavepointIndex = 0;
    public GameObject dieEffect;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
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

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(dieEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(this.gameObject);
    }
}
