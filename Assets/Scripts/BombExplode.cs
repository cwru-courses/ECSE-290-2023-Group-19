using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public float timeBeforeExploding = 2.0f;
    public float range = 3.0f;
    public string enemyTag = "Enemy";
    public float damage = 100.0f;
    public GameObject explodeEffect;
    public AudioSource audioSource;

    private Renderer rend;
    private Color startColor;
    public Color hoverColor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("explode");
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Collect by mouse click
    private void OnMouseDown()
    {
        PlayerStats.totalBomb++;
        Destroy(gameObject);
    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(timeBeforeExploding);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        createEffect();
        audioSource.Play();
        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (enemy != null && distanceToEnemy <= range)
                {
                    Damage(enemy);
                }
            }
            transform.position = transform.position + new Vector3(0f, -10f, 0f);
        }
    }


    void Damage(GameObject enemy)
    {
        enemy.GetComponent<EnemyMovement>().TakeDamage(damage);
    }


    void createEffect()
    {
        GameObject effect = (GameObject)Instantiate(explodeEffect, transform.position, transform.rotation);
        StartCoroutine(waitToDestroyEffect(effect));
    }


    IEnumerator waitToDestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(effect);
        Destroy(gameObject);
    }

}
