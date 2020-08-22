using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public PlayerController player;

    public float initDelay = 10f;
    public float delayDec = 0.2f;
    public float minDelay = 1f;

    private float delay;

    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        delay = initDelay;

        while (!player.gameOver)
        {
            Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

            yield return new WaitForSeconds(delay);

            delay = Mathf.Max(minDelay, delay - delayDec);
        }
    }
}
