﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerOld : MonoBehaviour {

    [SerializeField] List<Formation> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    private IEnumerator Start() {

        do {

            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnEnemies(Formation waveConfig) {

        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++) {

            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(), 
                waveConfig.GetWaypoints()[0].transform.position, 
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());

        }
    }

    private IEnumerator SpawnAllWaves() {

        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) {

            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnEnemies(currentWave));
        }
    }
}
