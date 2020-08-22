using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public float bulletLifeTime = 5f;
    public float bulletDamage = 3f;

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy)
        {
            enemy.GetDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Destroy(gameObject, bulletLifeTime);
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
