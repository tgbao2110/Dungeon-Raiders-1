using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : MonoBehaviour
{
    [SerializeField] protected int amount = 0;

    public abstract void Restore(GameObject player);
}
