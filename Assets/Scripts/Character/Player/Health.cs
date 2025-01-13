using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] SimpleFlash flash;
    [Header("Health UI")]
    [SerializeField] StatBar healthBar;
    [SerializeField] StatBar energyBar;

    private CharacterData data;

    private void Start() {
        ReloadHealth();
        ReloadEnergy();
    }
    public void Initialize(CharacterData pData, SimpleFlash pFlash)
    {
        data = pData;
        flash = pFlash; 

        if (healthBar != null)
        {
            healthBar.SetMaxStat(data.maxHealth);
            energyBar.SetMaxStat(data.maxEnergy);
        }
        
        ReloadHealth();
        ReloadEnergy();
    }

    public void StartGame()
    {
        healthBar = GameObject.Find("Health").GetComponent<StatBar>();
        energyBar = GameObject.Find("Energy").GetComponent<StatBar>();
        PlayerStats.Instance.Health = data.maxHealth;
        PlayerStats.Instance.Energy = data.maxEnergy;
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
        if (PlayerStats.Instance.Health > data.maxHealth)
        {
            PlayerStats.Instance.Health = data.maxHealth;
        }
        ReloadHealth();
    }

    public void RestoreEnergy(int amount)
    {
        PlayerStats.Instance.Energy += amount;
        if (PlayerStats.Instance.Energy > data.maxEnergy)
        {
            PlayerStats.Instance.Energy = data.maxEnergy;
        }
        ReloadEnergy();
    }

    private void ReloadHealth()
    {
        if (healthBar != null)
            healthBar.SetStat(PlayerStats.Instance.Health);
    }

    private void ReloadEnergy()
    {
        if (energyBar != null)
            energyBar.SetStat(PlayerStats.Instance.Energy);
    }

    private void Die()
    {
        Time.timeScale = 0;
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            eventSystem.TriggerGameOver();
        }
    }
}
