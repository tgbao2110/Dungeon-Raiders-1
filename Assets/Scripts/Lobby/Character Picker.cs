using System.Collections.Generic;
using UnityEngine;

public class CharacterPicker : MonoBehaviour
{
    [SerializeField] private List<CharacterData> datasets;
    [SerializeField] private Player player;
    private GameObject currentSprite;
    public bool hasPickedCharacter { get; private set; } = false;

    public void Equip(Armor armor)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentSprite = player.transform.GetChild(0).gameObject;
        GameObject newSprite = null;
        CharacterData characterData = null;

        switch (armor)
        {
            case Armor.Knight:
                characterData = datasets[0];
                break;
            case Armor.Ninja:
                characterData = datasets[1];
                break;
            case Armor.Ghost:
                characterData = datasets[2];
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

    public CharacterData getData(Armor armor)
    {
        CharacterData characterData = null;

        switch (armor)
        {
            case Armor.Knight:
                characterData = datasets[0];
                break;
            case Armor.Ninja:
                characterData = datasets[1];
                break;
               case Armor.Ghost:
                characterData = datasets[2];
                break; 
                // Add cases for other armor types here
        }
        
        return characterData;
    }
}


public enum Armor
{
    Knight,
    Ninja,
    Ghost,
    Hidden
}
