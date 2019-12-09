using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public bool canSpawn = true;

    GameObject[] spawnPositions;
    public GameObject enemyPrefab;
    GameObject[] enemiesInScene;
    public float spawnTimer = 2.0f;
    public int maxNumberOfEnemies = 4;
    int randomNumber;

    void Update()
    {
        spawnPositions = GameObject.FindGameObjectsWithTag("Spawner");
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

        if(canSpawn && enemiesInScene.Length < maxNumberOfEnemies)
        {
            canSpawn = false;
            randomNumber = Random.Range(0, spawnPositions.Length);
            Instantiate(enemyPrefab, spawnPositions[randomNumber].transform.position, spawnPositions[randomNumber].transform.rotation);
            Invoke("SpawnReset", spawnTimer);
        }
    }

    void SpawnReset()
    {
        canSpawn = true;
    }
}
