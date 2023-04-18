using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowingEffect : MonoBehaviour
{
    private Transform target;
    public float range = 5f;

    public string enemyTag = "Enemy";

    public float fireRate = 1f;
    public float cd = 10f;

    public GameObject slowingEffect;
    private bool slowing;
    public float slowDown_percent;
    public Transform firePoint;

    public float health = 100;
    private float initialHealth;
    public Image healthBar;

    // The color that the turret will change to
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        InvokeRepeating("Decay", 0f, 1f);
    }

    private void OnMouseDown()
    {
        if (slowing == false)
        {
            slowing = true;
            InvokeRepeating("UpdateTarget", 0f, 0.05f);
            createEffect();
            StartCoroutine("startDetection");
        }
    }

    IEnumerator startDetection()
    {
        rend.material.color = startColor;
        audioSource.Play();
        yield return new WaitForSeconds(4.0f);
        CancelInvoke("UpdateTarget");
        endEffect();
        yield return new WaitForSeconds(cd);
        slowing = false;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemy != null && distanceToEnemy <= range)
            {
                enemy.GetComponent<EnemyMovement>().isSlowDown = true;    // mark them as being slowed down
                SlowDown(enemy);
            }
            else if (enemy != null && distanceToEnemy > range && distanceToEnemy < (range + 0.2f))
            {
                enemy.GetComponent<EnemyMovement>().isSlowDown = false;
                enemy.GetComponent<EnemyMovement>().stopSlowDown(slowDown_percent);
            }
        }
    }

    void SlowDown(GameObject enemy)
    {
        enemy.GetComponent<EnemyMovement>().slowDown(slowDown_percent);
    }

    void createEffect()
    {
        GameObject slowEffect = (GameObject)Instantiate(slowingEffect, firePoint.position, firePoint.rotation);
        Destroy(slowEffect, 4f);
    }

    void endEffect()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemy != null && distanceToEnemy <= (range + 0.2f))
            {
                enemy.GetComponent<EnemyMovement>().isSlowDown = false;
                enemy.GetComponent<EnemyMovement>().stopSlowDown(slowDown_percent);
            }
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

    void OnMouseEnter()
    {
        if (slowing == false)
        {
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}