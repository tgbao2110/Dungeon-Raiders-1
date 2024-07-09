using System.Collections.Generic;
using UnityEngine;

public class CharacterPicker : MonoBehaviour
{
    [SerializeField] private List<CharacterData> datasets;
    [SerializeField] private Player player;
    private GameObject currentSprite;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();;
        currentSprite = player.transform.GetChild(0).gameObject;
    }

    public bool hasPickedCharacter { get; private set; } = false;

    public void Equip(Armor armor)
    {
        GameObject newSprite = null;
        CharacterData characterData = null;

        switch (armor)
        {
            case Armor.Knight:
                characterData = datasets[0];
                break;
            case Armor.FrostKnight:
                characterData = datasets[1];
                break;
                // Add cases for other armor types here
        }

        newSprite = player.AddCharacter(characterData);
        GameManager.Instance.SetSelectedCharacter(characterData);

        if (currentSprite != null)
        {
            Destroy(currentSprite);
        }
        currentSprite = newSprite;
        hasPickedCharacter = true;
    }
}


public enum Armor
{
    Knight,
    FrostKnight,
    Hidden
    // Add other armor types here
}
