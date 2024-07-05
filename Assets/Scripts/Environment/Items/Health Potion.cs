using UnityEngine;

public class HealthPotion : Potion
{

    public override void Restore(GameObject player)
    {
        Health health = player.GetComponentInChildren<Health>();
        if (health != null)
        {
            health.RestoreHealth(amount);
        }
    }
}
