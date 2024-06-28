using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    [System.Serializable]
    public abstract class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite sprite;
        public GameObject prefab;
    }

    [System.Serializable]
    public abstract class WeaponData : ItemData
    {
        public int damage;
        public int energy;
        public float coolDown;
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "Gun Data", menuName = "Scriptable Objects/Gun")]
    public class GunData : WeaponData
    {
        public float bulletSpeed;
        public GameObject bulletPrefab;
    }