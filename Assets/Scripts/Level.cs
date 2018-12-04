﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] List<Wave> allWaves;
    [SerializeField] int maxNumWaves;
    EnemySpawner2 enemySpawner;

    private void Start() {

        enemySpawner = FindObjectOfType<EnemySpawner2>();
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves() {

        for (int currentWaveNum = 0; currentWaveNum < maxNumWaves; currentWaveNum++) {

            Wave currentWave = allWaves[currentWaveNum];
            yield return StartCoroutine(enemySpawner.SpawnWave(currentWave.GetFormations()));
        }
    }
}