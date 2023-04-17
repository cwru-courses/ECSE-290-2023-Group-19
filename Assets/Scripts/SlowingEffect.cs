using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingEffect : MonoBehaviour
{
    private Transform target;
    public float range = 5f;

    public string enemyTag = "Enemy";

    public float fireRate = 1f;
    public float cd = 10f;

    public GameObject slowingEffect;
    private bool slowing;
    public float slowDown_speed;
    public Transform firePoint;

    // The color that the grass cube will change to
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (slowing == false)
        {
            slowing = true;
            InvokeRepeating("UpdateTarget", 0f, 0.2f);
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
                SlowDown(enemy);
            }
            else if (enemy != null && distanceToEnemy > range)
            {
                enemy.GetComponent<EnemyMovement>().speed = enemy.GetComponent<EnemyMovement>().initialSpeed;
                enemy.GetComponent<EnemyMovement>().isSlowDown = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SlowDown(GameObject enemy)
    {
        if (!enemy.GetComponent<EnemyMovement>().isSlowDown)
        {
            enemy.GetComponent<EnemyMovement>().speed -= slowDown_speed;
            enemy.GetComponent<EnemyMovement>().isSlowDown = true;
        }
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
            if (enemy != null && distanceToEnemy <= range)
            {
                enemy.GetComponent<EnemyMovement>().speed = enemy.GetComponent<EnemyMovement>().initialSpeed;
                enemy.GetComponent<EnemyMovement>().isSlowDown = false;
            }
        }
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
