using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowingEffect : MonoBehaviour
{
            //Attack range indicator
    public Sprite circleSprite;
    private GameObject attackRangeIndicator;
    private Transform target;
    public float range = 5f;

    public string enemyTag = "Enemy";

    public float fireRate = 1f;
    public float cd = 10f;

    public GameObject slowingEffect;
    public GameObject fixingEffect;
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
    public AudioSource fixingSound;

    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        InvokeRepeating("Decay", 0f, 1f);
    }

    /* deprecated
    private void OnMouseDown()
    {
        if (slowing == false && health > 10)
        {
            slowing = true;
            InvokeRepeating("UpdateTarget", 0f, 0.05f);
            createEffect();
            StartCoroutine("startDetection");
        }
    }
    */

    // to fix
    private void OnMouseOver()
    {
            //when mouse on it, create an indicator
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
                createFixEffect();
                fixingSound.Play();
                Debug.Log("fixed");
            }
            else
            {
                Debug.Log("Not enough wood");
                StartCoroutine(buildManager.ShowAndHideWoodsAlert());
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (slowing == false && health > 10)
            {
                slowing = true;
                InvokeRepeating("UpdateTarget", 0f, 0.05f);
                createEffect();
                StartCoroutine("startDetection");
            }
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

    void createFixEffect()
    {
        GameObject fixEffect = (GameObject)Instantiate(fixingEffect, transform.position + new Vector3(0f, 0.04f, 0f), transform.rotation);
        Destroy(fixEffect, 4f);
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

    void OnMouseEnter()
    {
        if (slowing == false && health >= 10)
        {
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
                if (attackRangeIndicator != null)
        {
            Destroy(attackRangeIndicator);
        }
    }
}
