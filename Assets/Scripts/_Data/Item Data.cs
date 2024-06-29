using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    [System.Serializable]

    [CreateAssetMenu(fileName = "Revolver", menuName = "Scriptable Objects/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public string itemName;
        public Sprite sprite;
        public GameObject prefab;
        public int damage;
        public int energy;
        public float coolDown;
        public float bulletSpeed;
        public GameObject bulletPrefab;
    }