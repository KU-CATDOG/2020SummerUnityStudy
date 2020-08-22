using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
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
        SetHP(hp);
        scoreText.text = "Score : " + score;
    }

    private void Update()
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

    public void GetDamage(float damage)
    {
        if(currImmuneTime <= 0)
        {
            SetHP(hp - 1);
            if (hp <= 0)
            {
                PlayerDeath();
            }
            currImmuneTime = immuneTime;
            StartCoroutine(DamagedEffect());
        }
    }

    private IEnumerator DamagedEffect()
    {
        Color originalColor = GetComponent<Renderer>().material.color;

        for(int i = 0; i < 20; i++)
        {
            GetComponent<Renderer>().material.color = new Color(originalColor.r, originalColor.g, originalColor.b, i % 2 == 0 ? 0.5f : 1);
            yield return new WaitForSeconds(immuneTime / 20);
        }

        GetComponent<Renderer>().material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fall"))
        {
            SetHP(0);
            PlayerDeath();
        }
    }

    private void SetHP(int _hp)
    {
        hp = _hp;
        hpText.text = "HP : " + _hp;
    }

    private void PlayerDeath()
    {
        gameOver = true;
        Debug.Log("you died");
    }
}
