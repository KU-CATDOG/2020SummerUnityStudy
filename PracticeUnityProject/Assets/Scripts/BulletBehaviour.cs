using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
