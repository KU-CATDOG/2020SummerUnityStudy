using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float speed = 2f;

    public Transform bulletOut;
    public GameObject bulletPrefab;

    private int score = 0;
    public int hp = 5;

    void Update()
    {
        Move();
        Rotate();
        Fire();
    }

    void Move()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        transform.position += new Vector3(dx * speed * Time.deltaTime, 0, dz * speed * Time.deltaTime);

    }

    void Rotate()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Floor")))
        {
            Vector3 lookPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPoint, Vector3.up);
        }
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, bulletOut.position, bulletOut.rotation);
        }
    }

    public void GainScore(int s)
    {
        score += s;
    }
}
