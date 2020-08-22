using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackRange = 2;
    public PlayerController player;

    public float hp = 10f;
    public bool isDead = false;

    public void GetDamage(float damage)
    {
        if (!isDead)
        {
            hp -= damage;
            if (hp <= 0)
            {
                GetComponent<Animator>().SetTrigger("Death");
                player.GetScore(1);
                isDead = true;
                Destroy(gameObject, 3);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}
