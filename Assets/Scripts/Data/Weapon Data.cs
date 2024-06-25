using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    public abstract class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite sprite;
        public GameObject prefab;
    }

    public abstract class WeaponData : ItemData
    {
        public int damage;
        public int energy;
        public float coolDown;
    }


    [CreateAssetMenu(fileName = "Gun Data", menuName = "Scriptable Objects/Gun")]
    public class GunData : WeaponData
    {
        public float bulletSpeed;
        public GameObject bulletPrefab;
    }