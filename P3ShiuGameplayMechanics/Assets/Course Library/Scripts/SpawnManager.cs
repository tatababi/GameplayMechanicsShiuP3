using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;
    public List<WeightedEnemy> weightedEnemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created]
    [System.Serializable]
    public class WeightedEnemy
    {
        public GameObject  enemyPrefab;
        public float weight;
    }

    void Start()
    {

        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(),
            powerupPrefab.transform.rotation);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)

        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);

            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }

    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            {
               
                GameObject enemyToSpawn = GetRandomWeightedEnemy();
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            }
        }
        GameObject GetRandomWeightedEnemy()
        {
            float totalWeight = 0.0f;

            foreach (var weightedEnemy in weightedEnemies)
            {
                totalWeight += weightedEnemy.weight;
            }

            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0.0f;
            foreach (var weightedEnemy in weightedEnemies)
            {
                cumulativeWeight += weightedEnemy.weight;
                if (randomValue < cumulativeWeight)
                {
                    return weightedEnemy.enemyPrefab;
                }
            }
            return null;
        }
    



            // Update is called once per frame


        }    private Vector3 GenerateSpawnPosition()
    {

        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }


    


}


