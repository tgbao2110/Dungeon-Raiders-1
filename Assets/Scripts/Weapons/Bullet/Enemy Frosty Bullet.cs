using UnityEngine;

public class EnemyFrostyBullet : Bullet
{
    [SerializeField] private float slowDuration = 5f;
    [SerializeField] private float slowAmount = 0.25f;
    private Color slowColor = Color.blue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            Health playerHealth = other.GetComponentInChildren<Health>();
            PlayerController controller = other.GetComponentInChildren<PlayerController>();

            playerHealth.TakeDamage(damage);
            controller.ApplySlow(slowAmount, slowDuration);
            Destroy(this.gameObject);
        }

        if (other.tag == "Collider")
        {
            Destroy(this.gameObject);
        }
    }
}
