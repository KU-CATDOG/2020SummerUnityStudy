using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text hpText;
    public Text scoreText;
    public Text gameOverText;

    public float speed = 10f;
    public Transform firePos;
    public GameObject bullet;

    public float hp = 5;

    public float immuneTime = 2f;
    public float currImmuneTime = 0;

    public bool gameOver = false;

    public float score = 0;


    void Move()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        transform.position += new Vector3(dx, 0, dz) * speed * Time.deltaTime;
    }

    void Rotate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Raycast")))
        {
            Vector3 lookPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPoint, Vector3.up);
        }
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firePos.position, firePos.rotation);
        }
    }

    public void GetDamage(float damage)
    {
        if (currImmuneTime <= 0)
        {
            hp -= damage;
            hpText.text = "HP : " + hp;
            if (hp <= 0)
            {
                PlayerDeath();
            }
            currImmuneTime = immuneTime;
        }
    }

    public void GetScore(float _score)
    {
        score += _score;
        scoreText.text = "Score : " + score;
    }

    private void PlayerDeath()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
        Debug.Log("you died");
    }

    // Start is called before the first frame update
    void Start()
    {
        hpText.text = "HP : " + hp;
        scoreText.text = "Score : " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            Move();
            Rotate();
            Fire();
            currImmuneTime = Mathf.Max(0f, currImmuneTime - Time.deltaTime);

            if(transform.position.y < GameObject.Find("Floor").transform.position.y)
            {
                PlayerDeath();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
