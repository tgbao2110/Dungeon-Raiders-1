using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public int maxHealth;
    public int maxEnergy;
    public int meleeDamage;
    public UltimateData ultimateAbility;
}