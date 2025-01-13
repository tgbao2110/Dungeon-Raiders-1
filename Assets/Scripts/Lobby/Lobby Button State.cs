using UnityEngine;

public abstract class LobbyButtonState
{
    public abstract void StateEnter(LobbyButton lobbyButton);
    public abstract void OnButtonClicked(LobbyButton lobbyButton);
}

// ----------------------------------------------------------------------------
public class AttackState : LobbyButtonState
{
    public override void StateEnter(LobbyButton lobbyButton)
    {
        lobbyButton.attackButton.gameObject.SetActive(true);
        lobbyButton.switchButton.gameObject.SetActive(false);
        lobbyButton.purchaseButton.gameObject.SetActive(false);
    }

    public override void OnButtonClicked(LobbyButton lobbyButton)
    {
        // Implement attack action if needed
        Debug.Log("Attack action performed");
    }
}

// ----------------------------------------------------------------------------
public class SwitchState : LobbyButtonState
{
    public override void StateEnter(LobbyButton lobbyButton)
    {
        lobbyButton.attackButton.gameObject.SetActive(false);
        lobbyButton.switchButton.gameObject.SetActive(true);
        lobbyButton.purchaseButton.gameObject.SetActive(false);
    }

    public override void OnButtonClicked(LobbyButton lobbyButton)
    {
        lobbyButton.characterPicker.Equip(lobbyButton.currentArmor);
        lobbyButton.HideButtons();
    }
}

// ----------------------------------------------------------------------------
public class PurchaseState : LobbyButtonState
{
    public override void StateEnter(LobbyButton lobbyButton)
    {
        lobbyButton.attackButton.gameObject.SetActive(false);
        lobbyButton.switchButton.gameObject.SetActive(false);
        lobbyButton.purchaseButton.gameObject.SetActive(true);
    }

    public override void OnButtonClicked(LobbyButton lobbyButton)
    {
        lobbyButton.unlockText.OnPurchaseClicked(lobbyButton.currentCharacter);
    }
}
