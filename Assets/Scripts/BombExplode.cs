using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public float timeBeforeExploding;
    public float range;
    public string enemyTag = "Enemy";
    public float damage;
    public GameObject explodeEffect;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("explode");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(timeBeforeExploding);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemy != null && distanceToEnemy <= range)
            {
                Damage(enemy);
                createEffect();
                audioSource.Play();
            }
        }
        transform.position = transform.position + new Vector3(0f, -10f, 0f);
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
