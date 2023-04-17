using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public float range = 2.0f;
    public string enemyTag = "Enemy";
    public float damage = 80.0f;
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
        yield return new WaitForSeconds(2.0f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemy != null && distanceToEnemy <= range)
            {
                Damage(enemy);
                Destroy(gameObject);
            }
        }
    }

    void Damage(GameObject enemy)
    {
        enemy.GetComponent<EnemyMovement>().TakeDamage(damage);
    }

}
