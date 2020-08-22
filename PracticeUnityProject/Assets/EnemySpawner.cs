using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public PlayerController player;

    public float delay = 10f;
    public float delayDec = 0.2f;
    public float minDelay = 1f;

    private float curDelay;

    public Transform[] spawnPoints;
    public GameObject enemy;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(!player.gameOver)
        {
            if (curDelay <= 0)
            {
                Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                curDelay = delay;
                delay = Mathf.Max(minDelay, delay - delayDec);
            }
            curDelay -= Time.deltaTime;
        }
    }
}
