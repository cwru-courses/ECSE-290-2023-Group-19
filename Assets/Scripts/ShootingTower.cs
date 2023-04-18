using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingTower : MonoBehaviour
{
    private Transform target;
    public float range = 5f;

    public string enemyTag = "Enemy";
    public Transform rotater;
    public float turnSpeed = 10f;

    public float fireRate = 1f;
    private float fireCountdown = 0.4f;

    public float health = 100;
    private float initialHealth;
    public Image healthBar;

    public GameObject ammunitionPrefab;
    public Transform firePoint;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("Decay", 0f, 1f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Debug.Log(enemies.Length);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
//            Debug.Log(enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                //Debug.Log(nearestEnemy.name);
            }
        }
        //Debug.Log(shortestDistance);
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            //Debug.Log("change target");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update");
        if (target == null)
        {
            return;
        }
        //Debug.Log("not null");
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotationAngle = Quaternion.Lerp(rotater.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //Debug.Log("rotate");
        rotater.rotation = Quaternion.Euler(0f, rotationAngle.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject newAmmunition = (GameObject)Instantiate(ammunitionPrefab, firePoint.position, firePoint.rotation);
        //newAmmunition.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        Ammunition a = newAmmunition.GetComponent<Ammunition>();
        audioSource.Play();
        
        if (a != null)
        {
            a.Seek(target);
        }
    }

    public void Decay()
    {
        TakeDamage(1.0f);
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

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
