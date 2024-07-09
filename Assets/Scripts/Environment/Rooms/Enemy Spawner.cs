using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> enemiesPrefabs;
    int index = 1;

    public int GetLength()
    {
        return spawnPoints.Count;
    }

    public void Spawn(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (spawnPoints.Count == 0) return;

            int randomPrefabIndex = Random.Range(0, enemiesPrefabs.Count);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);

            Transform spawnPoint = spawnPoints[randomSpawnPointIndex];
            GameObject instantiatedEnemy = Instantiate(enemiesPrefabs[randomPrefabIndex], spawnPoint.position, Quaternion.identity);
            instantiatedEnemy.transform.SetParent(this.transform);
            instantiatedEnemy.name = "Enemy " + (i + 1);
        }
    }

}
