using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHP = 10f;
    private float currentHP;
    public bool isDead = false;

    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        currentHP = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            BulletBehaviour bullet = other.GetComponent<BulletBehaviour>();
            if (bullet)
            {
                currentHP -= bullet.bulletDamage;
                Destroy(bullet.gameObject);
            }
            if (currentHP <= 0)
            {
                GetComponent<Animator>().SetTrigger("death");
                isDead = true;
                player.GainScore(1);
                StartCoroutine(BodyLifeOver());
            }
        }
    }

    IEnumerator BodyLifeOver()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
