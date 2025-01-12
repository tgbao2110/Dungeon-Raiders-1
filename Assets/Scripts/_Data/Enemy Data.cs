using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string discription;
    public GameObject prefab;
    public int maxHealth;
    public int damage;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
}