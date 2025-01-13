using UnityEngine;
public abstract class CharacterState
{
    public abstract void StateEnter(Character character);
    public abstract void StateUpdate(Character character);
    public abstract void CollisionEnter(Character character);
    public abstract void CollisionExit(Character character);
}

// -------------------------------------------------------------------------------------------------
public class UnlockedState : CharacterState
{
    public override void StateEnter(Character character)
    {
        character.sprite.Show();
        character.text.text = character.armor.ToString();
    }

    public override void StateUpdate(Character character)
    {
        character.text.transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.offset);
    }

    public override void CollisionEnter(Character character)
    {
        character.lobbyButton.ShowSwitchButton(character);
        character.text.gameObject.SetActive(true);
    }

    public override void CollisionExit(Character character)
    {
        character.text.gameObject.SetActive(false);
        character.lobbyButton.ShowAttackButton();
    }
}

// -------------------------------------------------------------------------------------------------
public class LockedState : CharacterState
{
    public override void StateEnter(Character character)
    {
        character.sprite.Hide();
        character.text.text = "???";
    }

    public override void StateUpdate(Character character)
    {
        character.text.transform.position = Camera.main.WorldToScreenPoint(character.transform.position + character.offset);
    }

    public override void CollisionEnter(Character character)
    {
        character.text.gameObject.SetActive(true);
        character.unlockText.SetState(new Price(character.characterPicker.getData(character.armor).price));
        character.lobbyButton.ShowPurchaseButton(character);
    }

    public override void CollisionExit(Character character)
    {
        character.text.gameObject.SetActive(false);
        character.unlockText.SetState(new Empty());
        character.lobbyButton.ShowAttackButton();
    }
}

