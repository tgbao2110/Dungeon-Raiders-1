using UnityEngine;

public abstract class UnlockTextState
{
    protected int price = 0;
    public abstract void StateEnter(UnlockText unlockText);
    public abstract void PurchaseClicked(UnlockText unlockText, Character character);
}

// ------------------------------------------------------------------------------------------
public class Price : UnlockTextState
{
    public Price(int price)
    {
        this.price = price;
    }

    public override void StateEnter(UnlockText unlockText)
    {
        unlockText.tmp.text = "Unlock for " + price.ToString();
    }

    public override void PurchaseClicked(UnlockText unlockText, Character character)
    {
        unlockText.SetState(new Confirm(price, character));
    }
}

// ------------------------------------------------------------------------------------------
public class Confirm : UnlockTextState
{
    private Character character;

    public Confirm(int price, Character character)
    {
        this.price = price;
        this.character = character;
    }

    public override void StateEnter(UnlockText unlockText)
    {
        unlockText.tmp.text = "Confirm purchase?";
    }

    public override void PurchaseClicked(UnlockText unlockText, Character character)
    {
        if (GameManager.Instance.RemoveCoins(price))
        {
            character.SetState(new UnlockedState());
            unlockText.SetState(new Empty());
            character.lobbyButton.ShowSwitchButton(character);
        }
        else
        {
            unlockText.SetState(new Failed());
        }
    }
}

// ------------------------------------------------------------------------------------------
public class Failed : UnlockTextState
{
    public override void StateEnter(UnlockText unlockText)
    {
        unlockText.tmp.text = "Not enough coins!";
    }

    public override void PurchaseClicked(UnlockText unlockText, Character character)
    {
        unlockText.SetState(new Empty());
        character.lobbyButton.ShowAttackButton();
    }
}

// ------------------------------------------------------------------------------------------
public class Empty : UnlockTextState
{
    public override void StateEnter(UnlockText unlockText)
    {
        unlockText.tmp.text = "";
    }

    public override void PurchaseClicked(UnlockText unlockText, Character character)
    {
        // No action needed for empty state
    }
}
