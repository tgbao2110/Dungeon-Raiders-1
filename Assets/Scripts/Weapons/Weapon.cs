using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    protected float lastAtkTime;
    public float coolDown;
    virtual public void AttackAction() { }
    bool isAtacking = false;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        Debug.Log("FUCK OFF");
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



