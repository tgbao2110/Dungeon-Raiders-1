using System.Collections;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] SimpleFlash flash;
    [SerializeField] Animator animator;

    [Header("Health UI")]
    [SerializeField] StatBar healthBar;
    [SerializeField] StatBar energyBar;

    void Awake()
    {
        playerData.CurrentHealth = playerData.maxHealth;
        healthBar.SetMaxStat(playerData.maxHealth);

        playerData.CurrentEnergy = playerData.maxEnergy;
        energyBar.SetMaxStat(playerData.maxEnergy);
    }

    public void TakeDamage(int amount)
    {
        playerData.CurrentHealth -= amount;
        ReloadHealth();
        flash.Flash();
        
        if (playerData.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void UseEnergy(int amount)
    {
        playerData.CurrentEnergy -= amount;
        ReloadEnergy();
        if (playerData.CurrentEnergy < 0)
        {
            playerData.CurrentEnergy = 0;
        }
    }

    public void RestoreHealth(int amount)
    {
        playerData.CurrentHealth += amount;
        ReloadHealth();
        if (playerData.CurrentHealth > playerData.maxHealth)
        {
            playerData.CurrentHealth = playerData.maxHealth;
        }
    }

    public void RestoreEnergy(int amount)
    {
        playerData.CurrentEnergy += amount;
        ReloadEnergy();
        if (playerData.CurrentEnergy > playerData.maxEnergy)
        {
            playerData.CurrentEnergy = playerData.maxEnergy;
        }
    }

    public int GetCurrentHealth()
    {
        return playerData.CurrentHealth;
    }

    public int GetCurrentEnergy()
    {
        return playerData.CurrentEnergy;
    }

    public void ReloadHealth()
    {
        healthBar.SetStat(playerData.CurrentHealth);
    }

    public void ReloadEnergy()
    {
        energyBar.SetStat(playerData.CurrentEnergy);
    }

    private void Die()
    {
        Time.timeScale = 0;
        //animator.SetTrigger("Die");
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            eventSystem.TriggerGameOver();
        }
    }

}
