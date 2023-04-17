using UnityEngine;

public class Ammunition : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float damage = 30;
    //public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        var rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

        if (direction.magnitude <= distanceThisFrame)
        {
            //Debug.Log("hit");
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effect, 2f);
        //Destroy(target.gameObject);
        Damage(target);
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        enemy.gameObject.GetComponent<EnemyMovement>().TakeDamage(damage);
    }
}
