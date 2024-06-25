using UnityEngine;

public class EventSystem : MonoBehaviour
{
    PlayerController player;
    PlayerItemInteraction interaction;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject switchWeaponButton;
    Collectable currentCollectable;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        interaction = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerItemInteraction>();

        // Example of how to subscribe methods to events
        PlayerActions.OnAttack += Attack;
        PlayerActions.OnSwitchWeapon += SwitchWeaponPressed;
        PlayerActions.OnEnterCollectable += ShowSwitchWeaponButton;
        PlayerActions.OnExitCollectable += ShowAttackButton;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        PlayerActions.OnAttack -= Attack;
        PlayerActions.OnSwitchWeapon -= SwitchWeaponPressed;
    }

    private void ShowSwitchWeaponButton()
    {
        attackButton.SetActive(false);
        switchWeaponButton.SetActive(true);
    }

    private void ShowAttackButton()
    {
        switchWeaponButton.SetActive(false);
        attackButton.SetActive(true);
    }

    private void Attack()
    {
        Weapon equippedWeapon = interaction.equippedWeapon;
        equippedWeapon?.Attack();
    }

    private void SwitchWeaponPressed()
    {
        interaction.SwitchWeaponPressed(currentCollectable);
    }

    // Methods to be called by UI buttons
    public void TriggerAttack()
    {
        PlayerActions.OnAttack?.Invoke();
    }

    public void TriggerSwitchWeapon()
    {
        PlayerActions.OnSwitchWeapon?.Invoke();
    }

    public void SetCurrentCollectable(Collectable collectable)
    {
        currentCollectable = collectable;
    }
}