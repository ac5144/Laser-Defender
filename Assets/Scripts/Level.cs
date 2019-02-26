using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] List<Wave> allWaves;
    [SerializeField] int maxNumWaves;
    EnemySpawner enemySpawner;
    [SerializeField] GameObject bossPrefab;

    private void Start() {

        enemySpawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnAllWaves() {

        for (int currentWaveNum = 0; currentWaveNum < maxNumWaves; currentWaveNum++) {

            Wave currentWave = allWaves[currentWaveNum];
            yield return StartCoroutine(enemySpawner.SpawnWave(currentWave.GetFormations()));
        }
    }

    void SpawnBoss() {

        Vector3 spawnLocation = new Vector3(0, 9, 0);

        Instantiate(bossPrefab, spawnLocation, Quaternion.identity);
    }

    IEnumerator SpawnEnemies() {

        yield return StartCoroutine(SpawnAllWaves());

        SpawnBoss();
    }
}
