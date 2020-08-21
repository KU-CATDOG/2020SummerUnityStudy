using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public float bulletLifeTime = 5f;
    public float bulletDamage = 3f;
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void Start()
    {
        StartCoroutine(BulletLifeOver());
    }

    IEnumerator BulletLifeOver()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
