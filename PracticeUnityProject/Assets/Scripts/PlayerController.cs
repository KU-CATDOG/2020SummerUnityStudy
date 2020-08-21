using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float speed = 2f;
    public float immuneTime = 2f;

    public Transform bulletOut;
    public GameObject bulletPrefab;

    public int score = 0;
    public int hp = 5;

    public float currImmuneTime = 0;

    public bool gameOver = false;

    public Text hpText;
    public Text scoreText;


    private void Start()
    {
        hpText.text = "HP : " + hp;
        scoreText.text = "Score : " + score;

    }
    void Update()
    {
        if (!gameOver)
        {
            Move();
            Rotate();
            Fire();
        }
        currImmuneTime = Mathf.Max(0f, currImmuneTime - Time.deltaTime);
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

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Raycast")))
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
        scoreText.text = "Score : " + score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && currImmuneTime <= 0 && !collision.gameObject.GetComponent<EnemyController>().isDead)
        {
            hp--;
            currImmuneTime = immuneTime;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fall"))
        {
            hp = 0;
        }
        if (hp <= 0)
        {
            PlayerDeath();
        }
        hpText.text = "HP : " + hp;
    }

    void PlayerDeath()
    {
        gameOver = true;
        Debug.Log("you died");
    }
}
