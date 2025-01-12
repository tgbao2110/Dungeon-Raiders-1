using UnityEngine;

public class Bullet : MonoBehaviour, IPausable
{
    protected int damage;
    private Rigidbody2D rb;
    private Vector2 savedVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            savedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        else
        {
            rb.velocity = savedVelocity;
            rb.isKinematic = false;
        }
    }

    public void OnDestroy()
    {
        PauseManager.Instance.UnregisterPausable(this);
    }
}
