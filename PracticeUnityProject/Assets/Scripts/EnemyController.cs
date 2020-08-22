using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHP = 10f;
    private float currentHP;
    public bool isDead = false;
    public float attackRange = 2;
    public PlayerController player;

    public void GetDamage(float damage)
    {
        if(!isDead)
        {
            currentHP -= damage;
            if(currentHP <= 0)
            {
                GetComponent<Animator>().SetTrigger("death");
                isDead = true;
                player.GainScore(1);
                Destroy(gameObject, 3);
            }
        }
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            GetComponent<Animator>().SetTrigger("attack");
        }
    }
}
