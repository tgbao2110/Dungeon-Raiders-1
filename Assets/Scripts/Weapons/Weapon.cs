using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    protected float lastAtkTime = -9999f;
    protected abstract float coolDown{ get; }
    virtual public void AttackAction() { }
    bool isAtacking = false;
    protected Vector3 direction;

    private void Start()
    {
        Debug.Log(coolDown);
        player = GetComponentInParent<PlayerController>();
    }

    private void Update() 
    {
        Rotate();
    }

    public void Attack()
    {
        if (Time.time - lastAtkTime > coolDown)
        {
            AttackAction();
            lastAtkTime = Time.time;
        }
    }

    void Rotate()
    {
        direction = player.joystick.Direction;
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



