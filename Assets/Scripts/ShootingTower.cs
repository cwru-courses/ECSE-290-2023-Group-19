using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootingTower : MonoBehaviour
{
        //Attack range indicator
    public Sprite circleSprite;
    private GameObject attackRangeIndicator;

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
    public GameObject fixingEffect;
    public AudioSource fixingSound;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
        InvokeRepeating("Decay", 0f, 1f);
        buildManager = BuildManager.FindObjectOfType<BuildManager>();
    }


    private void OnMouseOver()
    {
         if (attackRangeIndicator == null)
        {
            attackRangeIndicator = CreateAttackRangeIndicator();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (PlayerStats.totalWood >= 1)
            {
                if (health <= 50)
                {
                    health += 50;
                }
                else
                {
                    health = 100;
                }
                PlayerStats.totalWood -= 1;
                createEffect();
                fixingSound.Play();
                Debug.Log("fixed");
            }
            else
            {
                Debug.Log("Not enough wood");
                StartCoroutine(buildManager.ShowAndHideWoodsAlert());
            }
        }
    }


    private void OnMouseExit()
    {
        if (attackRangeIndicator != null)
        {
            Destroy(attackRangeIndicator);
        }
    }


    private GameObject CreateAttackRangeIndicator()
    {
        GameObject indicator = new GameObject("AttackRangeIndicator");
        indicator.transform.SetParent(transform);
        indicator.transform.position = transform.position;
        indicator.transform.rotation = Quaternion.Euler(90, 0, 0);
        indicator.transform.position += Vector3.up * 0.1f;
        float indicatorScale = range * 2;
        indicator.transform.localScale = new Vector3(indicatorScale, indicatorScale, 1);

        SpriteRenderer spriteRenderer = indicator.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = circleSprite;
        spriteRenderer.color = Color.red;
        spriteRenderer.sortingOrder = -1; // Set the sorting order to make sure it's rendered below the tower

        return indicator;
    }

    void createEffect()
    {
        GameObject fixEffect = (GameObject)Instantiate(fixingEffect, transform.position + new Vector3(0f, 0.04f, 0f), transform.rotation);
        Destroy(fixEffect, 4f);
    }

    void UpdateTarget()
    {
        if (health > 10)
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
            else
            {
                target = null;
            }
        }
        else
        {
            target = null;
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
        if (health > 10)
        {
            TakeDamage(1.0f);
        }
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
