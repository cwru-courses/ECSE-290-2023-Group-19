using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Transform target;
    public float range = 5f;

    public string enemyTag = "Enemy";
    public Transform rotater;
    public float turnSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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
            Debug.Log(enemy.transform.position);
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
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
