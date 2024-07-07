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
        healthBar.SetMaxStat(playerData.maxHealth);
        energyBar.SetMaxStat(playerData.maxEnergy);
        PlayerStats.Instance.Health = playerData.maxHealth;
        PlayerStats.Instance.Energy = playerData.maxEnergy;
        ReloadHealth();
        ReloadEnergy();
    }

    public void TakeDamage(int amount)
    {
        PlayerStats.Instance.Health -= amount;
        ReloadHealth();
        flash.Flash();

        if (PlayerStats.Instance.Health <= 0)
        {
            Die();
        }
    }

    public void UseEnergy(int amount)
    {
        PlayerStats.Instance.Energy -= amount;
        if (PlayerStats.Instance.Energy < 0)
        {
            PlayerStats.Instance.Energy = 0;
        }
        ReloadEnergy();
    }

    public void RestoreHealth(int amount)
    {
        PlayerStats.Instance.Health += amount;
        if (PlayerStats.Instance.Health > playerData.maxHealth)
        {
            PlayerStats.Instance.Health = playerData.maxHealth;
        }
        ReloadHealth();
    }

    public void RestoreEnergy(int amount)
    {
        PlayerStats.Instance.Energy += amount;
        if (PlayerStats.Instance.Energy > playerData.maxEnergy)
        {
            PlayerStats.Instance.Energy = playerData.maxEnergy;
        }
        ReloadEnergy();
    }

    public void ReloadHealth()
    {
        healthBar.SetStat(PlayerStats.Instance.Health);
    }

    public void ReloadEnergy()
    {
        energyBar.SetStat(PlayerStats.Instance.Energy);
    }

    private void Die()
    {
        Time.timeScale = 0;
        // animator.SetTrigger("Die");
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            eventSystem.TriggerGameOver();
        }
    }
}
