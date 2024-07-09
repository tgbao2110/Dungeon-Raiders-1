using UnityEngine;

[CreateAssetMenu(fileName = "Ulti", menuName = "Scriptable Objects/Ultimate Data")]
public class UltimateData : ScriptableObject
{
    public string abilityName;
    public Sprite abilityIcon;
    public float cooldownTime;
    public GameObject abilityEffectPrefab;

    public void ActivateAbility(Vector3 position, Transform parent)
    {
        if (abilityEffectPrefab != null)
        {
            GameObject effect = Instantiate(abilityEffectPrefab, position, Quaternion.identity, parent);
            // Additional logic for ability activation
        }
    }
}
