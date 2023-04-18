using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBombMovement : MonoBehaviour
{
    public float initialSpeed = 6f;
    public float speed;
    public float health = 100;
    private float initialHealth;
    private Transform target;
    private int wavepointIndex = 0;
    public GameObject explodeEffect;
    public Image healthBar;
    public bool isSlowDown = false;
    public string turretTag = "Turret";
    public float detectRange = 1f;
    public float explodeRange = 5f;
    public float damage = 40.0f;
    public GameObject bombCollectable;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        speed = initialSpeed;
        target = Waypoints.points[0];
        InvokeRepeating("detectTurret", 0f, 0.2f);
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

    void detectTurret()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
        foreach (GameObject turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);
            if (turret != null && distanceToTurret <= detectRange)
            {
                explode();
            }
        }
    }

    void explode()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
        foreach (GameObject turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);
            if (turret != null && distanceToTurret <= explodeRange)
            {
                if (turret.GetComponent<ShootingTower>() != null)
                {
                    turret.GetComponent<ShootingTower>().TakeDamage(damage);
                }
                else if (turret.GetComponent<SlowingEffect>() != null)
                {
                    turret.GetComponent<SlowingEffect>().TakeDamage(damage);
                }
            }
        }
        GameObject effect = (GameObject)Instantiate(explodeEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        transform.position = transform.position + new Vector3(0f, -10f, 0f);
        audioSource.Play();
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    void Die()
    {
        GameObject bombToCollect = (GameObject)Instantiate(bombCollectable, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
