using UnityEngine;

public class EnergyPotion : Potion
{

    public override void Restore(GameObject player)
    {
        Health health = player.GetComponentInChildren<Health>();
        if (health != null)
        {
            health.RestoreEnergy(amount);
        }
    }
}
