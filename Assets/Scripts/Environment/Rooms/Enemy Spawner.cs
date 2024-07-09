using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int currentLevel;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> enemiesPrefabs;
    int index = 1;

    public int GetLength()
    {
        return spawnPoints.Count;
    }

    public void Spawn(int numberOfEnemies)
    {
        currentLevel = GameManager.Instance.GetLevel();
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (availableSpawnPoints.Count == 0)
                return; // No more spawn points available

            int randomSpawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomSpawnPointIndex];
            GameObject enemyPrefab;

            switch (currentLevel)
            {
                case 1:
                    enemyPrefab = enemiesPrefabs[0];
                    break;
                case 2:
                    if (i < numberOfEnemies - 1)
                    {
                        enemyPrefab = enemiesPrefabs[0];
                    }
                    else
                    {
                        enemyPrefab = enemiesPrefabs[1];
                    }
                    break;
                default:
                    int randomPrefabIndex = Random.Range(0, enemiesPrefabs.Count);
                    enemyPrefab = enemiesPrefabs[randomPrefabIndex];
                    break;
            }

            GameObject instantiatedEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            instantiatedEnemy.transform.SetParent(this.transform);
            instantiatedEnemy.name = "Enemy " + (i + 1);

            availableSpawnPoints.RemoveAt(randomSpawnPointIndex);
        }
    }
}
