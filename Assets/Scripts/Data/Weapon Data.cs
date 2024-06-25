using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string itemName;
    public int damage;
    public float coolDown;
}

[CreateAssetMenu (fileName = "Gun Data", menuName = "Scriptable Objects/Gun")]
public class GunData : WeaponData
{
    public float bulletSpeed;
    public GameObject bulletPrefab;
}
