using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButton : MonoBehaviour
{
    [SerializeField] public Button switchButton;
    [SerializeField] public Button attackButton;
    [SerializeField] public Button purchaseButton;
    [SerializeField] public UnlockText unlockText;

    public Armor currentArmor;
    public CharacterPicker characterPicker;
    public Character currentCharacter;

    private LobbyButtonState currentState;

    private void Awake()
    {
        switchButton.onClick.AddListener(OnButtonClicked);
        attackButton.onClick.AddListener(OnButtonClicked);
        purchaseButton.onClick.AddListener(OnButtonClicked);

        SetState(new AttackState());
    }

    public void SetState(LobbyButtonState newState)
    {
        currentState = newState;
        currentState.StateEnter(this);
    }

    private void OnButtonClicked()
    {
        currentState.OnButtonClicked(this);
    }

    public void ShowSwitchButton(Character character)
    {
        characterPicker = character.characterPicker;
        currentArmor = character.armor;
        SetState(new SwitchState());
    }

    public void ShowPurchaseButton(Character character)
    {
        characterPicker = character.characterPicker;
        currentArmor = character.armor;
        currentCharacter = character;
        SetState(new PurchaseState());
    }

    public void ShowAttackButton()
    {
        SetState(new AttackState());
    }

    public void HideButtons()
    {
        attackButton.gameObject.SetActive(false);
        switchButton.gameObject.SetActive(false);
        purchaseButton.gameObject.SetActive(false);
    }
}
