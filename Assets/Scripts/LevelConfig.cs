using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Config")]
public class LevelConfig : ScriptableObject {
    
    [SerializeField] List<Wave> waves;
    [SerializeField] int currentWave = 0;
    [SerializeField] int totalNumWaves = 5;
    bool wavesCompleted = false;

    public Wave GetWave() {

        currentWave++;

        int waveIndex = Random.Range(0, waves.Count - 1);

        return waves[waveIndex];
    }

    public bool areWavesCompleted() {

        return wavesCompleted;
    }
}
