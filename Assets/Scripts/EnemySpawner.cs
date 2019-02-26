using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    int numWavesBeingSpawned = 0;

    public IEnumerator SpawnWave(List<Formation> allFormations) {

        foreach(var formation in allFormations) {

            StartCoroutine(SpawnEnemies(formation));
        }

        while (numWavesBeingSpawned > 0) {

            yield return null;
        }
    }

    public IEnumerator SpawnEnemies(Formation formation) {
        
        numWavesBeingSpawned++;

        for (int enemyCount = 0; enemyCount < formation.GetNumberOfEnemies(); enemyCount++) {

            var newEnemy = Instantiate(
                formation.GetEnemyPrefab(),
                formation.GetWaypoints()[0].transform.position,
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(formation);

            yield return new WaitForSeconds(formation.GetTimeBetweenSpawn());
        }

        numWavesBeingSpawned--;
    }
}
