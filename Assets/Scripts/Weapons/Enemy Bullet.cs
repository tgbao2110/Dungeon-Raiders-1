using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
        }
        if(other.CompareTag("Player"))
        {
            other.GetComponentInChildren<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if(other.CompareTag("Collider"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
