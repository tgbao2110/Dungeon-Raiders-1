using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    private int currentHealth;
    private int currentEnergy;
    private string currentWeapon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public int Energy
    {
        get { return currentEnergy; }
        set { currentEnergy = value; }
    }

    public string Weapon
    {
        get { return currentWeapon; }
        set { currentWeapon = value; }
    }
}
