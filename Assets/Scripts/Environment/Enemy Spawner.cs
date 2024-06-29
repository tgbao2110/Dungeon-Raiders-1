using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> enemiesPrefabs;

    public int GetLength()
    {
        return spawnPoints.Count;
    }

    public void Spawn()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            int randomPrefabIndex = Random.Range(0, enemiesPrefabs.Count);
            GameObject instantiatedEnemy = Instantiate(enemiesPrefabs[randomPrefabIndex], spawnPoint.position, Quaternion.identity);
            instantiatedEnemy.transform.SetParent(this.transform);
        }
    }

}
