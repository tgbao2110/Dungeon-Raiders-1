using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected PlayerController player;
    protected string itemName;
    protected string description;
    protected float lastAtkTime;
    public float coolDown;
    virtual protected void AttackAction() { }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    }

    public void Attack()
    {
        if (Time.time - lastAtkTime > coolDown)
        {
            AttackAction();
            lastAtkTime = Time.time;
        }
    }
}



