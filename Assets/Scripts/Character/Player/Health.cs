using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    [SerializeField] StatBar healthBar;
    [SerializeField] StatBar energyBar;

    void Awake()
    {
        playerData.CurrentHealth = playerData.maxHealth;
        healthBar.SetMaxStat(playerData.maxHealth);

        playerData.CurrentEnergy = playerData.maxEnergy;
        energyBar.SetMaxStat(playerData.maxEnergy);
    }

    private void Update() {
    }

    public void TakeDamage(int amount)
    {
        playerData.CurrentHealth -= amount;
        healthBar.SetStat(playerData.CurrentHealth);
        if (playerData.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void UseEnergy(int amount)
    {
        playerData.CurrentEnergy -= amount;
        if (playerData.CurrentEnergy < 0)
        {
            playerData.CurrentEnergy = 0;
        }
    }

    public void RestoreHealth(int amount)
    {
        playerData.CurrentHealth += amount;
        if (playerData.CurrentHealth > playerData.maxHealth)
        {
            playerData.CurrentHealth = playerData.maxHealth;
        }
    }

    public void RestoreEnergy(int amount)
    {
        playerData.CurrentEnergy += amount;
        if (playerData.CurrentEnergy > playerData.maxEnergy)
        {
            playerData.CurrentEnergy = playerData.maxEnergy;
        }
    }

    private void Die()
    {
        // Handle death logic (destroy object, play animation, etc.)
        Debug.Log("HEHE YOU DIED MFKER");
    }

    public int GetCurrentHealth()
    {
        return playerData.CurrentHealth;
    }

    public int GetCurrentEnergy()
    {
        return playerData.CurrentEnergy;
    }
}
