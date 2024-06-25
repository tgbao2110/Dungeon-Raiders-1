using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Gun Data", menuName = "Scriptable Objects/Gun")]
public class GunData : ScriptableObject
{
    public string itemName;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public float coolDown =10;
}
