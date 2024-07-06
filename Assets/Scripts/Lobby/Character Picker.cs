using System.Collections.Generic;
using UnityEngine;

public class CharacterPicker : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    [SerializeField] GameObject player;
    [SerializeField] GameObject currentSprite;
    GameObject newSprite;
    FixedJoystick fixedJoystick;

    public void Equip(Armor armor)
    {
        fixedJoystick = FindAnyObjectByType<FixedJoystick>();
        switch (armor)
        {
            case Armor.Knight:
                newSprite = Instantiate(characters[0], player.transform.position, Quaternion.identity);
                newSprite.name = "Knight";
                break;
        }

        var facingDirection = player.GetComponentInChildren<PlayerController>().GetFacingDirection();
        if (facingDirection.x<0)
        {
            newSprite.transform.localScale = new Vector3(-1, 1, 1);
        }

        newSprite.transform.SetParent(player.transform);
        var controller = player.GetComponentInChildren<PlayerController>();
        controller.SetSprite(newSprite);
        controller.Initialize(newSprite);
        Destroy(currentSprite);
        currentSprite = newSprite;
        newSprite = null;
    }
}

public enum Armor
{
    Knight,
    Hidden
}
