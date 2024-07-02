using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Bullet hit "+ other.name);
            other.GetComponentInChildren<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if(other.tag == "Collider")
        {
            Destroy(this.gameObject);
        }
        
    }
}
