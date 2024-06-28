using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected WeaponData weaponData;
    public WeaponData WeaponData => weaponData;
    public GameObject player;
    public PlayerController playerController;
    protected Health health;
    protected float lastAtkTime = -9999f;
    protected abstract float coolDown{ get; }
    virtual public void AttackAction() { }
    protected AttackType attackType;
    bool isAtacking = false;
    protected Vector3 direction;


    public void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponentInChildren<PlayerController>();
        health = player.GetComponentInChildren<Health>();
        SetAttackType();

        Debug.Log(player.gameObject.name + playerController.name + health.name);
    }

    private void Update() 
    {
        Rotate();
    }

    protected void InitializeWeaponData(WeaponData data)
    {
        weaponData = data;
    }

    public void Attack()
    {
        if ((Time.time - lastAtkTime > coolDown) && health.GetCurrentEnergy()>=weaponData.energy)
        {
            AttackAction();
            health.UseEnergy(weaponData.energy);
            lastAtkTime = Time.time;
        }
    }

    public abstract void SetAttackType();

    void Rotate()
    {
        direction = playerController.joystick.Direction;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if(direction.x >0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Flipped along Y-axis
            }
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1f, -1f, 1f); // Flipped along Y-axis
            }
        }
    }
}



