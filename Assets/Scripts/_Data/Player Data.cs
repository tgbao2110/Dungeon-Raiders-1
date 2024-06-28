using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    public int maxHealth;
    public int maxEnergy;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentEnergy;

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public int CurrentEnergy
    {
        get => currentEnergy;
        set => currentEnergy = Mathf.Clamp(value, 0, maxEnergy);
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
    }
}