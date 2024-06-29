using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            other.GetComponentInChildren<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if(other.tag == "Collider")
        {
            Destroy(this.gameObject);
        }
        
    }
}
